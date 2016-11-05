using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SparkAPI.Data;
using SparkAPI.Data.Securities;
using ThreadState = System.Threading.ThreadState;

namespace SparkWinFormsGUI
{

    /// <summary>
    /// Manages the downloading and exporting of Spark events to file for later replay
    /// </summary>
    public partial class FormExport : Form
    {

        private Thread _exportThread;
        private ApiSecurityEventExportManager _exportManager;
        private bool _isExportCancelled = false;

        //Delegates used to update controls via a cross-thread call
        private delegate void UpdateTextBoxTextDelegate(TextBox textBox, string text);

        /// <summary>
        /// FormExport constructor
        /// </summary>
        public FormExport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Executed when folder browse button clicked
        /// </summary>
        private void btnBrowse_Click(object sender, EventArgs e)
        {

            //Display select folder dialog to specify primary output folder
            DialogResult result = folderBrowser.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                txtOutputFolder.Text = folderBrowser.SelectedPath;
            }

        }

        /// <summary>
        /// Executed when the Close button is clicked
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Executed when the Export button is clicked
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {

            //Validate ouptut folder
            string dataFolder = txtOutputFolder.Text.Trim();
            if (!ValidateOutputFolder(dataFolder)) return;

            //Export security data to file
            bool includeCurrentDay = chkIncludeToday.Checked;
            _isExportCancelled = false;
            string fileName = txtSymbolListFileName.Text.Trim();
            if (fileName != string.Empty)
            {
                if (File.Exists(fileName))
                {
                    string[] symbols = File.ReadAllLines(fileName);
                    foreach (string symbol in symbols)
                    {
                        string[] symbolItems = symbol.Split('.');
                        if (symbolItems.Count() == 1)
                        {

                            //Export both exchanges to file (if available) if no exchange specified
                            exportSecurityToFile(symbol, "ASX", dataFolder, includeCurrentDay);
                            if (!_isExportCancelled) exportSecurityToFile(symbol, "CXA", dataFolder, includeCurrentDay);

                        }
                        else
                        {

                            //Export security to file for specified exchange
                            exportSecurityToFile(symbolItems[0], symbolItems[1], dataFolder, includeCurrentDay);
                            
                        }
                        if (_isExportCancelled) return; 
                    }
                }
                else
                {
                    MessageBox.Show(this, "Specified symbol file does not exist", "Export To File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {

                //Export security to file for specified exchange
                string symbol = txtSymbol.Text.Trim();
                if (cbExchange.Text == "Both")
                {
                    exportSecurityToFile(symbol, "ASX", dataFolder, includeCurrentDay);
                    if (!_isExportCancelled) exportSecurityToFile(symbol, "CXA", dataFolder, includeCurrentDay);                    
                }
                else
                {
                    exportSecurityToFile(symbol, cbExchange.Text, dataFolder, includeCurrentDay);
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="exchange"></param>
        /// <param name="dataFolder"></param>
        /// <param name="includeCurrentDay"></param>
        private void exportSecurityToFile(string symbol, string exchange, string dataFolder, bool includeCurrentDay)
        {

            //Add spacer line to progress text if already run an export
            btnCancel.Enabled = true;
            btnExport.Enabled = false;
            if (txtProgress.Text.Length > 0) txtProgress.Text = "\r\n" + txtProgress.Text;

            //Initiate export
            int daysToExport = (int) udDays.Value;
            _exportManager = new ApiSecurityEventExportManager(symbol, exchange, daysToExport, dataFolder, includeCurrentDay);
            _exportManager.OnProgressUpdate += new EventHandler<SparkAPI.Common.Events.GenericEventArgs<string>>(exportManager_OnProgressUpdate);            
            _exportThread = new Thread(_exportManager.Export);
            _exportThread.Start();

            //HACK: Following the initiation of the export process on a separate thread, the export form loses focus.
            //This means that if a user clicks 'Cancel' the first click is refocusing on the form, and the button click
            //is not detected. To avoid this, we force focus back on the export form to ensure the 'Cancel' click is
            //detected.

            //Focus on form so that Cancel button click is detected first click 
            this.Focus();

            //Wait until export complete or cancelled
            while ((_exportThread.ThreadState != ThreadState.Stopped) && (_exportThread.ThreadState != ThreadState.Aborted))
            {
                Thread.Sleep(200);
                Application.DoEvents();
            }
            btnCancel.Enabled = false;
            btnExport.Enabled = true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void exportManager_OnProgressUpdate(object sender, SparkAPI.Common.Events.GenericEventArgs<string> e)
        {
            string progressText = e.Value + "\r\n" + txtProgress.Text;
            if (progressText.Length > 4000) progressText = progressText.Substring(0, 4000);
            UpdateTextBoxText(txtProgress, progressText);
        }

        /// <summary>
        /// Handles cross-thread updating text in a textbox
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="text"></param>
        private static void UpdateTextBoxText(TextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new UpdateTextBoxTextDelegate(UpdateTextBoxText), textBox, text);
            }
            else
            {
                textBox.Text = text;
            }
        }

        /// <summary>
        /// Validates data output folder and prompts user to create if required. Returns TRUE if folder exists.
        /// </summary>
        /// <param name="dataFolder">Output data folder</param>
        private bool ValidateOutputFolder(string dataFolder)
        {
            if (!Directory.Exists(dataFolder))
            {

                //Prompt to create output folder
                DialogResult dialogResult = MessageBox.Show(this, "Specified data output folder does not exist. Create?", "Export To File", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No) return false;
                try
                {
                    Directory.CreateDirectory(dataFolder);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Unable to create specified output folder: " + ex.Message, "Export To File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Executed when the symbol textbox is changed
        /// </summary>
        private void txtSymbol_TextChanged(object sender, EventArgs e)
        {
            bool usingSymbol = (txtSymbol.Text != string.Empty);
            if (txtSymbolListFileName.Enabled == usingSymbol) txtSymbolListFileName.Enabled = !usingSymbol;
        }

        /// <summary>
        /// Executed when the file browser button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            DialogResult result = fileBrowser.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                txtSymbolListFileName.Text = fileBrowser.FileName;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void txtSymbolListFileName_TextChanged(object sender, EventArgs e)
        {
            txtSymbol.Enabled = (txtSymbolListFileName.Text == string.Empty);
            cbExchange.Enabled = (txtSymbolListFileName.Text == string.Empty);
        }

        /// <summary>
        /// Cancel current export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            _isExportCancelled = true;
            if (_exportThread != null) _exportThread.Abort();
            string progressText = "### EXPORT CANCELLED ###\r\n" + txtProgress.Text;            
            UpdateTextBoxText(txtProgress, progressText);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormExport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnCancel.Enabled) btnCancel_Click(null, null);
        }

    }
}
