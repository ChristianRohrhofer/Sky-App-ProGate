
using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
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
        public ImportADUsersTask ImportADUserTask { get; set; } = null;
        public ImportSAPOrgaChartTask ImportSAPOrgaChartTask { get; set; } = null;
        public CleanUpTask CleanUpTask { get; set; } = null;
        public List<Task> Tasks { get; protected set; } = null;
        public List<Task> TaskWaitList { get; protected set; } = null;
        protected Timer TaskTimer { get; set; } = null;
        public ProgressManager ProgressManager { get; set; } = null;

        public ServiceTarget()
        {
            //--- Init the config file and the message logger
            ConfigFile = new ConfigFile(Global.GetFileName(FileName_Cfg));

            TaskTimer = new Timer();
            TaskTimer.Interval = Itv_TskTime;
            TaskTimer.Tick += OnRunNextTask;
            ProgressManager = new ProgressManager(ConfigFile.GetString(NodPath_Log_Path, true));

            //--- Init the task lists
            Tasks = new List<Task>();
            TaskWaitList = new List<Task>();

            //--- Init the tasks
            InitTask(ImportADUserTask = new ImportADUsersTask(this));
            InitTask(ImportSAPOrgaChartTask = new ImportSAPOrgaChartTask(this));
            InitTask(CleanUpTask = new CleanUpTask(this));
        }

        protected void InitTask(Task Tsk)
        {
            //--- Set the task event handler and add to the tasks
            Tsk.TaskForRun += OnTaskForRun;
            Tasks.Add(Tsk);
        }

        public void Dispose()
        {
            //--- Stop the service target and dispose the task progress manager
            Stop();
            ProgressManager?.Dispose();
        }

        #endregion Members

        #region Events

        protected void OnTaskForRun(object sender, TaskEventArgs e)
        { AddTaskToTaskWaitList(e.Task); }

        protected void OnRunNextTask(object sender, EventArgs e)
        { RunNextTask(); }

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
            //--- Set the service target running flag
            IsServiceTargetRunning = true;

            //--- Start th active tasks 
            foreach (Task Tsk in Tasks)
            {
                //--- Start the task if active
                if (Tsk.Active)
                    Tsk.Start();
            }

            //--- Start the task timer
            TaskTimer.Start();
        }

        public void Stop()
        {
            //--- Stop the task timer
            TaskTimer.Stop();

            //--- Wait while running task exists
            while (RunningTask != null)
            {; }

            //--- Dispose the tasks 
            foreach (Task Tsk in Tasks)
                Tsk.Dispose();

            //--- Clear the service target running flag
            IsServiceTargetRunning = false;
        }

        public void AddTaskToTaskWaitList(Task Tsk)
        {
            //--- Check task can run or exists in the task wait list
            if (Tsk.CanRun() ? TaskWaitList.Contains(Tsk) : true)
                return;

            //--- Add the task to the task wait list
            TaskWaitList.Add(Tsk);
        }

        public void RunNextTask()
        {
            //--- Check task in task wait list exists
            if (TaskWaitList.Count == 0)
                return;

            //--- Step the task timer
            TaskTimer.Stop();

            //--- Get and remove the first task of the task wait list
            RunningTask = TaskWaitList[0];
            TaskWaitList.RemoveAt(0);

            //--- Run the task and clear the running task
            RunningTask.Run();
            RunningTask = null;

            //--- Start the task timer
            TaskTimer.Start();
        }

        #endregion Actions
    }

}


