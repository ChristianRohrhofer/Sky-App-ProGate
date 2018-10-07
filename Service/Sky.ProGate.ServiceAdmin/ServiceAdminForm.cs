
using System;
using System.Windows.Forms;
using System.Drawing;
using System.ServiceProcess;
using Sky.Library.Forms;
using Sky.ProGate.Application.Objects;
using Sky.ProGate.Service.Objects;


namespace Sky.ProGate.ServiceAdmin
{
    public partial class ServiceAdminForm : Form
    {
        #region Members

        protected const string
            CtrlTxt_Null = "-",
            CtrlTxt_None = "(None)",
            CtrlTxt_Bool_True = "Yes",
            CtrlTxt_Bool_False = "No",
            CtrlTxt_ProgPct = "%pct%%",
            CtrlTxt_MsgPad = "     ",
            CtrlTxt_CfgSta = "Version: %ver% / Server: %usr%@%svr% / Service:",
            IcoName_Tsk_Act = "Task.Active",
            IcoName_Tsk_Inact = "Task.Inactive",
            IcoName_Tsk_Run = "Task.Run",
            IcoName_MsgSta_Inf = "Message.Status.Info",
            IcoName_MsgSta_Warn = "Message.Status.Warning",
            IcoName_MsgSta_Err = "Message.Status.Error",
            IcoName_MsgSta_Exc = IcoName_MsgSta_Err;

        protected const int
            CtrlVal_MsgTxtBox_MarRgt = 5;

        protected Color
            Col_Svc_None = Color.DimGray,
            Col_Svc_Run = Color.DarkGreen,
            Col_Svc_Stop = Color.Red,
            Col_Svc_Oth = Color.OrangeRed;

        protected const string
            ErrMsg_InvMsgSta = "Invalid message status '%sta%'",
            ErrMsg_InvTskID = "Invalid task ID '%id%'";

        protected ServiceTarget ServiceTarget { get; set; } = null;
        protected ServiceManager ServiceManager { get; set; } = null;

        public ServiceAdminForm()
        { InitializeComponent(); }

        #endregion Members

        #region Events

        protected void OnFormLoad(object sender, EventArgs e)
        { Init(); }

        private void OnFormClose(object sender, FormClosingEventArgs e)
        { e.Cancel = !Exit(); }

        protected void OnMessagePanelRezied(object sender, EventArgs e)
        { LocateMessagePanel(); }

        protected void OnExit(object sender, EventArgs e)
        { Close(); }

        protected void OnStartService(object sender, EventArgs e)
        { StartService(); }

        protected void OnStopService(object sender, EventArgs e)
        { StopService(); }

        protected void OnInfo(object sender, EventArgs e)
        { ShowInfo(); }

        protected void OnMessageCreated(object sender, TaskProgressEventArgs e)
        { ShowMessage(e.Message); }

        protected void OnServiceStatusChanged(object sender, EventArgs e)
        { UpdateServiceStatus(); }

        #endregion Events

        #region Controller

        protected void Init()
        {
            try
            {
                //--- Locate the message panel
                LocateMessagePanel();

                //--- Init the service manager and set the event handler
                ServiceManager = new ServiceManager();
                ServiceManager.StatusChanged += OnServiceStatusChanged;
                UpdateServiceControls();

                //--- Init the service target and the task progress manager and start the watching
                ServiceTarget = new ServiceTarget();
                ServiceTarget.ProgressManager.MessageCreated += OnMessageCreated;
                ServiceTarget.ProgressManager.StartMessageWatching();

                //--- Update the controls and enable the controls
                UpdateControls();
                UpdateConfigStatusLabel();
                EnableControls();
            }
            catch (Exception Exc)
            {
                ShowException(Exc);
                Close();
            }
        }

        protected bool Exit()
        {
            try
            {
                //--- Stop the task messaage watchting
                ServiceTarget?.ProgressManager?.StopMessageWatching();

                return true;
            }
            catch (Exception Exc)
            {
                ShowException(Exc);
                Close();

                return true;
            }
        }

