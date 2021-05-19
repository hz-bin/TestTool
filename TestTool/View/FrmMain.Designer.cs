namespace ProcessTestClient
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.ribbon1 = new System.Windows.Forms.Ribbon();
            this.orbExit = new System.Windows.Forms.RibbonOrbOptionButton();
            this.rTabFunc = new System.Windows.Forms.RibbonTab();
            this.rpOperation = new System.Windows.Forms.RibbonPanel();
            this.rbtnExecTestCase = new System.Windows.Forms.RibbonButton();
            this.rbtnStopTestCase = new System.Windows.Forms.RibbonButton();
            this.rbtnReloadTestCase = new System.Windows.Forms.RibbonButton();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.tplLeft = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotalNum = new System.Windows.Forms.Label();
            this.btnFilterByID = new System.Windows.Forms.Button();
            this.txbTestCaseID = new System.Windows.Forms.TextBox();
            this.lblCaseID = new System.Windows.Forms.Label();
            this.lblFailNum = new System.Windows.Forms.Label();
            this.ckbSelectAll = new System.Windows.Forms.CheckBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblSuccNum = new System.Windows.Forms.Label();
            this.lvwTestCase = new System.Windows.Forms.ListView();
            this.HeadId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HeadTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HeadResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlpMain.SuspendLayout();
            this.tplLeft.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon1
            // 
            this.ribbon1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.OptionItems.Add(this.orbExit);
            this.ribbon1.OrbDropDown.Size = new System.Drawing.Size(527, 72);
            this.ribbon1.OrbDropDown.TabIndex = 0;
            this.ribbon1.OrbImage = null;
            this.ribbon1.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2013;
            this.ribbon1.OrbText = "文件";
            // 
            // 
            // 
            this.ribbon1.QuickAcessToolbar.Visible = false;
            this.ribbon1.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9F);
            this.ribbon1.Size = new System.Drawing.Size(1229, 122);
            this.ribbon1.TabIndex = 3;
            this.ribbon1.Tabs.Add(this.rTabFunc);
            this.ribbon1.TabsMargin = new System.Windows.Forms.Padding(12, 0, 20, 0);
            this.ribbon1.Text = "ribbon1";
            this.ribbon1.ThemeColor = System.Windows.Forms.RibbonTheme.Blue;
            // 
            // orbExit
            // 
            this.orbExit.Image = ((System.Drawing.Image)(resources.GetObject("orbExit.Image")));
            this.orbExit.SmallImage = ((System.Drawing.Image)(resources.GetObject("orbExit.SmallImage")));
            this.orbExit.Text = "退出";
            // 
            // rTabFunc
            // 
            this.rTabFunc.Panels.Add(this.rpOperation);
            this.rTabFunc.Text = "功能";
            // 
            // rpOperation
            // 
            this.rpOperation.ButtonMoreEnabled = false;
            this.rpOperation.ButtonMoreVisible = false;
            this.rpOperation.Items.Add(this.rbtnExecTestCase);
            this.rpOperation.Items.Add(this.rbtnStopTestCase);
            this.rpOperation.Items.Add(this.rbtnReloadTestCase);
            this.rpOperation.Text = "";
            // 
            // rbtnExecTestCase
            // 
            this.rbtnExecTestCase.Image = global::ProcessTestClient.Properties.Resources.Start48;
            this.rbtnExecTestCase.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbtnExecTestCase.SmallImage")));
            this.rbtnExecTestCase.Text = "执行测试用例";
            this.rbtnExecTestCase.Click += new System.EventHandler(this.rbtnExecTestCase_Click);
            // 
            // rbtnStopTestCase
            // 
            this.rbtnStopTestCase.Image = global::ProcessTestClient.Properties.Resources.Stop48;
            this.rbtnStopTestCase.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbtnStopTestCase.SmallImage")));
            this.rbtnStopTestCase.Text = "停止测试用例";
            this.rbtnStopTestCase.Click += new System.EventHandler(this.rbtnStopTestCase_Click);
            // 
            // rbtnReloadTestCase
            // 
            this.rbtnReloadTestCase.Image = global::ProcessTestClient.Properties.Resources.Reload48;
            this.rbtnReloadTestCase.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbtnReloadTestCase.SmallImage")));
            this.rbtnReloadTestCase.Text = "重新加载用例";
            this.rbtnReloadTestCase.Click += new System.EventHandler(this.rbtnReloadTestCase_Click);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 450F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.Controls.Add(this.rtbLog, 1, 0);
            this.tlpMain.Controls.Add(this.tplLeft, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 122);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1229, 637);
            this.tlpMain.TabIndex = 4;
            // 
            // rtbLog
            // 
            this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLog.Location = new System.Drawing.Point(453, 3);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(773, 631);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            // 
            // tplLeft
            // 
            this.tplLeft.ColumnCount = 1;
            this.tplLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplLeft.Controls.Add(this.panel1, 0, 0);
            this.tplLeft.Controls.Add(this.lvwTestCase, 0, 1);
            this.tplLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplLeft.Location = new System.Drawing.Point(3, 3);
            this.tplLeft.Name = "tplLeft";
            this.tplLeft.RowCount = 2;
            this.tplLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tplLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplLeft.Size = new System.Drawing.Size(444, 631);
            this.tplLeft.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblTotalNum);
            this.panel1.Controls.Add(this.btnFilterByID);
            this.panel1.Controls.Add(this.txbTestCaseID);
            this.panel1.Controls.Add(this.lblCaseID);
            this.panel1.Controls.Add(this.lblFailNum);
            this.panel1.Controls.Add(this.ckbSelectAll);
            this.panel1.Controls.Add(this.lblTime);
            this.panel1.Controls.Add(this.lblSuccNum);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(438, 44);
            this.panel1.TabIndex = 1;
            // 
            // lblTotalNum
            // 
            this.lblTotalNum.AutoSize = true;
            this.lblTotalNum.Location = new System.Drawing.Point(88, 29);
            this.lblTotalNum.Name = "lblTotalNum";
            this.lblTotalNum.Size = new System.Drawing.Size(53, 12);
            this.lblTotalNum.TabIndex = 36;
            this.lblTotalNum.Text = "总数：--";
            // 
            // btnFilterByID
            // 
            this.btnFilterByID.Location = new System.Drawing.Point(240, 0);
            this.btnFilterByID.Name = "btnFilterByID";
            this.btnFilterByID.Size = new System.Drawing.Size(101, 23);
            this.btnFilterByID.TabIndex = 35;
            this.btnFilterByID.Text = "按ID过滤";
            this.btnFilterByID.UseVisualStyleBackColor = true;
            this.btnFilterByID.Click += new System.EventHandler(this.btnFilterByID_Click);
            // 
            // txbTestCaseID
            // 
            this.txbTestCaseID.Location = new System.Drawing.Point(63, 2);
            this.txbTestCaseID.Name = "txbTestCaseID";
            this.txbTestCaseID.Size = new System.Drawing.Size(132, 21);
            this.txbTestCaseID.TabIndex = 34;
            this.txbTestCaseID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbTestCaseID_KeyDown);
            // 
            // lblCaseID
            // 
            this.lblCaseID.AutoSize = true;
            this.lblCaseID.Location = new System.Drawing.Point(4, 5);
            this.lblCaseID.Name = "lblCaseID";
            this.lblCaseID.Size = new System.Drawing.Size(53, 12);
            this.lblCaseID.TabIndex = 33;
            this.lblCaseID.Text = "用例ID：";
            // 
            // lblFailNum
            // 
            this.lblFailNum.AutoSize = true;
            this.lblFailNum.Location = new System.Drawing.Point(262, 29);
            this.lblFailNum.Name = "lblFailNum";
            this.lblFailNum.Size = new System.Drawing.Size(53, 12);
            this.lblFailNum.TabIndex = 31;
            this.lblFailNum.Text = "失败：--";
            // 
            // ckbSelectAll
            // 
            this.ckbSelectAll.AutoSize = true;
            this.ckbSelectAll.Location = new System.Drawing.Point(6, 28);
            this.ckbSelectAll.Name = "ckbSelectAll";
            this.ckbSelectAll.Size = new System.Drawing.Size(48, 16);
            this.ckbSelectAll.TabIndex = 29;
            this.ckbSelectAll.Text = "全选";
            this.ckbSelectAll.UseVisualStyleBackColor = true;
            this.ckbSelectAll.CheckedChanged += new System.EventHandler(this.ckbSelectAll_CheckedChanged);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(349, 29);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(71, 12);
            this.lblTime.TabIndex = 32;
            this.lblTime.Text = "时间：00:00";
            // 
            // lblSuccNum
            // 
            this.lblSuccNum.AutoSize = true;
            this.lblSuccNum.Location = new System.Drawing.Point(175, 29);
            this.lblSuccNum.Name = "lblSuccNum";
            this.lblSuccNum.Size = new System.Drawing.Size(53, 12);
            this.lblSuccNum.TabIndex = 30;
            this.lblSuccNum.Text = "成功：--";
            // 
            // lvwTestCase
            // 
            this.lvwTestCase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwTestCase.CheckBoxes = true;
            this.lvwTestCase.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HeadId,
            this.HeadTitle,
            this.HeadResult});
            this.lvwTestCase.FullRowSelect = true;
            this.lvwTestCase.GridLines = true;
            this.lvwTestCase.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwTestCase.Location = new System.Drawing.Point(3, 53);
            this.lvwTestCase.MultiSelect = false;
            this.lvwTestCase.Name = "lvwTestCase";
            this.lvwTestCase.ShowItemToolTips = true;
            this.lvwTestCase.Size = new System.Drawing.Size(438, 575);
            this.lvwTestCase.TabIndex = 0;
            this.lvwTestCase.UseCompatibleStateImageBehavior = false;
            this.lvwTestCase.View = System.Windows.Forms.View.Details;
            this.lvwTestCase.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwTestCase_MouseDoubleClick);
            // 
            // HeadId
            // 
            this.HeadId.Text = "ID";
            this.HeadId.Width = 120;
            // 
            // HeadTitle
            // 
            this.HeadTitle.Text = "测试用例";
            this.HeadTitle.Width = 250;
            // 
            // HeadResult
            // 
            this.HeadResult.Text = "结果";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1229, 759);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.ribbon1);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "测试工具";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tlpMain.ResumeLayout(false);
            this.tplLeft.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Ribbon ribbon1;
        private System.Windows.Forms.RibbonTab rTabFunc;
        private System.Windows.Forms.RibbonPanel rpOperation;
        private System.Windows.Forms.RibbonOrbOptionButton orbExit;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.TableLayoutPanel tplLeft;
        private System.Windows.Forms.ListView lvwTestCase;
        private System.Windows.Forms.ColumnHeader HeadId;
        private System.Windows.Forms.ColumnHeader HeadTitle;
        private System.Windows.Forms.ColumnHeader HeadResult;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RibbonButton rbtnExecTestCase;
        private System.Windows.Forms.RibbonButton rbtnStopTestCase;
        private System.Windows.Forms.RibbonButton rbtnReloadTestCase;
        private System.Windows.Forms.Label lblTotalNum;
        private System.Windows.Forms.Button btnFilterByID;
        private System.Windows.Forms.TextBox txbTestCaseID;
        private System.Windows.Forms.Label lblCaseID;
        private System.Windows.Forms.Label lblFailNum;
        private System.Windows.Forms.CheckBox ckbSelectAll;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblSuccNum;
    }
}

