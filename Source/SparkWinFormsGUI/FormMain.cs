using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SparkAPI.Data;

namespace SparkWinFormsGUI
{
    public partial class FormMain : Form
    {

        /// <summary>
        /// FormMain constructor
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Display options form
        /// </summary>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOptions optionsForm = new FormOptions();
            optionsForm.ShowDialog(this);            
        }

        /// <summary>
        /// Close application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Ensure disconnection from Spark API
            ApiFunctions.Disconnect();
            
            //Close main form
            Close();

        }

        /// <summary>
        /// Display live market security form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void securityLiveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Check that user has setup Spark credentials
            ApiCredentials credentials = ApiCredentials.LoadFromFile();
            if ((credentials == null) || (!credentials.Validate()))
            {
                MessageBox.Show(this, "Connection to live market feed via the Spark API requires a valid username and password. " +
                                      "Please enter security credentials in the appliction options.",
                                "Authorisation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                optionsToolStripMenuItem_Click(this, null);
                return;
            }

            //Display live market security form
            FormSecurity securityForm = new FormSecurity();
            securityForm.Show();

        }

        /// <summary>
        /// Display replay from file security form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void securityReplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSecurityReplay securityReplayForm = new FormSecurityReplay();
            securityReplayForm.Show();
        }

        /// <summary>
        /// Display the export security to file form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormExport exporForm = new FormExport();
            exporForm.ShowDialog(this);
        }

    }
}
