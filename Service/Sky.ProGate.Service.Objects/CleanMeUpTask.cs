
using System;
using Sky.Library.Files;


namespace Sky.ProGate.Service.Objects
{
    public class CleanUpTask : Task
    {
        #region Members

        public const int
            TaskID = 3;

        protected const string
            NodName_Tsk = "CleanUpTask",
            NodName_ClnUpFile = "CleanUpFile";

        protected FileWatcher ClnUpFileWatcher { get; set; } = null;

        public CleanUpTask(ServiceTarget SvcTar)
            : base(SvcTar, TaskID, NodName_Tsk)
        {
            //--- Init the clean up file watcher
            ClnUpFileWatcher = new FileWatcher(ServiceTarget.ConfigFile.GetString(GetConfigNodePath(NodName_ClnUpFile), true));

            //--- Set the event handlers
            ClnUpFileWatcher.FileCreated += OnCleanUpFileChanged;
            ClnUpFileWatcher.FileUpdated += OnCleanUpFileChanged;
        }

        #endregion Members

        #region Events

        protected void OnCleanUpFileChanged(object sender, FileWatcherEventArgs e)
        { InvokeTaskForRun(); }

        #endregion Events

        #region Actions

        public override void Start()
        {
            //--- Start the clean up file watcher
            ClnUpFileWatcher.Watch(true);
        }

        public override void Dispose()
        {
            //--- Dispose the task and the clean up file watcher
            base.Dispose();
            ClnUpFileWatcher.Dispose();
        }

        protected void HanldeCleanUpFileChanged()
        { InvokeTaskForRun(); }

        public override void Run()
        {
            Library.Windows.Windows.Pause(10000);
        }

        #endregion Actions
    }

}
