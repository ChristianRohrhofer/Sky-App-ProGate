
using System;
using System.Windows.Forms;
using System.ServiceProcess;
using Sky.ProGate.Application.Objects;
using Sky.ProGate.Service.Objects;
using Microsoft.Win32;


namespace Sky.ProGate.ServiceSimulator
{
    public partial class ServiceSimulatorForm : Form
    {
        #region Members

        protected const string
            RegKeyPath_SvcSim = @"Software\ProGate\ServiceSimulator",
            RegValName_SvcSim_Sta = "Status",
            RegValName_SvcSim_Cmd = "Command";

        protected const int
            Sta_None = 0,
            CmdCod_None = 0,
            CmdCod_Start = 1,
            CmdCod_Stop = 2;

        protected ServiceTarget ServiceTarget { get; set; } = new ServiceTarget();

        public ServiceSimulatorForm()
        { InitializeComponent(); }

        #endregion Members

        #region Events

        protected void OnLoad(object sender, EventArgs e)
        { Init(); }

        protected void OnFormClosing(object sender, FormClosingEventArgs e)
        { Exit(); }

        protected void OnStart(object sender, EventArgs e)
        { Start(); }

        protected void OnStop(object sender, EventArgs e)
        { Stop(); }

        protected void OnCheckCommand(object sender, EventArgs e)
        { CheckCommand(); }

        #endregion Events

        #region Controller

        protected void Init()
        {
            //-- Enable the buttons
            try
            {
                //-- Set the service status and the command timer
                SetServiceStatus();
                CommandTimer.Start();

                //-- Enable the buttons
                EnableButtons();
            }
            catch (Exception Exc)
            {
                ShowErrorMessage(Exc);
                Close();
            }

            Start();
        }

        protected void Exit()
        {
            //-- Dispose and clear the service target
            try
            {
                //--- Stop the service target if exists
                if (ServiceTarget != null)
                {
                    //--- Stop the service and dispose the service target
                    Stop();
                    ServiceTarget.Dispose();
                }

                //--- Set the null service status
                SetServiceStatus(Sta_None);
            }
            catch (Exception Exc)
            {
                ShowErrorMessage(Exc);
                return;
            }
        }

        protected void Start()
        {
            try
            {
                //--- Start the service
                Cursor = Cursors.WaitCursor;

                //-- Start the service target and set the service status
                ServiceTarget.Start();
                SetServiceStatus();

                //-- Enable the buttons
                EnableButtons();
            }
            catch(Exception Exc)
            {
                ShowErrorMessage(Exc);
                return;
            }
            finally
            { Cursor = Cursors.Default; }
        }

        protected void Stop()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                //-- Stop the service and set the service status
                ServiceTarget.Stop();
                SetServiceStatus();

                //--- Enable the buttons
                EnableButtons();
            }
            catch (Exception Exc)
            {
                ShowErrorMessage(Exc);
                return;
            }
            finally
            { Cursor = Cursors.Default; }
        }

        protected void CheckCommand()
        {
            int nCmdCod = Global.IntegerNull;

            //--- Stop the command timer
            CommandTimer.Stop();

            //--- Get the status registry key
            nCmdCod = (int)Library.Registry.Registry.GetKeyValue
                                        (
                                            Registry.CurrentUser, 
                                            RegKeyPath_SvcSim, 
                                            RegValName_SvcSim_Cmd,  
                                            RegistryValueKind.DWord, 
                                            false, 
                                            false
                                        );

            //--- Get the command code
            if (nCmdCod == CmdCod_Start)
                Start();
            else if (nCmdCod == CmdCod_Stop)
                Stop();

            //--- Set the none command if command exists
            if (nCmdCod != CmdCod_None)
                Library.Registry.Registry.SetKeyValue(Registry.CurrentUser, RegKeyPath_SvcSim, RegValName_SvcSim_Cmd, CmdCod_None, RegistryValueKind.DWord);

            //--- Start the command timer
            CommandTimer.Start();
        }

        protected void SetServiceStatus(int nSta = -1)
        {
            //--- Get the status dependend on the service target running 
            nSta = 
                nSta != -1 ? nSta : 
                ServiceTarget.IsServiceTargetRunning ? (int)ServiceControllerStatus.Running : 
                (int)ServiceControllerStatus.Stopped;

            //--- Set the status registry key
            Library.Registry.Registry.SetKeyValue(Registry.CurrentUser, RegKeyPath_SvcSim, RegValName_SvcSim_Sta, nSta, RegistryValueKind.DWord);
        }

        #endregion Controller

        #region Forms

        protected void EnableButtons()
        {
            //--- Enable the start and stop buttons
            StartButton.Enabled = !ServiceTarget.IsServiceTargetRunning;
            StopButton.Enabled = !StartButton.Enabled;
        }

        protected void ShowErrorMessage(Exception Exc)
        {
            //--- Show the error message
            Cursor = Cursors.Default;
            MessageBox.Show(Text, Exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion Forms

    }
}

