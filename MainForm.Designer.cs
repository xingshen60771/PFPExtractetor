namespace PFPExtractetor
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.listView_PFPList = new System.Windows.Forms.ListView();
            this.num = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.file = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileActualSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_OpenPFP = new System.Windows.Forms.Button();
            this.openFileDialog_OpenPFP = new System.Windows.Forms.OpenFileDialog();
            this.btn_ExtractSingle = new System.Windows.Forms.Button();
            this.btn_ExtractAll = new System.Windows.Forms.Button();
            this.saveFileDialog_ExtractSingle = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog_ExtractAll = new System.Windows.Forms.FolderBrowserDialog();
            this.progressBar_Extracted = new System.Windows.Forms.ProgressBar();
            this.label_ExtractState = new System.Windows.Forms.Label();
            this.textBox_ExtractAllOutDir = new System.Windows.Forms.TextBox();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.label_ExtractAll = new System.Windows.Forms.Label();
            this.label_progress = new System.Windows.Forms.Label();
            this.button_Exit = new System.Windows.Forms.Button();
            this.button_About = new System.Windows.Forms.Button();
            this.btn_Help = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView_PFPList
            // 
            this.listView_PFPList.AllowDrop = true;
            this.listView_PFPList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.num,
            this.file,
            this.fileOffset,
            this.fileActualSize});
            this.listView_PFPList.FullRowSelect = true;
            this.listView_PFPList.GridLines = true;
            this.listView_PFPList.HideSelection = false;
            this.listView_PFPList.LabelEdit = true;
            this.listView_PFPList.Location = new System.Drawing.Point(14, 7);
            this.listView_PFPList.Name = "listView_PFPList";
            this.listView_PFPList.Size = new System.Drawing.Size(1024, 560);
            this.listView_PFPList.TabIndex = 2;
            this.listView_PFPList.UseCompatibleStateImageBehavior = false;
            this.listView_PFPList.View = System.Windows.Forms.View.Details;
            this.listView_PFPList.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListView_PFPList_DragDrop);
            this.listView_PFPList.DragEnter += new System.Windows.Forms.DragEventHandler(this.ListView_PFPList_DragEnter);
            this.listView_PFPList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_PFPList_MouseDoubleClick);
            // 
            // num
            // 
            this.num.Text = "序号";
            this.num.Width = 80;
            // 
            // file
            // 
            this.file.Text = "文件名称";
            this.file.Width = 400;
            // 
            // fileOffset
            // 
            this.fileOffset.Text = "文件偏移";
            this.fileOffset.Width = 100;
            // 
            // fileActualSize
            // 
            this.fileActualSize.Text = "实际大小";
            this.fileActualSize.Width = 100;
            // 
            // btn_OpenPFP
            // 
            this.btn_OpenPFP.Location = new System.Drawing.Point(918, 614);
            this.btn_OpenPFP.Name = "btn_OpenPFP";
            this.btn_OpenPFP.Size = new System.Drawing.Size(120, 32);
            this.btn_OpenPFP.TabIndex = 5;
            this.btn_OpenPFP.Text = "打开(&O)";
            this.btn_OpenPFP.UseVisualStyleBackColor = true;
            this.btn_OpenPFP.Click += new System.EventHandler(this.Btn_OpenPFP_Click);
            // 
            // openFileDialog_OpenPFP
            // 
            this.openFileDialog_OpenPFP.Filter = "PFP资源文件|*.pfp|所有文件|*.*";
            this.openFileDialog_OpenPFP.Title = "请选择PFP资源文件";
            // 
            // btn_ExtractSingle
            // 
            this.btn_ExtractSingle.Enabled = false;
            this.btn_ExtractSingle.Location = new System.Drawing.Point(792, 614);
            this.btn_ExtractSingle.Name = "btn_ExtractSingle";
            this.btn_ExtractSingle.Size = new System.Drawing.Size(120, 32);
            this.btn_ExtractSingle.TabIndex = 6;
            this.btn_ExtractSingle.Text = "解压选中(&S)";
            this.btn_ExtractSingle.UseVisualStyleBackColor = true;
            this.btn_ExtractSingle.Click += new System.EventHandler(this.Btn_ExtractSingle_Click);
            // 
            // btn_ExtractAll
            // 
            this.btn_ExtractAll.Enabled = false;
            this.btn_ExtractAll.Location = new System.Drawing.Point(666, 614);
            this.btn_ExtractAll.Name = "btn_ExtractAll";
            this.btn_ExtractAll.Size = new System.Drawing.Size(120, 32);
            this.btn_ExtractAll.TabIndex = 7;
            this.btn_ExtractAll.Text = "解压全部(&A)";
            this.btn_ExtractAll.UseVisualStyleBackColor = true;
            this.btn_ExtractAll.Click += new System.EventHandler(this.Btn_ExtractAll_Click);
            // 
            // saveFileDialog_ExtractSingle
            // 
            this.saveFileDialog_ExtractSingle.AddExtension = false;
            this.saveFileDialog_ExtractSingle.Filter = "所有文件 (*.*)|*.*";
            this.saveFileDialog_ExtractSingle.Title = "请选择保存位置";
            // 
            // folderBrowserDialog_ExtractAll
            // 
            this.folderBrowserDialog_ExtractAll.Description = "请选择解压路径：";
            // 
            // progressBar_Extracted
            // 
            this.progressBar_Extracted.Location = new System.Drawing.Point(12, 681);
            this.progressBar_Extracted.Name = "progressBar_Extracted";
            this.progressBar_Extracted.Size = new System.Drawing.Size(522, 23);
            this.progressBar_Extracted.TabIndex = 8;
            // 
            // label_ExtractState
            // 
            this.label_ExtractState.AutoSize = true;
            this.label_ExtractState.Location = new System.Drawing.Point(12, 657);
            this.label_ExtractState.Name = "label_ExtractState";
            this.label_ExtractState.Size = new System.Drawing.Size(55, 15);
            this.label_ExtractState.TabIndex = 9;
            this.label_ExtractState.Text = "label1";
            this.label_ExtractState.Visible = false;
            // 
            // textBox_ExtractAllOutDir
            // 
            this.textBox_ExtractAllOutDir.Enabled = false;
            this.textBox_ExtractAllOutDir.Location = new System.Drawing.Point(12, 618);
            this.textBox_ExtractAllOutDir.Name = "textBox_ExtractAllOutDir";
            this.textBox_ExtractAllOutDir.Size = new System.Drawing.Size(522, 25);
            this.textBox_ExtractAllOutDir.TabIndex = 10;
            // 
            // btn_Browse
            // 
            this.btn_Browse.Enabled = false;
            this.btn_Browse.Location = new System.Drawing.Point(540, 614);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(120, 32);
            this.btn_Browse.TabIndex = 11;
            this.btn_Browse.Text = "浏览…(&B)";
            this.btn_Browse.UseVisualStyleBackColor = true;
            this.btn_Browse.Click += new System.EventHandler(this.Btn_Browse_Click);
            // 
            // label_ExtractAll
            // 
            this.label_ExtractAll.AutoSize = true;
            this.label_ExtractAll.Location = new System.Drawing.Point(12, 591);
            this.label_ExtractAll.Name = "label_ExtractAll";
            this.label_ExtractAll.Size = new System.Drawing.Size(127, 15);
            this.label_ExtractAll.TabIndex = 12;
            this.label_ExtractAll.Text = "请选择解压路径：";
            // 
            // label_progress
            // 
            this.label_progress.AutoSize = true;
            this.label_progress.Location = new System.Drawing.Point(537, 685);
            this.label_progress.Name = "label_progress";
            this.label_progress.Size = new System.Drawing.Size(55, 15);
            this.label_progress.TabIndex = 13;
            this.label_progress.Text = "label2";
            this.label_progress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_progress.Visible = false;
            // 
            // button_Exit
            // 
            this.button_Exit.Location = new System.Drawing.Point(918, 676);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(120, 32);
            this.button_Exit.TabIndex = 14;
            this.button_Exit.Text = "关闭工具(&X)";
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.Button_Exit_Click);
            // 
            // button_About
            // 
            this.button_About.Location = new System.Drawing.Point(792, 676);
            this.button_About.Name = "button_About";
            this.button_About.Size = new System.Drawing.Size(120, 32);
            this.button_About.TabIndex = 15;
            this.button_About.Text = "版本信息(&I)";
            this.button_About.UseVisualStyleBackColor = true;
            this.button_About.Click += new System.EventHandler(this.Btn_About_Click);
            // 
            // btn_Help
            // 
            this.btn_Help.Location = new System.Drawing.Point(666, 676);
            this.btn_Help.Name = "btn_Help";
            this.btn_Help.Size = new System.Drawing.Size(120, 32);
            this.btn_Help.TabIndex = 16;
            this.btn_Help.Text = "使用说明(&H)";
            this.btn_Help.UseVisualStyleBackColor = true;
            this.btn_Help.Click += new System.EventHandler(this.Btn_Help_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 721);
            this.Controls.Add(this.btn_Help);
            this.Controls.Add(this.button_About);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.label_progress);
            this.Controls.Add(this.label_ExtractAll);
            this.Controls.Add(this.btn_Browse);
            this.Controls.Add(this.textBox_ExtractAllOutDir);
            this.Controls.Add(this.label_ExtractState);
            this.Controls.Add(this.progressBar_Extracted);
            this.Controls.Add(this.btn_ExtractAll);
            this.Controls.Add(this.btn_ExtractSingle);
            this.Controls.Add(this.btn_OpenPFP);
            this.Controls.Add(this.listView_PFPList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main_Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_Form_FormClosing);
            this.Load += new System.EventHandler(this.Main_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listView_PFPList;
        private System.Windows.Forms.ColumnHeader num;
        private System.Windows.Forms.ColumnHeader file;
        private System.Windows.Forms.ColumnHeader fileOffset;
        private System.Windows.Forms.ColumnHeader fileActualSize;
        private System.Windows.Forms.Button btn_OpenPFP;
        private System.Windows.Forms.OpenFileDialog openFileDialog_OpenPFP;
        private System.Windows.Forms.Button btn_ExtractSingle;
        private System.Windows.Forms.Button btn_ExtractAll;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_ExtractSingle;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_ExtractAll;
        private System.Windows.Forms.ProgressBar progressBar_Extracted;
        private System.Windows.Forms.Label label_ExtractState;
        private System.Windows.Forms.TextBox textBox_ExtractAllOutDir;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.Label label_ExtractAll;
        private System.Windows.Forms.Label label_progress;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.Button button_About;
        private System.Windows.Forms.Button btn_Help;
    }
}

