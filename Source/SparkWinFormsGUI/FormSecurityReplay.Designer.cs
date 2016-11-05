namespace SparkWinFormsGUI
{
    partial class FormSecurityReplay
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
            this.components = new System.ComponentModel.Container();
            this.btnReplay = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.fileBrowser = new System.Windows.Forms.OpenFileDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnNextEvent = new System.Windows.Forms.Button();
            this.ReplayToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.securityMarketView = new SparkWinFormsGUI.SecurityMarketView();
            this.SuspendLayout();
            // 
            // btnReplay
            // 
            this.btnReplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReplay.Location = new System.Drawing.Point(649, 9);
            this.btnReplay.Name = "btnReplay";
            this.btnReplay.Size = new System.Drawing.Size(75, 23);
            this.btnReplay.TabIndex = 1;
            this.btnReplay.Text = "Replay";
            this.ReplayToolTips.SetToolTip(this.btnReplay, "Start Replay");
            this.btnReplay.UseVisualStyleBackColor = true;
            this.btnReplay.Click += new System.EventHandler(this.btnReplay_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(12, 14);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(54, 13);
            this.lblFileName.TabIndex = 4;
            this.lblFileName.Text = "Event File";
            this.lblFileName.Click += new System.EventHandler(this.lblFileName_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.Location = new System.Drawing.Point(72, 11);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(537, 20);
            this.txtFileName.TabIndex = 5;
            this.txtFileName.Text = "SampleFiles\\BHP_Event_20120426.txt";
            this.txtFileName.TextChanged += new System.EventHandler(this.txtFileName_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(614, 9);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(31, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "...";
            this.ReplayToolTips.SetToolTip(this.btnBrowse, "Browse for event file");
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPause.Enabled = false;
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.Location = new System.Drawing.Point(726, 9);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(31, 23);
            this.btnPause.TabIndex = 11;
            this.btnPause.Text = "| |";
            this.ReplayToolTips.SetToolTip(this.btnPause, "Pause replay");
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnNextEvent
            // 
            this.btnNextEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextEvent.Enabled = false;
            this.btnNextEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextEvent.Location = new System.Drawing.Point(760, 9);
            this.btnNextEvent.Name = "btnNextEvent";
            this.btnNextEvent.Size = new System.Drawing.Size(31, 23);
            this.btnNextEvent.TabIndex = 12;
            this.btnNextEvent.Text = ">";
            this.ReplayToolTips.SetToolTip(this.btnNextEvent, "Step forward to next event");
            this.btnNextEvent.UseVisualStyleBackColor = true;
            this.btnNextEvent.Click += new System.EventHandler(this.btnNextEvent_Click);
            // 
            // securityMarketView
            // 
            this.securityMarketView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.securityMarketView.DepthLevels = 20;
            this.securityMarketView.DisplayAggregateDepth = true;
            this.securityMarketView.EnableSynchronisation = false;
            this.securityMarketView.Location = new System.Drawing.Point(15, 42);
            this.securityMarketView.Name = "securityMarketView";
            this.securityMarketView.Security = null;
            this.securityMarketView.Size = new System.Drawing.Size(776, 392);
            this.securityMarketView.TabIndex = 13;
            // 
            // FormSecurityReplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 446);
            this.Controls.Add(this.securityMarketView);
            this.Controls.Add(this.btnNextEvent);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.btnReplay);
            this.Name = "FormSecurityReplay";
            this.Text = "Market Replay";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormSecurityReplay_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReplay;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.OpenFileDialog fileBrowser;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnNextEvent;
        private System.Windows.Forms.ToolTip ReplayToolTips;
        private SecurityMarketView securityMarketView;
    }
}