        protected void StartService()
        {
            //--- Start the service
            try
            { ServiceManager.StartService(); }
            catch (Exception Exc)
            {
                ShowException(Exc);
                return;
            }
        }

        protected void StopService()
        {
            //--- Stop the service
            try
            { ServiceManager.StopService(); }
            catch (Exception Exc)
            {
                ShowException(Exc);
                return;
            }
        }

        protected void OnCOMMAND(object sender, EventArgs e)
        {
            //--- Stop the service
            try
            { ServiceManager.SendServiceCommand(1234); }
            catch (Exception Exc)
            {
                ShowException(Exc);
                return;
            }
           
        }

        protected void ShowInfo()
        {
            //--- Stop the service
            try
            { new InfoDialog().ShowDialog(GetVersion()); }
            catch (Exception Exc)
            {
                ShowException(Exc);
                return;
            }
        }

        protected void UpdateServiceStatus()
        {
            try
            {
                //--- Update the service controls and enable the controls
                UpdateServiceControls();
                EnableControls();
            }
            catch (Exception Exc)
            {
                ShowException(Exc);
                return;
            }
        }

        protected void ShowMessage(ProgressMessage Msg)
        {
            //--- Update the message controls
            try
            { UpdateMessageControls(Msg); }
            catch (Exception Exc)
            {
                ShowException(Exc);
                return;
            }
        }

        protected Version GetVersion()
        { return Library.Windows.Windows.GetRunningApplicationVersion(); }

        #endregion Controller

        #region Forms

        protected void LocateMessagePanel()
        { MessageTextBox.Size = new Size(TaskProgressSplitter.Panel2.Width - MessageTextBox.Left - CtrlVal_MsgTxtBox_MarRgt, MessageTextBox.Height); }

        protected void UpdateControls()
        {
            //--- Clear the tasks items and begin the task item update
            TaskListView.Items.Clear();
            TaskListView.BeginUpdate();

            //--- Add the task items
            foreach (Task Tsk in ServiceTarget.Tasks)
                AddTaskItem(Tsk);
                
            //--- End the task item update
            TaskListView.EndUpdate();
        }

        protected void AddTaskItem(Task Tsk)
        {
            TreeListViewItem Item = null;

            //--- Add the task item
            Item = TaskListView.Items.Add();

            //--- Tag the task and the item
            Item.Tag = Tsk;
            Tsk.Tag = Item;

            //--- Update the task item
            UpdateTaskItem(Tsk);
        }

        protected void UpdateTaskItem(Task Tsk, bool bRun = false)
        {
            //--- Update the task item
            ((TreeListViewItem)Tsk.Tag).Update
                    (
                        new string[]
                            {
                                    Tsk.Name,
                                    ToBooleanItemText(Tsk.Active),
                                    Tsk.StartTime.ToString(),
                                    Tsk.EndTime.ToString(),
                                    ToDateTimeItemText(Tsk.LastRunDate),
                                    Tsk.LastRunDuration.ToString()
                            }, 
                        bRun ? IcoName_Tsk_Run : Tsk.Active ? IcoName_Tsk_Act : IcoName_Tsk_Inact
                    );
        }

        protected string ToBooleanItemText(bool bVal)
        { return bVal ? CtrlTxt_Bool_True : CtrlTxt_Bool_False; }

        protected string ToDateTimeItemText(DateTime dtDat)
        { return dtDat != Global.DateTimeNull ? dtDat.ToString() : CtrlTxt_Null; }

        protected void UpdateConfigStatusLabel()
        {
            //--- Set the version and the server parameters
            ConfigStatusLabel.Text = CtrlTxt_CfgSta
                                        .Replace("%ver%", GetVersion().ToString())
                                        .Replace("%usr%", ServiceTarget.GetServerUser())
                                        .Replace("%svr%", ServiceTarget.GetServerName());
        }

