
using System;
using System.Collections;
using System.IO;
using Sky.ProGate.Application.Objects;
using Sky.Library.SBM;


namespace Sky.ProGate.Service.Objects
{
    public abstract class Task
    {
        #region Members

        protected const string
            NodPath_Tsks = ServiceTarget.NodPath_Root + @"\Tasks",
            NodName_Tsk_Name = "Name",
            NodName_Tsk_Act = "Active",
            NodName_Tsk_StartTime = "StartTime",
            NodName_Tsk_EndTime = "EndTime",
            NodName_Tsk_LastRunDat = "LastRunDate",
            NodName_Tsk_LastRunDur = "LastRunDuration";

        protected const string
            ErrMsg_SBMTabNotExi = "SBM Table with name '%name%' does not exists",
            ErrMsg_SBMFldNotExi = "SBM Item field with database name '%name%' does not exists";

        public delegate void TaskEventHandler(object sender, TaskEventArgs e);

        public event TaskEventHandler TaskForRun;

        public ServiceTarget ServiceTarget { get; protected set; } = null;
        public int ID { get; protected set; } = Global.IntegerNull;
        public string Name { get; protected set; } = Global.StringNull;
        public bool Active { get; protected set; } = Global.BooleanNull;
        public TimeSpan StartTime { get; protected set; } = Global.TimeSpanNull;
        public TimeSpan EndTime { get; protected set; } = Global.TimeSpanNull;
        protected string TaskNodePath { get; set; } = Global.StringNull;
        public Server Server { get; protected set; } = null;
        public DateTime LastRunDate { get; protected set; } = Global.DateTimeNull;
        public TimeSpan LastRunDuration { get; protected set; } = Global.TimeSpanNull;
        public object Tag { get; set; } = null;

        public Task(ServiceTarget SvcTar, int nTaskID, string sTskNodName)
        {
            //--- Set the service target
            ServiceTarget = SvcTar;

            //--- Set the message source and the task node path
            ID = nTaskID;
            TaskNodePath = Path.Combine(NodPath_Tsks, sTskNodName);

            //--- Get the schedule parameters
            Name = ServiceTarget.ConfigFile.GetString(GetConfigNodePath(NodName_Tsk_Name), true);
            Active = ServiceTarget.ConfigFile.GetBoolean(GetConfigNodePath(NodName_Tsk_Act), true);
            StartTime = ServiceTarget.ConfigFile.GetTimeSpan(GetConfigNodePath(NodName_Tsk_StartTime), true);
            EndTime = ServiceTarget.ConfigFile.GetTimeSpan(GetConfigNodePath(NodName_Tsk_EndTime), true);
            LastRunDate = ServiceTarget.ConfigFile.GetDateTime(GetConfigNodePath(NodName_Tsk_LastRunDat), false);
            LastRunDuration = ServiceTarget.ConfigFile.GetTimeSpan(GetConfigNodePath(NodName_Tsk_LastRunDur), false);
        }

        public override string ToString()
        { return Name; }

        protected string GetConfigNodePath(string sNodName)
        { return Path.Combine(TaskNodePath, sNodName); }

        #endregion Members

        #region Server

        protected Server GetServer()
        {
            //--- Check server exists
            return 
                Server == null ? 
                    Server = new Server(ServiceTarget.GetServerName(), ServiceTarget.GetServerUser(), ServiceTarget.GetServerPassword()) : 
                    Server;
        }

        public virtual void Dispose()
        {
            //--- Logout from the server if exists
            if (Server != null)
                Server.Logout();
        }

        #endregion Server

        #region Actions

        public abstract void Start();

        protected void InvokeTaskForRun()
        { TaskForRun?.Invoke(this, new TaskEventArgs(this)); }

