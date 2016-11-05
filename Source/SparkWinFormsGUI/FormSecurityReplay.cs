using System;
using System.Threading;
using System.Windows.Forms;
using SparkAPI.Data;
using SparkAPI.Market;

namespace SparkWinFormsGUI
{

    /// <summary>
    /// Creates a replay data feed based on a file and displays trade and depth information for a specified security
    /// </summary>
    public partial class FormSecurityReplay : Form
    {

        private ApiEventFeedReplay _replayFeed;
        private Thread _replayThread;
        private Security _security;

        /// <summary>
        /// FormReplay constructor
        /// </summary>
        public FormSecurityReplay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Executed when the Replay/Stop button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReplay_Click(object sender, EventArgs e)
        {
            if (_replayFeed == null)
            {

                //Start new replay using specified file name
                _replayFeed = new ApiEventFeedReplay(txtFileName.Text);
                string securitySymbol = System.IO.Path.GetFileName(txtFileName.Text).Substring(0, 3);

                //Create stock to process events
                _security = new Security(securitySymbol);
                _replayFeed.AddSecurity(_security);
                securityMarketView.Security = _security;

                //Initiate replay on separate thread
                _replayThread = new Thread(_replayFeed.Execute);
                _replayThread.Start();

                //Update GUI controls
                btnPause.Enabled = true;
                btnReplay.Text = "Stop";
                ReplayToolTips.SetToolTip(btnReplay, "Stop replay");

            }
            else
            {
                
                //Stop replay
                Stop();

                //Update GUI controls
                btnPause.Enabled = false;
                btnReplay.Text = "Replay";
                ReplayToolTips.SetToolTip(btnReplay, "Start replay");
                btnPause_Click(this, null);

            }

        }

        /// <summary>
        /// Executed when the form is being closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormSecurityReplay_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Stop();
        }

        /// <summary>
        /// Stops event feed updates and closes down feed and thread
        /// </summary>
        private void Stop()
        {
            if (_replayFeed != null)
            {
                _replayFeed.RemoveSecurity(_security);
                Application.DoEvents();
                _replayThread.Abort();

                while (_replayThread.ThreadState != ThreadState.Aborted)
                {
                    Thread.Sleep(100);
                }
                _replayThread = null;
                _replayFeed = null;
            }
        }

        /// <summary>
        /// Display file browser and select file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            fileBrowser.InitialDirectory = System.Environment.CurrentDirectory;
            DialogResult result = fileBrowser.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                txtFileName.Text = fileBrowser.FileName;
            }
        }

        /// <summary>
        /// Executed when the Pause/Resume button is clicked during replay
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender, EventArgs e)
        {
            if ((_replayFeed != null) && (!_replayFeed.IsPaused))
            {
                _replayFeed.Pause();
                btnPause.Text = ">>>";
                ReplayToolTips.SetToolTip(btnPause, "Resume replay");
                btnNextEvent.Enabled = true;
            }
            else
            {
                if (_replayFeed != null) _replayFeed.Resume();
                btnPause.Text = "| |";
                ReplayToolTips.SetToolTip(btnPause, "Pause replay");
                btnNextEvent.Enabled = false;
            }
        }

        /// <summary>
        /// Steps replay forward to next event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextEvent_Click(object sender, EventArgs e)
        {
            if (_replayFeed != null) _replayFeed.StepForward();
        }

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblFileName_Click(object sender, EventArgs e)
        {

        }

    }
}
