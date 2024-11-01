using System;
using System.Reflection;

namespace PFPExtractetor
{
    /// <summary>
    /// 公共函数
    /// </summary>
    internal class PublicFunction
    {
        ///<summary>
        /// &lt;文本型&gt; 取软件基本信息 
        /// <param name="paramcode">(整数型 要获取的信息代码)<para>参数代码含义:</para>1：取软件名称；2:取软件版本；3:取软件开发者；4、取软件产品名称。<para></para></param>
        /// <returns><para></para>返回文本型基本信息结果，失败使用了除1-4以外的参数则返回"InvalidRequest"。</returns> 
        /// </summary>
        public static string GetAPPInformation(int paramcode)
        {
            //取程序名称、版本、公司、产品名称、版权信息
            Assembly asm = Assembly.GetExecutingAssembly();           
            AssemblyTitleAttribute asmTitle = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyTitleAttribute));
            Version asmVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            AssemblyCompanyAttribute asmCompany = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyCompanyAttribute));
            AssemblyProductAttribute asmProduct = (AssemblyProductAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyProductAttribute));
            AssemblyCopyrightAttribute asmCopyRight = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyCopyrightAttribute));
            
            // 将获取接管转为文本型
            string version = "Ver " + asmVersion.ToString();
            string title = asmTitle.Title;
            string company = asmCompany.Company;
            string appEnglishNane = asmProduct.Product;
            string copyRight = asmCopyRight.Copyright.ToString();

            // 参数接到不同代码返回不同的信息
            if (paramcode == 1)                 // 返回软件名称
            {
                return title;
            }
            else if (paramcode == 2)            // 返回软件版本
            {
                return version;
            }
            else if (paramcode == 3)            // 返回软件公司
            {
                return company;
            }
            else if (paramcode == 4)            // 返回软件产品名称
            {
                return appEnglishNane;
            }
            else if (paramcode == 5)            // 返回版权信息
            {
                return copyRight;
            }
            else                                // 输入其他字符则返回"InvalidRequest"
            {
                return "InvalidRequest";
            }
        }

        /// <summary>
        ///  &lt;字节集&gt; 8字节补零
        /// <param name="bytes">(字节集 欲填充0x00的8字节字节集数组)</param>
        /// <returns><para>返回处理后的8字节数组</para></returns>
        /// </summary>
        public static byte[] EightByteConverter(byte[] bytes)
        {
            // 补到8字节用于Bit转换
            byte[] newByees = new byte[8];
            Array.Copy(bytes, 0, newByees, 0, bytes.Length);
            for (int i = bytes.Length; i < 8; i++)
            {
                newByees[i] = 0x00; // 补充0x00
            }
            // 将处理完的字节集数组交还给bytes变量
            return newByees;
        }

        /// <summary>
        /// &lt;文本型&gt; 字节大小数值转换
        /// <param name="bytes">(长整型 欲转换的字节大小数组)</param>
        /// <returns></returns>
        /// </summary>
        public static string BytesToSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            long order = 0;

            // 检查bytes是否大于1024，如果是，则进行转换
            while (bytes >= 1024 && order < sizes.Length - 1)
            {
                order++;
                bytes = bytes / 1024;
            }

            // 返回格式化后的字符串，保留两位小数
            return string.Format("{0:0.##} {1}", bytes, sizes[order]);
        }
    }
}