        protected void UpdateServiceControls()
        {
            Color SvcCol = Color.Transparent;

            //--- Set the service label text
            ServiceStatusLabel.Text = ServiceManager.Status == 0 ? CtrlTxt_None : ServiceManager.Status.ToString();

            //--- Set the service label forecalor
            ServiceStatusLabel.ForeColor =
                ServiceManager.Status == 0 ? Col_Svc_None :
                ServiceManager.Status == ServiceControllerStatus.Running ? SvcCol = Col_Svc_Run :
                ServiceManager.Status == ServiceControllerStatus.Stopped ? SvcCol = Col_Svc_Stop :
                Col_Svc_Oth;
        }

        protected void UpdateMessageControls(ProgressMessage Msg)
        {
            Task Tsk = null;
            string sIcoName = Global.StringNull;

            //--- Find the task
            if ((Tsk = ServiceTarget.Tasks.Find(Elem => Elem.ID == Msg.TaskID)) == null)
                throw new Exception(ErrMsg_InvTskID.Replace("%id%", Msg.TaskID.ToString()));

            //--- Update the task if sdtarted or finished
            if ((Msg.RunState == ProgressMessage.enRunState.Start) || (Msg.RunState == ProgressMessage.enRunState.Finish))
                UpdateTaskItem(Tsk, Msg.RunState == ProgressMessage.enRunState.Start);

            //--- Get the status icon name
            if (Msg.MessageState == ProgressMessage.enMessageState.Info)
                sIcoName = IcoName_MsgSta_Inf;
            else if (Msg.MessageState == ProgressMessage.enMessageState.Warning)
                sIcoName = IcoName_MsgSta_Warn;
            else if (Msg.MessageState == ProgressMessage.enMessageState.Error)
                sIcoName = IcoName_MsgSta_Err;
            else if (Msg.MessageState == ProgressMessage.enMessageState.Exception)
                sIcoName = IcoName_MsgSta_Exc;
            else
                sIcoName = null;

            //--- Visible the message status box and set the message text if text exists
            MessageStatusBox.Visible = sIcoName != null;
            MessageStatusBox.Image = sIcoName != null ? IconList.Images[sIcoName] : null;

            //--- Set the message text
            MessageTextBox.Text = CtrlTxt_MsgPad + Msg.Text;
            MessageTimeStampTextBox.Text = Msg.TimeStamp.ToString();

            //--- Check message step exists
            if (Msg.Step != Global.IntegerNull)
            {
                //--- Set the message progress bar parameters
                MessageProgressBar.Minimum = 0;
                MessageProgressBar.Maximum = Msg.Count;
                MessageProgressBar.Value = Msg.Step;

                //--- Set the message progress values
                MessageStepTextBox.Text = Msg.Step.ToString();
                MessageCountTextBox.Text = Msg.Count.ToString();
                MessagePercentTextBox.Text = (Msg.Step * 100 / Msg.Count).ToString();
            }
        }

        protected void UpdateMessageTextBox(TextBox TxtBox, int nVal)
        {
            //--- Set the valuje if exists
            if (nVal != Global.IntegerNull)
                TxtBox.Text = nVal.ToString();
        }

        protected void EnableControls()
        {
            bool bSvrRun = false;

            //--- Get the service is running
            bSvrRun = ServiceManager.Status != ServiceControllerStatus.Stopped;

            //--- Enable the service and task menues
            StartServiceFormMenu.Enabled =
                ServicePropertiesFormMenu.Enabled =
                RunTaskFormMenu.Enabled =
                TaskPropertiesFormMenu.Enabled = !bSvrRun;

            //--- Enable the stop servic menue
            StopServiceFormMenu.Enabled = bSvrRun && (ServiceTarget.RunningTask == null);
        }

        protected void ShowException(Exception Exc)
        {
            //--- Set the default cursor and show a message box
            Cursor = Cursors.Default;
            MessageBox.Show(Exc.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion Forms

    }

}


