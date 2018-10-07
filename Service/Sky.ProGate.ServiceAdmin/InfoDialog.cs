
using System;
using System.Windows.Forms;


namespace Sky.ProGate.ServiceAdmin
{
    public partial class InfoDialog : Form
    {
        public InfoDialog()
        { InitializeComponent(); }

        protected void OnFormClose(object sender, EventArgs e)
        { Close(); }

        public DialogResult ShowDialog(Version Ver)
        {
            //--- Update the controls
            UpdateControls(Ver);
            return ShowDialog();
        }

        protected void UpdateControls(Version Ver)
        { VersionTextBox.Text = Ver.ToString(); }
    }
}