        public virtual bool CanRun()
        {
            DateTime dtNowDat = Global.DateTimeNull;

            //--- Get the now date
            dtNowDat = DateTime.Now;

            //--- Check now date is between start time and end time if exists
            return
                !Active ? false :
                dtNowDat.TimeOfDay < (StartTime == Global.TimeSpanNull ? new TimeSpan(0, 0, 0) : StartTime) ? false :
                dtNowDat.TimeOfDay < (EndTime == Global.TimeSpanNull ? new TimeSpan(24, 0, 0) : EndTime);
        }

        public void RunTask()
        {
            Library.Windows.StopWatch TskWat = null;

            try
            {
                //--- Init and start the task watch
                TskWat = new Library.Windows.StopWatch();
                TskWat.Start();

                //--- Create a new task start message and run the task
                ServiceTarget.ProgressManager.CreateMessage(new ProgressMessage(ID, ProgressMessage.enRunState.Start));
                Run();
            }
            catch (Exception Exc)
            { CreateMessage(Exc); }
            finally
            {
                //--- Dispose the task and stop the task watch
                Dispose();
                TskWat.Stop();

                //--- Set the last run parameters
                LastRunDate = DateTime.Now;
                LastRunDuration = TskWat.Elapsed;

                //--- Set the task last run parameters
                ServiceTarget.ConfigFile.SetDateTime(GetConfigNodePath(NodName_Tsk_LastRunDat), LastRunDate);
                ServiceTarget.ConfigFile.SetTimeSpan(GetConfigNodePath(NodName_Tsk_LastRunDur), LastRunDuration);

                //--- Update the config file and create a new task finish message
                ServiceTarget.ConfigFile.Update();
                ServiceTarget.ProgressManager.CreateMessage(new ProgressMessage(ID, ProgressMessage.enRunState.Finish));
            }
        }

        public abstract void Run();

        #endregion Actions

        #region SBM

        public enum enAction { None = 0, Create, Update, Delete }

        protected UserList ReadAllSBMUsers()
        {
            UserList UsrLst = null;

            //--- Init and read the users
            UsrLst = new UserList(GetServer());
            UsrLst.ReadAllUsers();

            return UsrLst;
        }

        protected Table GetSBMTable(string sTabName)
        {
            Table Tab;

            //--- Get the table
            if ((Tab = GetServer().Tables.FindByDatabaseName(sTabName)) == null)
                throw new SBMException(ErrMsg_SBMTabNotExi.Replace("%name%", sTabName));

            return Tab;
        }

        protected ItemList ReadSBMItems(string sTabName, string sSQLWhere = null, string sSQLOrdBy = null)
        {
            Table Tab = null;
            ItemList ItemLst = null;

            //--- Get the table
            Tab = GetSBMTable(sTabName);

            //--- Init and read the users
            ItemLst = new ItemList(Tab);
            ItemLst.ReadBySQL(sSQLWhere, sSQLOrdBy);

            return ItemLst;
        }

        protected ItemField GetSBMItemFieldByDatabaseName(Item Item, string sFldName)
        {
            ItemField Fld = null;

            //--- Get the field
            if ((Fld = Item.Fields.FindByDatabaseName(sFldName)) == null)
                throw new SBMException(ErrMsg_SBMFldNotExi.Replace("%name%", sFldName));

            return Fld;
        }

        #endregion SBM

        #region Logging

        protected void CreateMessage
                            (
                                ProgressMessage.enMessageState MsgSta, 
                                string sTxt, 
                                int nStep = Global.IntegerNull, 
                                int nCnt = Global.IntegerNull
                            )
        { ServiceTarget.ProgressManager.CreateMessage(new ProgressMessage(ID, ProgressMessage.enRunState.Step, MsgSta, sTxt, nStep, nCnt) ); }

        protected void CreateMessage(Exception Exc)
        { ServiceTarget.ProgressManager.CreateMessage(new ProgressMessage(ID, Exc)); }

        #endregion Logging
    }

    public class TaskEventArgs : EventArgs
    {
        public Task Task { get; protected set; } = null;

        public TaskEventArgs(Task Tsk)
        {
            //--- Set the task
            Task = Tsk;
        }
    }
}

