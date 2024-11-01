using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Media;

namespace PFPExtractetor
{
    public partial class MainForm : Form
    {
        // 已获取的PFP文件列表（文件路径长度，文件路径，文件偏移，文件真实大小小）
        List<Tuple<int, string, long, long>> pfpList = new List<Tuple<int, string, long, long>>();

        // 被操作的PFP文件路径
        string targetPFP;

        /// <summary>
        /// 复位图形界面
        /// </summary>
        private void GUIReset()
        {
            label_ExtractState.Visible = false;
            label_progress.Visible = false;
            btn_ExtractSingle.Enabled = false;
            btn_ExtractAll.Enabled = false;
            btn_Browse.Enabled = false;
            targetPFP = string.Empty;
            pfpList.Clear();
            listView_PFPList.Items.Clear();
            progressBar_Extracted.Value = 0;
            this.Text = string.Empty;
            this.Text = PublicFunction.GetAPPInformation(1) + Convert.ToChar(32) + PublicFunction.GetAPPInformation(2);
        }

        private void OpenFile(string filePath)
        {
            GUIReset();                                     // 复位图形界面
            targetPFP = filePath;                            // 欲获取列表的PFP文件

            // 尝试获取PFP文件列表
            try
            {
                LoadPFP(targetPFP);                          //载入PFP

                // 载入成功，继续执行                 
                textBox_ExtractAllOutDir.Text = Path.GetDirectoryName(targetPFP);    // 解压路径输入框解压路径为PFP文件所在路径
                textBox_ExtractAllOutDir.Enabled = true;

                // 配置图像界面
                btn_ExtractSingle.Enabled = true;
                btn_ExtractAll.Enabled = true;
                btn_Browse.Enabled = true;
                label_ExtractState.Visible = true;
                label_ExtractState.Font = new System.Drawing.Font(label_ExtractState.Font, label_ExtractState.Font.Style | System.Drawing.FontStyle.Bold);
                label_ExtractState.ForeColor = System.Drawing.Color.Blue;
                label_ExtractState.Text = "支持的格式，可以解压！(共有" + pfpList.Count + "个文件)";
                this.Text += "—— (已打开\"" + targetPFP + "\")";

                // 播放系统提示音
                SystemSounds.Asterisk.Play();
            }
            catch (Exception ex)                                                     // 捕获LoadPFP抛出的异常，并配置读取失败时候图形界面
            {
                textBox_ExtractAllOutDir.Text = string.Empty;
                textBox_ExtractAllOutDir.Enabled = false;
                label_ExtractState.Visible = true;
                label_ExtractState.Font = new System.Drawing.Font(label_ExtractState.Font, label_ExtractState.Font.Style | System.Drawing.FontStyle.Bold);
                label_ExtractState.ForeColor = System.Drawing.Color.Red;
                btn_ExtractSingle.Enabled = false;
                btn_ExtractAll.Enabled = false;
                btn_Browse.Enabled = false;

                // 把抛出异常的消息放到列表框显示
                ListViewItem pfpItemList = new ListViewItem();
                pfpItemList.SubItems.Add(ex.Message);
                this.listView_PFPList.Items.Add(pfpItemList);

                //调试输出异常
                Console.WriteLine(ex.Message);
                label_ExtractState.Text = "未知的支持的格式，不可以解压！";

                // 播放系统提示音
                SystemSounds.Hand.Play();

                return;
            }
        }

