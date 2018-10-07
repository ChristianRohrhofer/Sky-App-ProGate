
using System;
using System.Windows.Forms;
using System.ServiceProcess;
using Sky.ProGate.Application.Objects;
using Microsoft.Win32;


namespace Sky.ProGate.Service.Objects
{
    public class ServiceManager
    {
        #region Members

        protected const int
            Itv_StaTime = 250,
            CmdCod_None = 0,
            CmdCod_Start = 1,
            CmdCod_Stop = 2;

        protected const string
            RegKeyPath_SvcSim = @"Software\ProGate\ServiceSimulator",
            RegValName_SvcSim_Sta = "Status",
            RegValName_SvcSim_Cmd = "Command",
        SvcName_Svc = "ProGate Service";

        public delegate void ServiceManagerEventHandler(object sender, ServiceManagerEventArgs e);
        public event ServiceManagerEventHandler StatusChanged;

        protected ServiceController ServiceController { get; set; } = null;
        public ServiceControllerStatus Status { get; protected set; } = 0;
        protected Timer StatusTimer { get; set; } = null;

        public ServiceManager()
        {
            //--- Check server running
            if (!Global.IsRunningLocal())
                ServiceController = new ServiceController(SvcName_Svc);

            //--- Init the status timer and set the event handler
            StatusTimer = new Timer();
            StatusTimer.Interval = Itv_StaTime;
            StatusTimer.Tick += OnStatusTimerTick;

            //--- Start the status timer
            StatusTimer.Start();
        }

        #endregion Members

        #region Events

        protected void OnStatusTimerTick(object sender, EventArgs e)
        { UpdateStatus(); }

        #endregion Events

        #region ServiceController

        public virtual void StartService()
        {
            //--- Check service stopped
            if (GetStatus() != ServiceControllerStatus.Stopped)
                return;

            //--- Check local running
            if (Global.IsRunningLocal())
                SetRegistryValue(CmdCod_Start);
            else
                ServiceController.Start();
        }

        public virtual void StopService()
        {
            //--- Check service running
            if (GetStatus() != ServiceControllerStatus.Running)
                return;

            //--- Check local running
            if (Global.IsRunningLocal())
                SetRegistryValue(CmdCod_Stop);
            else
                ServiceController.Stop();
        }

        protected void SetRegistryValue(int nCmdCod)
        { Library.Registry.Registry.SetKeyValue(Registry.CurrentUser, RegKeyPath_SvcSim, RegValName_SvcSim_Cmd, nCmdCod, RegistryValueKind.DWord); }

        public void SendServiceCommand(int nCmdCod)
        {
            //--- Check local running
            if (Global.IsRunningLocal())
                ;
            else
                ;
//                ServiceController.ExecuteCommand(nCmdCod);
            //            service.WaitForStatus(ServiceControllerStatus.Running, timeout);
        }

        protected void UpdateStatus()
        {
            ServiceControllerStatus Sta = 0;

            //--- Invoke the status changed event if exists and the status changed
            if ((Sta = GetStatus()) != Status)
                StatusChanged?.Invoke(this, new ServiceManagerEventArgs(Status = Sta));
        }

        public ServiceControllerStatus GetStatus()
        {
            ServiceControllerStatus Sta = 0;

            //--- Check local running
            if (Global.IsRunningLocal())
            {
                //--- Get the service controller status
                Sta = (ServiceControllerStatus)Library.Registry.Registry.FindKeyValue
                                        (
                                            Registry.CurrentUser, 
                                            RegKeyPath_SvcSim, 
                                            RegValName_SvcSim_Sta, 
                                            RegistryValueKind.DWord, 
                                            true
                                        );
            }
            //--- Handle server running
            else
            {
                //--- Refresh the service controller and get the status
                ServiceController.Refresh();
                Sta = ServiceController.Status;
            }

            return Sta;
        }

        public virtual void Dispose()
        {
            //--- Steo the status timer
            StatusTimer.Stop();

            //--- Dispose the service controller if server runnung
            if (!Global.IsRunningLocal())
                ServiceController.Dispose();
        }

        #endregion ServiceController
    }

    public class ServiceManagerEventArgs : EventArgs
    {
        protected ServiceControllerStatus Status { get; set; } = 0;

        public ServiceManagerEventArgs(ServiceControllerStatus Sta)
        { Status = Sta; }
    }
}



