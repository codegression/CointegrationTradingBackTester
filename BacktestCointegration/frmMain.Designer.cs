namespace BacktestCointegration
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.singleRunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SingleRunRunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SingleRunloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optimizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptimizationRunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptimizationloadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.megaOptimizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MegaOptimizationRunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MegaOptimizationloadToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.MegaOptimizationloadAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.individualChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDataFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.source1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.source2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cboResponseV = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBasket = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cboTimeFrame = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBandwidth = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtWindow = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMaxOpenPositions = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLeverage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtInitialDeposit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTransactionCost = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmdBackTest = new System.Windows.Forms.Button();
            this.txtSystems = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(261, 109);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(176, 144);
            this.textBox1.TabIndex = 12;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.singleRunToolStripMenuItem,
            this.optimizationToolStripMenuItem,
            this.megaOptimizationToolStripMenuItem,
            this.drawToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(468, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // singleRunToolStripMenuItem
            // 
            this.singleRunToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SingleRunRunToolStripMenuItem,
            this.SingleRunloadToolStripMenuItem});
            this.singleRunToolStripMenuItem.Name = "singleRunToolStripMenuItem";
            this.singleRunToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.singleRunToolStripMenuItem.Text = "SingleRun";
            // 
            // SingleRunRunToolStripMenuItem
            // 
            this.SingleRunRunToolStripMenuItem.Name = "SingleRunRunToolStripMenuItem";
            this.SingleRunRunToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.SingleRunRunToolStripMenuItem.Text = "Run";
            this.SingleRunRunToolStripMenuItem.Click += new System.EventHandler(this.SingleRunrunToolStripMenuItem_Click);
            // 
            // SingleRunloadToolStripMenuItem
            // 
            this.SingleRunloadToolStripMenuItem.Name = "SingleRunloadToolStripMenuItem";
            this.SingleRunloadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.SingleRunloadToolStripMenuItem.Text = "Load";
            this.SingleRunloadToolStripMenuItem.Click += new System.EventHandler(this.SingleRunloadToolStripMenuItem_Click);
            // 
            // optimizationToolStripMenuItem
            // 
            this.optimizationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OptimizationRunToolStripMenuItem,
            this.OptimizationloadToolStripMenuItem1});
            this.optimizationToolStripMenuItem.Name = "optimizationToolStripMenuItem";
            this.optimizationToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.optimizationToolStripMenuItem.Text = "Optimization";
            // 
            // OptimizationRunToolStripMenuItem
            // 
            this.OptimizationRunToolStripMenuItem.Name = "OptimizationRunToolStripMenuItem";
            this.OptimizationRunToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.OptimizationRunToolStripMenuItem.Text = "Run";
            this.OptimizationRunToolStripMenuItem.Click += new System.EventHandler(this.OptimizationRunToolStripMenuItem1_Click);
            // 
            // OptimizationloadToolStripMenuItem1
            // 
            this.OptimizationloadToolStripMenuItem1.Name = "OptimizationloadToolStripMenuItem1";
            this.OptimizationloadToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.OptimizationloadToolStripMenuItem1.Text = "Load";
            this.OptimizationloadToolStripMenuItem1.Click += new System.EventHandler(this.OptimizationloadToolStripMenuItem1_Click);
            // 
            // megaOptimizationToolStripMenuItem
            // 
            this.megaOptimizationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MegaOptimizationRunToolStripMenuItem,
            this.MegaOptimizationloadToolStripMenuItem2,
            this.MegaOptimizationloadAllToolStripMenuItem});
            this.megaOptimizationToolStripMenuItem.Name = "megaOptimizationToolStripMenuItem";
            this.megaOptimizationToolStripMenuItem.Size = new System.Drawing.Size(119, 20);
            this.megaOptimizationToolStripMenuItem.Text = "Mega optimization";
            // 
            // MegaOptimizationRunToolStripMenuItem
            // 
            this.MegaOptimizationRunToolStripMenuItem.Name = "MegaOptimizationRunToolStripMenuItem";
            this.MegaOptimizationRunToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.MegaOptimizationRunToolStripMenuItem.Text = "Run";
            this.MegaOptimizationRunToolStripMenuItem.Click += new System.EventHandler(this.MegaOptimizationRunToolStripMenuItem_Click);
            // 
            // MegaOptimizationloadToolStripMenuItem2
            // 
            this.MegaOptimizationloadToolStripMenuItem2.Name = "MegaOptimizationloadToolStripMenuItem2";
            this.MegaOptimizationloadToolStripMenuItem2.Size = new System.Drawing.Size(134, 22);
            this.MegaOptimizationloadToolStripMenuItem2.Text = "Load single";
            // 
            // MegaOptimizationloadAllToolStripMenuItem
            // 
            this.MegaOptimizationloadAllToolStripMenuItem.Name = "MegaOptimizationloadAllToolStripMenuItem";
            this.MegaOptimizationloadAllToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.MegaOptimizationloadAllToolStripMenuItem.Text = "Load all";
            this.MegaOptimizationloadAllToolStripMenuItem.Click += new System.EventHandler(this.MegaOptimizationloadAllToolStripMenuItem_Click);
            // 
            // drawToolStripMenuItem
            // 
            this.drawToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.individualChartToolStripMenuItem});
            this.drawToolStripMenuItem.Name = "drawToolStripMenuItem";
            this.drawToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.drawToolStripMenuItem.Text = "Draw";
            // 
            // individualChartToolStripMenuItem
            // 
            this.individualChartToolStripMenuItem.Name = "individualChartToolStripMenuItem";
            this.individualChartToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.individualChartToolStripMenuItem.Text = "Individual chart";
            this.individualChartToolStripMenuItem.Click += new System.EventHandler(this.individualChartToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setDataFolderToolStripMenuItem,
            this.dataSourceToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // setDataFolderToolStripMenuItem
            // 
            this.setDataFolderToolStripMenuItem.Name = "setDataFolderToolStripMenuItem";
            this.setDataFolderToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.setDataFolderToolStripMenuItem.Text = "Set data folder";
            this.setDataFolderToolStripMenuItem.Click += new System.EventHandler(this.setDataFolderToolStripMenuItem_Click);
            // 
            // dataSourceToolStripMenuItem
            // 
            this.dataSourceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.source1ToolStripMenuItem,
            this.source2ToolStripMenuItem});
            this.dataSourceToolStripMenuItem.Name = "dataSourceToolStripMenuItem";
            this.dataSourceToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.dataSourceToolStripMenuItem.Text = "Data source";
            // 
            // source1ToolStripMenuItem
            // 
            this.source1ToolStripMenuItem.Checked = true;
            this.source1ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.source1ToolStripMenuItem.Name = "source1ToolStripMenuItem";
            this.source1ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.source1ToolStripMenuItem.Text = "Source 1";
            this.source1ToolStripMenuItem.Click += new System.EventHandler(this.source1ToolStripMenuItem_Click);
            // 
            // source2ToolStripMenuItem
            // 
            this.source2ToolStripMenuItem.Name = "source2ToolStripMenuItem";
            this.source2ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.source2ToolStripMenuItem.Text = "Source 2";
            this.source2ToolStripMenuItem.Click += new System.EventHandler(this.source2ToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(468, 308);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(460, 282);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Pair test";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cboResponseV);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBasket);
            this.groupBox1.Location = new System.Drawing.Point(8, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(443, 91);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Symbol basket";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(302, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 25);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // cboResponseV
            // 
            this.cboResponseV.FormattingEnabled = true;
            this.cboResponseV.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cboResponseV.Location = new System.Drawing.Point(140, 54);
            this.cboResponseV.Name = "cboResponseV";
            this.cboResponseV.Size = new System.Drawing.Size(49, 21);
            this.cboResponseV.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Response variable index:";
            // 
            // txtBasket
            // 
            this.txtBasket.Location = new System.Drawing.Point(6, 25);
            this.txtBasket.Name = "txtBasket";
            this.txtBasket.Size = new System.Drawing.Size(430, 20);
            this.txtBasket.TabIndex = 0;
            this.txtBasket.Text = "EURUSD,GBPUSD,USDCHF";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cboTimeFrame);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtBandwidth);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtWindow);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(8, 114);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(217, 152);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Parameters";
            // 
            // cboTimeFrame
            // 
            this.cboTimeFrame.FormattingEnabled = true;
            this.cboTimeFrame.Items.AddRange(new object[] {
            "m5",
            "m15",
            "m30",
            "H1",
            "H2",
            "H3",
            "H4",
            "H6",
            "H8",
            "D1"});
            this.cboTimeFrame.Location = new System.Drawing.Point(109, 96);
            this.cboTimeFrame.Name = "cboTimeFrame";
            this.cboTimeFrame.Size = new System.Drawing.Size(53, 21);
            this.cboTimeFrame.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Time frame:";
            // 
            // txtBandwidth
            // 
            this.txtBandwidth.Location = new System.Drawing.Point(109, 58);
            this.txtBandwidth.Name = "txtBandwidth";
            this.txtBandwidth.Size = new System.Drawing.Size(37, 20);
            this.txtBandwidth.TabIndex = 5;
            this.txtBandwidth.Text = "2.5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Bandwidth (sd)";
            // 
            // txtWindow
            // 
            this.txtWindow.Location = new System.Drawing.Point(109, 23);
            this.txtWindow.Name = "txtWindow";
            this.txtWindow.Size = new System.Drawing.Size(37, 20);
            this.txtWindow.TabIndex = 3;
            this.txtWindow.Text = "10";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Window size (n):";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMaxOpenPositions);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtLeverage);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtInitialDeposit);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtTransactionCost);
            this.groupBox2.Location = new System.Drawing.Point(235, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(217, 152);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Trading system";
            // 
            // txtMaxOpenPositions
            // 
            this.txtMaxOpenPositions.Location = new System.Drawing.Point(152, 105);
            this.txtMaxOpenPositions.Name = "txtMaxOpenPositions";
            this.txtMaxOpenPositions.Size = new System.Drawing.Size(37, 20);
            this.txtMaxOpenPositions.TabIndex = 7;
            this.txtMaxOpenPositions.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Max open positions:";
            // 
            // txtLeverage
            // 
            this.txtLeverage.Location = new System.Drawing.Point(152, 79);
            this.txtLeverage.Name = "txtLeverage";
            this.txtLeverage.Size = new System.Drawing.Size(37, 20);
            this.txtLeverage.TabIndex = 5;
            this.txtLeverage.Text = "-40";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Leverage:";
            // 
            // txtInitialDeposit
            // 
            this.txtInitialDeposit.Location = new System.Drawing.Point(152, 52);
            this.txtInitialDeposit.Name = "txtInitialDeposit";
            this.txtInitialDeposit.Size = new System.Drawing.Size(37, 20);
            this.txtInitialDeposit.TabIndex = 3;
            this.txtInitialDeposit.Text = "2000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Initial deposit ($):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Transaction cost (pip):";
            // 
            // txtTransactionCost
            // 
            this.txtTransactionCost.Location = new System.Drawing.Point(152, 23);
            this.txtTransactionCost.Name = "txtTransactionCost";
            this.txtTransactionCost.Size = new System.Drawing.Size(37, 20);
            this.txtTransactionCost.TabIndex = 0;
            this.txtTransactionCost.Text = "0";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmdBackTest);
            this.tabPage1.Controls.Add(this.txtSystems);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(460, 282);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Portfolio optimization";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cmdBackTest
            // 
            this.cmdBackTest.Location = new System.Drawing.Point(181, 224);
            this.cmdBackTest.Name = "cmdBackTest";
            this.cmdBackTest.Size = new System.Drawing.Size(115, 25);
            this.cmdBackTest.TabIndex = 1;
            this.cmdBackTest.Text = "Back test";
            this.cmdBackTest.UseVisualStyleBackColor = true;
            this.cmdBackTest.Click += new System.EventHandler(this.cmdBackTest_Click);
            // 
            // txtSystems
            // 
            this.txtSystems.Location = new System.Drawing.Point(3, 6);
            this.txtSystems.Multiline = true;
            this.txtSystems.Name = "txtSystems";
            this.txtSystems.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSystems.Size = new System.Drawing.Size(454, 199);
            this.txtSystems.TabIndex = 0;
            this.txtSystems.Text = resources.GetString("txtSystems.Text");
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 332);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Back test - Cointegration";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem singleRunToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SingleRunRunToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SingleRunloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optimizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OptimizationRunToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OptimizationloadToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem megaOptimizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MegaOptimizationRunToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MegaOptimizationloadToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem drawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem individualChartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MegaOptimizationloadAllToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtBandwidth;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtWindow;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtLeverage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtInitialDeposit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTransactionCost;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button cmdBackTest;
        private System.Windows.Forms.TextBox txtSystems;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDataFolderToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBasket;
        private System.Windows.Forms.ComboBox cboTimeFrame;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboResponseV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaxOpenPositions;
        private System.Windows.Forms.ToolStripMenuItem dataSourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem source1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem source2ToolStripMenuItem;
    }
}

