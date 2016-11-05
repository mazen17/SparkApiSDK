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

    /// <summary>
    /// 
    /// </summary>
    public partial class FormOptions : Form
    {

        private readonly ApiCredentials _apiCredentials = new ApiCredentials();

        /// <summary>
        /// FormOptions constructor
        /// </summary>
        public FormOptions()
        {

            //Initialise form components
            InitializeComponent();

            //Load options from file and assign to controls 
            ApiCredentials credentials = ApiCredentials.LoadFromFile();
            if (credentials != null)
            {
                _apiCredentials = credentials;
                txtSparkUsername.Text = credentials.Username;
                txtSparkPassword.Text = credentials.Password;
            }

        }

        /// <summary>
        /// Save options and close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {

            //Update credentials with form control values and save to file
            _apiCredentials.Username = txtSparkUsername.Text.Trim();
            _apiCredentials.Password = txtSparkPassword.Text.Trim();
            _apiCredentials.SaveToFile();
            
            //Close form
            Close();

        }

        /// <summary>
        /// Close form without saving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
