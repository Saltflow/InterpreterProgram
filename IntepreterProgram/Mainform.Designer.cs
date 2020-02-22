namespace IntepreterProgram
{
    partial class Mainform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainform));
            this.retLabel = new System.Windows.Forms.Label();
            this.MainTab = new System.Windows.Forms.TabControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toggleStepDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.RealtimeResultBox = new System.Windows.Forms.TextBox();
            this.contButton = new System.Windows.Forms.Button();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.ShowAnalyzeButton = new System.Windows.Forms.ToolStripButton();
            this.identiTableButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // retLabel
            // 
            this.retLabel.AutoSize = true;
            this.retLabel.Location = new System.Drawing.Point(72, 572);
            this.retLabel.Name = "retLabel";
            this.retLabel.Size = new System.Drawing.Size(0, 18);
            this.retLabel.TabIndex = 3;
            // 
            // MainTab
            // 
            this.MainTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTab.Location = new System.Drawing.Point(58, 46);
            this.MainTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MainTab.Name = "MainTab";
            this.MainTab.SelectedIndex = 0;
            this.MainTab.Size = new System.Drawing.Size(893, 545);
            this.MainTab.TabIndex = 4;
            this.MainTab.SizeChanged += new System.EventHandler(this.MainTab_SizeChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2,
            this.toolStripButton1,
            this.toolStripButton2,
            this.ShowAnalyzeButton,
            this.identiTableButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1234, 31);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.recentFileToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(76, 28);
            this.toolStripDropDownButton1.Text = "Open";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(186, 30);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // recentFileToolStripMenuItem
            // 
            this.recentFileToolStripMenuItem.Name = "recentFileToolStripMenuItem";
            this.recentFileToolStripMenuItem.Size = new System.Drawing.Size(186, 30);
            this.recentFileToolStripMenuItem.Text = "Recent File";
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleStepDebugToolStripMenuItem,
            this.compileToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(127, 28);
            this.toolStripDropDownButton2.Text = "Debugging";
            // 
            // toggleStepDebugToolStripMenuItem
            // 
            this.toggleStepDebugToolStripMenuItem.Name = "toggleStepDebugToolStripMenuItem";
            this.toggleStepDebugToolStripMenuItem.Size = new System.Drawing.Size(260, 30);
            this.toggleStepDebugToolStripMenuItem.Text = "Toggle Step Debug";
            this.toggleStepDebugToolStripMenuItem.Click += new System.EventHandler(this.toggleStepDebugToolStripMenuItem_Click);
            // 
            // compileToolStripMenuItem
            // 
            this.compileToolStripMenuItem.Name = "compileToolStripMenuItem";
            this.compileToolStripMenuItem.Size = new System.Drawing.Size(260, 30);
            this.compileToolStripMenuItem.Text = "Compile";
            this.compileToolStripMenuItem.Click += new System.EventHandler(this.compileToolStripMenuItem_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(100, 28);
            this.toolStripButton1.Text = "Interprete";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(169, 28);
            this.toolStripButton2.Text = "Show Parse Result";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // RealtimeResultBox
            // 
            this.RealtimeResultBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RealtimeResultBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.RealtimeResultBox.Location = new System.Drawing.Point(968, 46);
            this.RealtimeResultBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RealtimeResultBox.Multiline = true;
            this.RealtimeResultBox.Name = "RealtimeResultBox";
            this.RealtimeResultBox.ReadOnly = true;
            this.RealtimeResultBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RealtimeResultBox.Size = new System.Drawing.Size(229, 291);
            this.RealtimeResultBox.TabIndex = 6;
            // 
            // contButton
            // 
            this.contButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.contButton.Location = new System.Drawing.Point(968, 544);
            this.contButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.contButton.Name = "contButton";
            this.contButton.Size = new System.Drawing.Size(117, 44);
            this.contButton.TabIndex = 7;
            this.contButton.Text = "Continue";
            this.contButton.UseVisualStyleBackColor = true;
            this.contButton.Visible = false;
            this.contButton.Click += new System.EventHandler(this.contButton_Click);
            // 
            // inputTextBox
            // 
            this.inputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.inputTextBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.inputTextBox.Location = new System.Drawing.Point(968, 372);
            this.inputTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inputTextBox.Multiline = true;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(229, 152);
            this.inputTextBox.TabIndex = 8;
            this.inputTextBox.TextChanged += new System.EventHandler(this.inputTextBox_TextChanged);
            this.inputTextBox.Leave += new System.EventHandler(this.inputTextBox_Leave);
            // 
            // ShowAnalyzeButton
            // 
            this.ShowAnalyzeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ShowAnalyzeButton.Image = ((System.Drawing.Image)(resources.GetObject("ShowAnalyzeButton.Image")));
            this.ShowAnalyzeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ShowAnalyzeButton.Name = "ShowAnalyzeButton";
            this.ShowAnalyzeButton.Size = new System.Drawing.Size(191, 28);
            this.ShowAnalyzeButton.Text = "Show Analyze Result";
            this.ShowAnalyzeButton.Click += new System.EventHandler(this.ShowAnalyzeButton_Click);
            // 
            // identiTableButton
            // 
            this.identiTableButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.identiTableButton.Image = ((System.Drawing.Image)(resources.GetObject("identiTableButton.Image")));
            this.identiTableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.identiTableButton.Name = "identiTableButton";
            this.identiTableButton.Size = new System.Drawing.Size(195, 28);
            this.identiTableButton.Text = "Show Identifier Table";
            this.identiTableButton.Click += new System.EventHandler(this.identiTableButton_Click);
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 600);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.contButton);
            this.Controls.Add(this.RealtimeResultBox);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.MainTab);
            this.Controls.Add(this.retLabel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Mainform";
            this.Text = "Intepretor";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label retLabel;
        private System.Windows.Forms.TabControl MainTab;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem toggleStepDebugToolStripMenuItem;
        private System.Windows.Forms.TextBox RealtimeResultBox;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Button contButton;
        private System.Windows.Forms.ToolStripMenuItem compileToolStripMenuItem;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.ToolStripButton ShowAnalyzeButton;
        private System.Windows.Forms.ToolStripButton identiTableButton;
    }
}

