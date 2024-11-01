using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFPExtractetor
{
    /// <summary>
    /// PFP解压操作类
    /// </summary>
    internal class PFPExtractetor
    {
        /// <summary>
        /// &lt;List&gt; 取PFP内部文件列表
        /// <param name="pfpfilePath">(文本型 欲获取内部文件列表的PFP文件) </param>
        /// <returns><para>成功返回PFP文件的单个文件的目录长度、完整文件路径、、文件偏移、实际大小、压缩大小，并封装在 &lt;List&gt;中</para></returns>
        /// </summary>
        public static List<Tuple<int, string, long, long>> GetPFPInformation(string pfpfilePath)
        {
            List<Tuple<int, string, long, long>> pfpList = new List<Tuple<int, string, long, long>>();

            using (FileStream fs = new FileStream(pfpfilePath, FileMode.Open))
            {
                // 检查是否为PFP文件，如果不是，抛出异常
                byte[] fileHead = { 0x50, 0x46, 0x50, 0x4B };                            //PFP文件头“PFPK”的Hex字节集数组
                byte[] checkHead = new byte[fileHead.Length];                            //欲打开的PFP文件头
                // 读入PFP文件头
                fs.Read(checkHead, 0, fileHead.Length);
                Console.WriteLine(checkHead.SequenceEqual(fileHead));
                // 若两边的字节集数组内容不一样或者长度不一样则视为不是PFP文件，并抛出异常
                if (!checkHead.SequenceEqual(fileHead) || fileHead.Length != checkHead.Length)
                {

                    throw new Exception("The file\"" + pfpfilePath + "\" is not a valid PFP format file!");
                }


                // 头部未发现异常，开始解析文件列表

                //取文件个数
                byte[] fileCountBit = new byte[4];      // 字节集状态下的数据区物理大小数值
                long fileCount;                         // 长整型的数据区物理大小数值
                // 定位到文件的0004h
                fs.Seek(4, SeekOrigin.Begin);
                // 取PFP内部文件数量信息
                fs.Read(fileCountBit, 0, fileCountBit.Length);
                // 将取到的pfp内部文件信息补满八字节，然后转换成长整型数值
                fileCount = BitConverter.ToInt64(PublicFunction.EightByteConverter(fileCountBit), 0);
                Console.WriteLine("PFP文件数量为:" + fileCount.ToString());
                Console.WriteLine("获取完数量的偏移为:" + fs.Position.ToString("X"));

                // 创建字节偏移记录，并将文件流移到08h处
                long byteOffset = fs.Position;
                fs.Seek(byteOffset, SeekOrigin.Begin);
                Console.WriteLine("开始读取文件列表时候的偏移为:" + fs.Position.ToString("X"));

                // 开始读取文件
                for (int i = 0; i < fileCount; i++)
                {
                    // 取目录长度
                    byte[] flieNameLenghBit = new byte[1];
                    fs.Read(flieNameLenghBit, 0, 1);
                    int flieNameLengh = BitConverter.ToInt32(PublicFunction.EightByteConverter(flieNameLenghBit), 0);
                    //偏移移动一位
                    byteOffset += 1;
                    fs.Seek(byteOffset, SeekOrigin.Begin);

                    // 取文件名
                    byte[] flieNameBit = new byte[flieNameLengh];
                    fs.Read(flieNameBit, 0, flieNameLengh);
                    string flieName = Encoding.Default.GetString(flieNameBit);
                    //偏移移动文件名长度位数
                    byteOffset += flieNameLengh;
                    fs.Seek(byteOffset, SeekOrigin.Begin);

                    // 取文件偏移
                    byte[] fileOffsetBit = new byte[4];
                    fs.Read(fileOffsetBit, 0, fileOffsetBit.Length);
                    // 四字节补到八字节，再转换成长整型数值，
                    long fileOffset = BitConverter.ToInt64(PublicFunction.EightByteConverter(fileOffsetBit), 0);
                    //偏移四个字节
                    byteOffset += 4;
                    fs.Seek(byteOffset, SeekOrigin.Begin);

                    // 取文件大小
                    byte[] fileSizehBit = new byte[4];
                    // 四字节补到八字节，再转换成长整型数值，
                    fs.Read(fileSizehBit, 0, fileSizehBit.Length);
                    long fileSizeBit = BitConverter.ToInt64(PublicFunction.EightByteConverter(fileSizehBit), 0);
                    //偏移四个字节
                    byteOffset += 4;
                    fs.Seek(byteOffset, SeekOrigin.Begin);

                    // 调试输出此文件信息
                    Console.WriteLine("-------------------\n" +
                                        "读取第" + (i + 1) + "文件成功！\n" +
                                        "===================\n" +
                                        "文件名长度:" + flieNameLengh.ToString() + "\n" +
                                        "文件名称:" + flieName + "\n" +
                                        "文件偏移:0x" + fileOffset.ToString("X") + "\n" +
                                        "文件大小:" + fileSizeBit.ToString() + "字节\n" +
                                        "个文件后的偏移" + fs.Position.ToString("X") + "\n" +
                                        "-------------------\n\n");

                    // 加入到四元List
                    pfpList.Add(Tuple.Create(flieNameLengh, flieName, fileOffset, fileSizeBit));
                }
                // 返回四元List
                return pfpList;
            }
        }

        /// <summary>
        /// 解压单独文件
        /// <param name="pfpfilePath">(文本型 欲单独解压的PFP文件, </param>
        /// <param name="fileList">List 已处理好的文件列表, </param>
        /// <param name="targetNum">(欲解压的文件顺序号, </param>
        /// <param name="savePath">欲保存的路径)</param>
        /// </summary>
        public static void ExtractFileSingle(string pfpfilePath, List<Tuple<int, string, long, long>> fileList, int targetNum, string savePath)
        {
            // 取单独的文件名
            string fileName = Path.GetFileName(fileList[targetNum].Item2);

            // 开始获取指定文件的物理数据
            long pfpOffset = fileList[targetNum].Item3;               //pfp文件偏移
            long actualDataSize = fileList[targetNum].Item4;          // 指定文件的真实大小


            byte[] actualData = new byte[actualDataSize];                                          // 指定文件的物理数据字节集数组


            // 将物理数据拷贝到文件的物理数据字节集数组中
            using (FileStream fs = new FileStream(pfpfilePath, FileMode.Open))
            {
                fs.Seek(pfpOffset, SeekOrigin.Begin);
                fs.Read(actualData, 0, (int)actualDataSize);

                File.WriteAllBytes(savePath, actualData);
                // 关掉文件流以节约内存
                fs.Close();
            }
        }


        /// <summary>
        /// &lt;List&gt; 解压所有文件
        /// <param name="pfpfilePath">(文本型 欲单解压的PFP文件, </param>
        /// <param name="fileList">List 已处理好的文件列表, </param>
        /// <param name="savePath">欲保存的路径)</param>
        /// </summary>
        public static void ExtractAllFile(string pfpfilePath, List<Tuple<int, string, long, long>> fileList, string savePath)
        {
            // 取文件数量，即List成员数
            int fileCount = fileList.Count;

            // 设置解压文件夹，保存到文件名+_PFPUnpacked中
            string extractFolder = Path.GetFileNameWithoutExtension(pfpfilePath) + "_PFPUnpacked";
            Directory.CreateDirectory(extractFolder);

            //开始解压操作
            using (FileStream fs = new FileStream(pfpfilePath, FileMode.Open))
            {
                // 进入for循环
                for (int i = 0; i < fileCount; i++)
                {
                    // 取文件名和文件路径
                    string filePath = fileList[i].Item2;
                    string fileDir = Path.GetDirectoryName(filePath);
                    string fileName = Path.GetFileName(filePath);

                    // 置该文件最终绝对路径
                    string finalPath = savePath + "\\" + extractFolder + "\\" + filePath;
                    long pfpOffset = fileList[i].Item3;                                         //pfp文件偏移
                    long actualDataSize = fileList[i].Item4;                                    // 指定文件的真实大小
                    byte[] actualData = new byte[actualDataSize];                               // 指定文件的物理数据字节集数组

                    // 移到指定位置并拷贝文件数据
                    fs.Seek(pfpOffset, SeekOrigin.Begin);
                    fs.Read(actualData, 0, (int)actualDataSize);

                    // 检查目录是否存在，不存在则创建
                    if (!Directory.Exists(Path.GetDirectoryName(finalPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(finalPath));
                    }

                    // 此轮工作已结束，写到文件
                    File.WriteAllBytes(finalPath, actualData);
                }
                // 关掉文件流以节约内存
                fs.Close();
            }
        }



        public static async Task ExtractAllFileAsync(string pfpfilePath, List<Tuple<int, string, long, long>> targetFile, string savePath, Action<FileExtractProgress> progressCallback)
        {
            // 取文件数量，即List成员数
            int fileCount = targetFile.Count;

            // 设置解压文件夹，保存到文件名+_PFPUnpacked中
            string extractFolder = Path.GetFileNameWithoutExtension(pfpfilePath) + "_PFPUnpacked";
            Directory.CreateDirectory(extractFolder);

            //开始解压操作
            using (FileStream fs = new FileStream(pfpfilePath, FileMode.Open))
            {
                for (int i = 0; i < fileCount; i++)
                {
                    // 取文件名和文件路径
                    string filePath = targetFile[i].Item2;
                    string fileDir = Path.GetDirectoryName(filePath);
                    string fileName = Path.GetFileName(filePath);

                    // 置该文件最终绝对路径
                    string finalPath = savePath + "\\" + extractFolder + "\\" + filePath;
                    long pfpOffset = targetFile[i].Item3;                                       // pfp文件偏移
                    long actualDataSize = targetFile[i].Item4;                                  // 指定文件的真实大小
                    byte[] actualData = new byte[actualDataSize];                               // 指定文件的物理数据字节集数组

                    // 移到指定位置并拷贝文件数据
                    fs.Seek(pfpOffset, SeekOrigin.Begin);
                    await fs.ReadAsync(actualData, 0, (int)actualDataSize);

                    // 检查目录是否存在，不存在则创建
                    if (!Directory.Exists(Path.GetDirectoryName(finalPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(finalPath));
                    }

                    // 此轮工作已结束，写到文件
                    File.WriteAllBytes(finalPath, actualData);

                    // 计算并报告进度
                    int percent = (int)(((i + 1) / (double)fileCount) * 100);
                    progressCallback(new FileExtractProgress { Percentage = percent, extracting = filePath });

                }
                // 关掉文件流以节约内存
                fs.Close();
            }
        }

        /// <summary>
        /// 全部解压异步操作类
        /// </summary>
        public class FileExtractProgress
        {
            public int Percentage { get; set; }         // 解压百分比
            public string extracting { get; set; }      // 解压文件名
        }

    }
}