        /// <summary>
        /// 载入PFP文件
        /// <param name="targetPFP"></param>
        /// </summary>
        private void LoadPFP(string targetPFP)
        {
            try
            {
                // 取PFP文件列表
                pfpList = PFPExtractetor.GetPFPInformation(targetPFP);

                // 开始更新列表框
                this.listView_PFPList.BeginUpdate();
                for (int i = 0; i < pfpList.Count; i++)
                {
                    ListViewItem pfpItemList = new ListViewItem();
                    pfpItemList.Text = (i + 1).ToString();                                          // 序号列
                    pfpItemList.SubItems.Add(pfpList[i].Item2);                                     // 文件名列
                    pfpItemList.SubItems.Add("0x" + pfpList[i].Item3.ToString("X"));                // 文件偏移列
                    pfpItemList.SubItems.Add(PublicFunction.BytesToSize(pfpList[i].Item4));         // 文件大小列
                    this.listView_PFPList.Items.Add(pfpItemList);
                }
                this.listView_PFPList.EndUpdate();
            }
            catch (Exception ex)                                                                    // 当GetPFPInformation抛出异常后要抛给图形界面进行显示
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 解压全部
        /// <param name="extPath">(文本型 解压缩目标目录)</param>
        /// </summary>
        private async void ExtractetorAll(string extPath)
        {
            // 配置图形界面状态
            label_ExtractState.Font = new System.Drawing.Font(label_ExtractState.Font, label_ExtractState.Font.Style ^ System.Drawing.FontStyle.Bold);
            btn_ExtractSingle.Enabled = false;
            btn_ExtractAll.Enabled = false;
            btn_Browse.Enabled = false;
            btn_OpenPFP.Enabled = false;
            textBox_ExtractAllOutDir.Enabled = false;

            // 执行解压操作
            await PFPExtractetor.ExtractAllFileAsync(targetPFP, pfpList, extPath, progress =>
            {
                // 在UI线程上更新解压状态
                progressBar_Extracted.Value = progress.Percentage;
                label_ExtractState.ResetForeColor();
                label_ExtractState.Text = "正在解压:\"" + progress.extracting + "\"";
                label_progress.Visible = true;
                label_progress.Text = progress.Percentage.ToString() + "%";
                if (progress.Percentage == 100)
                {
                    label_ExtractState.Text = "解压完成！";
                    label_progress.Visible = false;
                    btn_OpenPFP.Enabled = true;
                    btn_ExtractSingle.Enabled = true;
                    btn_ExtractAll.Enabled = true;
                    btn_Browse.Enabled = true;
                    textBox_ExtractAllOutDir.Enabled = true;

                    // 解压完成后操作，询问是否打开解压文件夹
                    DialogResult r = MessageBox.Show("解压完成，是否打开解压的文件夹？", "解压完成", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (r == DialogResult.Yes)
                    {
                        string folderPath = extPath + "\\" + Path.GetFileNameWithoutExtension(targetPFP) + "_PFPUnpacked";
                        try
                        {
                            // 创建一个ProcessStartInfo对象来配置进程启动的信息
                            ProcessStartInfo startInfo = new ProcessStartInfo
                            {
                                // 要定位和打开的文件夹路径
                                FileName = "explorer.exe",
                                Arguments = $"/e,/select, \"{folderPath}\""
                            };
                            startInfo.UseShellExecute = true;
                            Process.Start(startInfo);
                        }
                        catch (Exception ex)
                        {
                            // 处理可能出现的异常
                            Console.WriteLine($"无法打开文件夹: {ex.Message}");
                        }
                    }
                }
            });
        }


        /// <summary>
        /// 构建窗体
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗口加载完毕响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Form_Load(object sender, EventArgs e)
        {
            // 显示软件名称及版本号
            this.Text = PublicFunction.GetAPPInformation(1) + Convert.ToChar(32) + PublicFunction.GetAPPInformation(2);
        }

        /// <summary>
        /// “打开”按钮单击响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_OpenPFP_Click(object sender, EventArgs e)
        {
            // 当选中文件并点击“打开”按钮的时候执行
            if (openFileDialog_OpenPFP.ShowDialog() == DialogResult.OK)
            {
                OpenFile(openFileDialog_OpenPFP.FileName);
            }
        }

        /// <summary>
        ///  点击“解压选中”按钮的响应函数
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </summary>
        private void Btn_ExtractSingle_Click(object sender, EventArgs e)
        {
            try
            {
                // 取列表框索引，索引是多少就是文件列表List相应的索引
                int fileNum = listView_PFPList.SelectedIndices[0];

                // 取文件名称作为默认文件名称
                string defaultFileName = Path.GetFileName(pfpList[fileNum].Item2);
                saveFileDialog_ExtractSingle.FileName = defaultFileName;

                // 调起对话框
                if (saveFileDialog_ExtractSingle.ShowDialog() == DialogResult.OK)
                {
                    // 开始解压
                    PFPExtractetor.ExtractFileSingle(targetPFP, pfpList, fileNum, saveFileDialog_ExtractSingle.FileName);
                    DialogResult r = MessageBox.Show("解压完成，是否打开解压的文件夹？", "解压完成", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (r == DialogResult.Yes)
                    {
                        string targetPFP = saveFileDialog_ExtractSingle.FileName;
                        try
                        {
                            // 创建一个ProcessStartInfo对象来配置进程启动的信息
                            ProcessStartInfo startInfo = new ProcessStartInfo
                            {
                                // 要定位和打开的文件夹路径
                                FileName = "explorer.exe",
                                Arguments = $"/e,/select, \"{targetPFP}\""
                            };
                            startInfo.UseShellExecute = true;
                            Process.Start(startInfo);
                        }
                        catch (Exception ex)
                        {
                            // 处理可能出现的异常
                            Console.WriteLine($"对不起，无法打开文件夹: {ex.Message}");
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("请选择要单独解压的文件！", "未选择文件", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch
            {
                MessageBox.Show("发生未知错误！操作失败", "未知错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        /// <summary>
        /// 点击“解压全部”按钮的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ExtractAll_Click(object sender, EventArgs e)
        {
            // 当解压路径输入框为空时，不能进行解压操作，并弹出对话框，然后点击一下“浏览”   按钮  
            if (textBox_ExtractAllOutDir.Text != string.Empty)
            {
                ExtractetorAll(textBox_ExtractAllOutDir.Text);
            }
            else
            {
                MessageBox.Show("请选择解压路径！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Btn_Browse_Click(sender, e);
            }
        }

        /// <summary>
        /// 点击“浏览”按钮的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Browse_Click(object sender, EventArgs e)
        {
            // 目录选择对话框选中目录为当前打开的PFP所在路径
            folderBrowserDialog_ExtractAll.SelectedPath = Path.GetDirectoryName(targetPFP);

            // 选中后将选择的路径返回给解压路径输入框
            if (folderBrowserDialog_ExtractAll.ShowDialog() == DialogResult.OK)
            {
                textBox_ExtractAllOutDir.Text = folderBrowserDialog_ExtractAll.SelectedPath;
            }
        }

        /// <summary>
        /// 将文件拖拽并进入到PFP文件列表框的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_PFPList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // 显示复制效果
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 将文件拖拽并松开鼠标到PFP文件列表框的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_PFPList_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop); // 获取拖放的文件路径数组
            if (files.Length > 0)
            {
                OpenFile(files[0]);
            }
        }

        /// <summary>
        /// PFP文件列表框双击后响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_PFPList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 双击列表项可单独解压文件，但必须在pfpList变量有数据的情况下才能触发
            if (pfpList.Count >= 0)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (btn_ExtractSingle.Enabled == true)
                    {
                        Btn_ExtractSingle_Click(sender, e);
                    }
                }
            }
        }

        /// <summary>
        /// 点击“关闭工具”按钮的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Exit_Click(object sender, EventArgs e)
        {
            // 执行关闭操作
            this.Close();
        }

        /// <summary>
        /// 点击“版本信息”按钮的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_About_Click(object sender, EventArgs e)
        {
            Form_About form_About = new Form_About();
            form_About.ShowDialog();
        }

        /// <summary>
        /// 点击“使用说明”按钮的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Help_Click(object sender, EventArgs e)
        {
            Form_Help form_Help = new Form_Help();
            form_Help.ShowDialog();
        }

        /// <summary>
        /// 窗口关闭时的响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 询问是否退出
            DialogResult r = MessageBox.Show("确定要退出吗", "退出确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (r == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
