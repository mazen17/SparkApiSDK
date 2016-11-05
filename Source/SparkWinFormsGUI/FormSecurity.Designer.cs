namespace SparkWinFormsGUI
{
    partial class FormSecurity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSecurity));
            this.lblSymbolHeader = new System.Windows.Forms.Label();
            this.txtSymbol = new System.Windows.Forms.TextBox();
            this.cbExchange = new System.Windows.Forms.ComboBox();
            this.securityMarketView = new SparkWinFormsGUI.SecurityMarketView();
            this.SuspendLayout();
            // 
            // lblSymbolHeader
            // 
            this.lblSymbolHeader.AutoSize = true;
            this.lblSymbolHeader.Location = new System.Drawing.Point(16, 10);
            this.lblSymbolHeader.Name = "lblSymbolHeader";
            this.lblSymbolHeader.Size = new System.Drawing.Size(41, 13);
            this.lblSymbolHeader.TabIndex = 0;
            this.lblSymbolHeader.Text = "Symbol";
            // 
            // txtSymbol
            // 
            this.txtSymbol.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSymbol.Location = new System.Drawing.Point(63, 7);
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Size = new System.Drawing.Size(46, 20);
            this.txtSymbol.TabIndex = 1;
            this.txtSymbol.TextChanged += new System.EventHandler(this.txtSymbol_TextChanged);
            // 
            // cbExchange
            // 
            this.cbExchange.FormattingEnabled = true;
            this.cbExchange.Items.AddRange(new object[] {
            "ASX",
            "CXA"});
            this.cbExchange.Location = new System.Drawing.Point(109, 7);
            this.cbExchange.Name = "cbExchange";
            this.cbExchange.Size = new System.Drawing.Size(52, 21);
            this.cbExchange.TabIndex = 2;
            this.cbExchange.SelectedIndexChanged += new System.EventHandler(this.cbExchange_SelectedIndexChanged);
            // 
            // securityMarketView
            // 
            this.securityMarketView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.securityMarketView.DepthLevels = 20;
            this.securityMarketView.DisplayAggregateDepth = true;
            this.securityMarketView.EnableSynchronisation = true;
            this.securityMarketView.Location = new System.Drawing.Point(14, 39);
            this.securityMarketView.Name = "securityMarketView";
            this.securityMarketView.Security = null;
            this.securityMarketView.Size = new System.Drawing.Size(779, 397);
            this.securityMarketView.TabIndex = 0;
            // 
            // FormSecurity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 448);
            this.Controls.Add(this.cbExchange);
            this.Controls.Add(this.txtSymbol);
            this.Controls.Add(this.lblSymbolHeader);
            this.Controls.Add(this.securityMarketView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSecurity";
            this.Text = "Security";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormSecurity_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SecurityMarketView securityMarketView;
        private System.Windows.Forms.Label lblSymbolHeader;
        private System.Windows.Forms.TextBox txtSymbol;
        private System.Windows.Forms.ComboBox cbExchange;
    }
}