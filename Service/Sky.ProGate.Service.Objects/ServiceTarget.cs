
using System;
using System.IO;
using System.Collections.Generic;
using System.Timers;
using Sky.ProGate.Application.Objects;


namespace Sky.ProGate.Service.Objects
{
    public class ServiceTarget
    {
        #region Members

        internal const string
            FileName_Cfg = @"Config\ProGate.Service.Config.xml",
            NodPath_Root = "ProGate.Service.Config",
            NodPath_Log = NodPath_Root + @"\Logging",
            NodPath_Log_Path = NodPath_Log + @"\Path",
            NodPath_SBM = ServiceTarget.NodPath_Root + @"\SBM",
            NodPath_SBM_Svr = NodPath_SBM + @"\Server",
            NodPath_SBM_Usr = NodPath_SBM + @"\User",
            NodPath_SBM_Pwd = NodPath_SBM + @"\Password";

        protected const int
            Itv_TskTime = 250;

        public ConfigFile ConfigFile { get; set; } = null;
        public bool IsServiceTargetRunning { get; protected set; } = false;
        public Task RunningTask { get; protected set; } = null;
        protected Timer TaskTimer { get; set; } = null;
        public ImportADUsersTask ImportADUserTask { get; set; } = null;
        public ImportSAPOrgaChartTask ImportSAPOrgaChartTask { get; set; } = null;
        public List<Task> Tasks { get; protected set; } = null;
        public ProgressManager ProgressManager { get; set; } = null;

        public ServiceTarget()
        {
            //--- Init the config file and the message logger
            ConfigFile = new ConfigFile(Global.GetFileName(FileName_Cfg));
            ProgressManager = new ProgressManager(ConfigFile.GetString(NodPath_Log_Path, true));
    
            //--- Init the tasks
            ImportADUserTask = new ImportADUsersTask(this);
            ImportSAPOrgaChartTask = new ImportSAPOrgaChartTask(this);

            //--- Init the tasks
            Tasks = new List<Task>() { ImportADUserTask, ImportSAPOrgaChartTask };

            //--- Init the task time and set the event handler
            TaskTimer = new Timer(Itv_TskTime);
            TaskTimer.Elapsed += OnRunTasks;
        }

        public void Dispose()
        {
            //--- Stop the service target and dispose the task progress manager
            Stop();
            ProgressManager?.Dispose();
        }

        #endregion Members

        #region Events

        protected void OnRunTasks(object sender, EventArgs e)
        { RunTasks(); }

        #endregion Events

        #region Server

        public string GetServerName()
        { return ConfigFile.GetString(NodPath_SBM_Svr, true); }

        public string GetServerUser()
        { return ConfigFile.GetString(NodPath_SBM_Usr, true); }

        public string GetServerPassword()
        { return Library.Text.Crypt.Decrypt(ConfigFile.GetString(NodPath_SBM_Pwd, true)); }

        #endregion Server

        #region Actions

        public void Start()
        {
            //--- Start the task timer and set the service target running flag
            TaskTimer.Start();
            IsServiceTargetRunning = true;
        }

        public void Stop()
        {
            //--- Wait while running task exists
            while(RunningTask != null)
            {; }

            //--- Stop the task timer and clear the service target running flag
            TaskTimer.Stop();
            IsServiceTargetRunning = false;
        }

        protected void RunTasks()
        {
            //--- Stop the task timer
            TaskTimer.Stop();

            //--- Run the task
            foreach (Task Tsk in Tasks)
            {
                //--- Run the zask if can run
                if (Tsk.CanRun())
                    RunTask(Tsk);
            }

            //--- Start the task timer
            TaskTimer.Start();
        }

        protected void RunTask(Task Tsk)
        {
            //--- Set the running task
            RunningTask = Tsk;

            //--- Run the task
            Tsk.RunTask();

            //--- Clear the running task
            RunningTask = null;
        }

        #endregion Actions
    }

}
