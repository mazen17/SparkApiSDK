namespace SparkWinFormsGUI
{
    partial class SecurityMarketView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMarketState = new System.Windows.Forms.Label();
            this.lblMarketTime = new System.Windows.Forms.Label();
            this.lblMarketStateHeader = new System.Windows.Forms.Label();
            this.lblMarketTimeHeader = new System.Windows.Forms.Label();
            this.gbTrades = new System.Windows.Forms.GroupBox();
            this.lbTrades = new System.Windows.Forms.ListBox();
            this.gbDepth = new System.Windows.Forms.GroupBox();
            this.lblDepthView = new System.Windows.Forms.Label();
            this.gbTrades.SuspendLayout();
            this.gbDepth.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMarketState
            // 
            this.lblMarketState.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarketState.Location = new System.Drawing.Point(326, 4);
            this.lblMarketState.Name = "lblMarketState";
            this.lblMarketState.Size = new System.Drawing.Size(200, 14);
            this.lblMarketState.TabIndex = 21;
            this.lblMarketState.Text = "N/A";
            // 
            // lblMarketTime
            // 
            this.lblMarketTime.AutoSize = true;
            this.lblMarketTime.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarketTime.Location = new System.Drawing.Point(86, 4);
            this.lblMarketTime.Name = "lblMarketTime";
            this.lblMarketTime.Size = new System.Drawing.Size(28, 14);
            this.lblMarketTime.TabIndex = 18;
            this.lblMarketTime.Text = "N/A";
            // 
            // lblMarketStateHeader
            // 
            this.lblMarketStateHeader.AutoSize = true;
            this.lblMarketStateHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarketStateHeader.Location = new System.Drawing.Point(243, 4);
            this.lblMarketStateHeader.Name = "lblMarketStateHeader";
            this.lblMarketStateHeader.Size = new System.Drawing.Size(77, 14);
            this.lblMarketStateHeader.TabIndex = 20;
            this.lblMarketStateHeader.Text = "Market State";
            // 
            // lblMarketTimeHeader
            // 
            this.lblMarketTimeHeader.AutoSize = true;
            this.lblMarketTimeHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarketTimeHeader.Location = new System.Drawing.Point(3, 4);
            this.lblMarketTimeHeader.Name = "lblMarketTimeHeader";
            this.lblMarketTimeHeader.Size = new System.Drawing.Size(64, 14);
            this.lblMarketTimeHeader.TabIndex = 19;
            this.lblMarketTimeHeader.Text = "Last Event";
            // 
            // gbTrades
            // 
            this.gbTrades.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTrades.Controls.Add(this.lbTrades);
            this.gbTrades.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTrades.Location = new System.Drawing.Point(471, 32);
            this.gbTrades.Name = "gbTrades";
            this.gbTrades.Size = new System.Drawing.Size(341, 383);
            this.gbTrades.TabIndex = 17;
            this.gbTrades.TabStop = false;
            this.gbTrades.Text = "Trades";
            // 
            // lbTrades
            // 
            this.lbTrades.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTrades.BackColor = System.Drawing.SystemColors.Control;
            this.lbTrades.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbTrades.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTrades.FormattingEnabled = true;
            this.lbTrades.ItemHeight = 14;
            this.lbTrades.Location = new System.Drawing.Point(10, 28);
            this.lbTrades.Name = "lbTrades";
            this.lbTrades.Size = new System.Drawing.Size(316, 336);
            this.lbTrades.TabIndex = 8;
            // 
            // gbDepth
            // 
            this.gbDepth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbDepth.Controls.Add(this.lblDepthView);
            this.gbDepth.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDepth.Location = new System.Drawing.Point(3, 32);
            this.gbDepth.Name = "gbDepth";
            this.gbDepth.Size = new System.Drawing.Size(462, 383);
            this.gbDepth.TabIndex = 16;
            this.gbDepth.TabStop = false;
            this.gbDepth.Text = "Depth";
            // 
            // lblDepthView
            // 
            this.lblDepthView.AutoSize = true;
            this.lblDepthView.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepthView.Location = new System.Drawing.Point(6, 30);
            this.lblDepthView.Name = "lblDepthView";
            this.lblDepthView.Size = new System.Drawing.Size(147, 14);
            this.lblDepthView.TabIndex = 9;
            this.lblDepthView.Text = "<No Depth Available>";
            this.lblDepthView.DoubleClick += new System.EventHandler(lblDepthView_DoubleClick);
            // 
            // SecurityMarketView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblMarketState);
            this.Controls.Add(this.lblMarketTime);
            this.Controls.Add(this.lblMarketStateHeader);
            this.Controls.Add(this.lblMarketTimeHeader);
            this.Controls.Add(this.gbTrades);
            this.Controls.Add(this.gbDepth);
            this.Name = "SecurityMarketView";
            this.Size = new System.Drawing.Size(815, 418);
            this.gbTrades.ResumeLayout(false);
            this.gbDepth.ResumeLayout(false);
            this.gbDepth.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMarketState;
        private System.Windows.Forms.Label lblMarketTime;
        private System.Windows.Forms.Label lblMarketStateHeader;
        private System.Windows.Forms.Label lblMarketTimeHeader;
        private System.Windows.Forms.GroupBox gbTrades;
        private System.Windows.Forms.ListBox lbTrades;
        private System.Windows.Forms.GroupBox gbDepth;
        private System.Windows.Forms.Label lblDepthView;

    }
}
