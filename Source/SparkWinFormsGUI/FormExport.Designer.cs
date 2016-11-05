namespace SparkWinFormsGUI
{
    partial class FormExport
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
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbExchange = new System.Windows.Forms.ComboBox();
            this.txtSymbol = new System.Windows.Forms.TextBox();
            this.lblSymbolHeader = new System.Windows.Forms.Label();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.lblOutputFolder = new System.Windows.Forms.Label();
            this.txtProgress = new System.Windows.Forms.TextBox();
            this.udDays = new System.Windows.Forms.NumericUpDown();
            this.lblDaysHeader = new System.Windows.Forms.Label();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.txtSymbolListFileName = new System.Windows.Forms.TextBox();
            this.lblSymbolListFileNameHeader = new System.Windows.Forms.Label();
            this.fileBrowser = new System.Windows.Forms.OpenFileDialog();
            this.lblExchangeHeader = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkIncludeToday = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.udDays)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(381, 388);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 13;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(462, 388);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbExchange
            // 
            this.cbExchange.FormattingEnabled = true;
            this.cbExchange.Items.AddRange(new object[] {
            "ASX",
            "CXA",
            "Both"});
            this.cbExchange.Location = new System.Drawing.Point(231, 73);
            this.cbExchange.Name = "cbExchange";
            this.cbExchange.Size = new System.Drawing.Size(52, 21);
            this.cbExchange.TabIndex = 8;
            this.cbExchange.Text = "Both";
            // 
            // txtSymbol
            // 
            this.txtSymbol.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSymbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSymbol.Location = new System.Drawing.Point(110, 73);
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Size = new System.Drawing.Size(46, 20);
            this.txtSymbol.TabIndex = 7;
            this.txtSymbol.TextChanged += new System.EventHandler(this.txtSymbol_TextChanged);
            // 
            // lblSymbolHeader
            // 
            this.lblSymbolHeader.AutoSize = true;
            this.lblSymbolHeader.Location = new System.Drawing.Point(12, 77);
            this.lblSymbolHeader.Name = "lblSymbolHeader";
            this.lblSymbolHeader.Size = new System.Drawing.Size(41, 13);
            this.lblSymbolHeader.TabIndex = 6;
            this.lblSymbolHeader.Text = "Symbol";
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFolder.Location = new System.Drawing.Point(507, 10);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(31, 23);
            this.btnBrowseFolder.TabIndex = 2;
            this.btnBrowseFolder.Text = "...";
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputFolder.Location = new System.Drawing.Point(110, 12);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(391, 20);
            this.txtOutputFolder.TabIndex = 1;
            this.txtOutputFolder.Text = "C:\\Temp\\SparkData";
            // 
            // lblOutputFolder
            // 
            this.lblOutputFolder.AutoSize = true;
            this.lblOutputFolder.Location = new System.Drawing.Point(12, 15);
            this.lblOutputFolder.Name = "lblOutputFolder";
            this.lblOutputFolder.Size = new System.Drawing.Size(95, 13);
            this.lblOutputFolder.TabIndex = 0;
            this.lblOutputFolder.Text = "Export Data Folder";
            // 
            // txtProgress
            // 
            this.txtProgress.AcceptsReturn = true;
            this.txtProgress.AcceptsTab = true;
            this.txtProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProgress.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtProgress.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProgress.Location = new System.Drawing.Point(13, 141);
            this.txtProgress.Multiline = true;
            this.txtProgress.Name = "txtProgress";
            this.txtProgress.ReadOnly = true;
            this.txtProgress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtProgress.Size = new System.Drawing.Size(524, 241);
            this.txtProgress.TabIndex = 11;
            // 
            // udDays
            // 
            this.udDays.Location = new System.Drawing.Point(110, 104);
            this.udDays.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udDays.Name = "udDays";
            this.udDays.Size = new System.Drawing.Size(44, 20);
            this.udDays.TabIndex = 10;
            this.udDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblDaysHeader
            // 
            this.lblDaysHeader.AutoSize = true;
            this.lblDaysHeader.Location = new System.Drawing.Point(12, 108);
            this.lblDaysHeader.Name = "lblDaysHeader";
            this.lblDaysHeader.Size = new System.Drawing.Size(31, 13);
            this.lblDaysHeader.TabIndex = 9;
            this.lblDaysHeader.Text = "Days";
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFile.Location = new System.Drawing.Point(506, 41);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(31, 23);
            this.btnBrowseFile.TabIndex = 5;
            this.btnBrowseFile.Text = "...";
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
            // 
            // txtSymbolListFileName
            // 
            this.txtSymbolListFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSymbolListFileName.Location = new System.Drawing.Point(110, 43);
            this.txtSymbolListFileName.Name = "txtSymbolListFileName";
            this.txtSymbolListFileName.Size = new System.Drawing.Size(390, 20);
            this.txtSymbolListFileName.TabIndex = 4;
            this.txtSymbolListFileName.TextChanged += new System.EventHandler(this.txtSymbolListFileName_TextChanged);
            // 
            // lblSymbolListFileNameHeader
            // 
            this.lblSymbolListFileNameHeader.AutoSize = true;
            this.lblSymbolListFileNameHeader.Location = new System.Drawing.Point(12, 46);
            this.lblSymbolListFileNameHeader.Name = "lblSymbolListFileNameHeader";
            this.lblSymbolListFileNameHeader.Size = new System.Drawing.Size(79, 13);
            this.lblSymbolListFileNameHeader.TabIndex = 3;
            this.lblSymbolListFileNameHeader.Text = "Symbol List File";
            // 
            // lblExchangeHeader
            // 
            this.lblExchangeHeader.AutoSize = true;
            this.lblExchangeHeader.Location = new System.Drawing.Point(170, 76);
            this.lblExchangeHeader.Name = "lblExchangeHeader";
            this.lblExchangeHeader.Size = new System.Drawing.Size(55, 13);
            this.lblExchangeHeader.TabIndex = 14;
            this.lblExchangeHeader.Text = "Exchange";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(300, 388);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkIncludeToday
            // 
            this.chkIncludeToday.AutoSize = true;
            this.chkIncludeToday.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIncludeToday.Location = new System.Drawing.Point(169, 107);
            this.chkIncludeToday.Name = "chkIncludeToday";
            this.chkIncludeToday.Size = new System.Drawing.Size(120, 17);
            this.chkIncludeToday.TabIndex = 16;
            this.chkIncludeToday.Text = "Include Current Day";
            this.chkIncludeToday.UseVisualStyleBackColor = true;
            // 
            // FormExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(549, 423);
            this.Controls.Add(this.chkIncludeToday);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblExchangeHeader);
            this.Controls.Add(this.btnBrowseFile);
            this.Controls.Add(this.txtSymbolListFileName);
            this.Controls.Add(this.lblSymbolListFileNameHeader);
            this.Controls.Add(this.lblDaysHeader);
            this.Controls.Add(this.udDays);
            this.Controls.Add(this.txtProgress);
            this.Controls.Add(this.btnBrowseFolder);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.lblOutputFolder);
            this.Controls.Add(this.cbExchange);
            this.Controls.Add(this.txtSymbol);
            this.Controls.Add(this.lblSymbolHeader);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnClose);
            this.Name = "FormExport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export To File";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormExport_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.udDays)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbExchange;
        private System.Windows.Forms.TextBox txtSymbol;
        private System.Windows.Forms.Label lblSymbolHeader;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Label lblOutputFolder;
        private System.Windows.Forms.TextBox txtProgress;
        private System.Windows.Forms.NumericUpDown udDays;
        private System.Windows.Forms.Label lblDaysHeader;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.TextBox txtSymbolListFileName;
        private System.Windows.Forms.Label lblSymbolListFileNameHeader;
        private System.Windows.Forms.OpenFileDialog fileBrowser;
        private System.Windows.Forms.Label lblExchangeHeader;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkIncludeToday;
    }
}