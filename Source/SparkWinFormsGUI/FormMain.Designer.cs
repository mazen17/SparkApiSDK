namespace SparkWinFormsGUI
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securityLiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securityReplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.securityToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(279, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "mnuMain";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // securityToolStripMenuItem
            // 
            this.securityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.securityLiveToolStripMenuItem,
            this.securityReplayToolStripMenuItem,
            this.exportToFileToolStripMenuItem});
            this.securityToolStripMenuItem.Name = "securityToolStripMenuItem";
            this.securityToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.securityToolStripMenuItem.Text = "Security";
            // 
            // securityLiveToolStripMenuItem
            // 
            this.securityLiveToolStripMenuItem.Name = "securityLiveToolStripMenuItem";
            this.securityLiveToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.securityLiveToolStripMenuItem.Text = "Live Market...";
            this.securityLiveToolStripMenuItem.Click += new System.EventHandler(this.securityLiveToolStripMenuItem_Click);
            // 
            // securityReplayToolStripMenuItem
            // 
            this.securityReplayToolStripMenuItem.Name = "securityReplayToolStripMenuItem";
            this.securityReplayToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.securityReplayToolStripMenuItem.Text = "Replay...";
            this.securityReplayToolStripMenuItem.Click += new System.EventHandler(this.securityReplayToolStripMenuItem_Click);
            // 
            // exportToFileToolStripMenuItem
            // 
            this.exportToFileToolStripMenuItem.Name = "exportToFileToolStripMenuItem";
            this.exportToFileToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.exportToFileToolStripMenuItem.Text = "Export To File...";
            this.exportToFileToolStripMenuItem.Click += new System.EventHandler(this.exportToFileToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 113);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spark API Example";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem securityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem securityLiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem securityReplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToFileToolStripMenuItem;
    }
}