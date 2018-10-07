
using System;
using System.IO;
using System.Windows.Forms;
using Sky.Library.Files;


namespace Sky.ProGate.Service.Objects
{
    public class ProgressManager
    {
        #region Members

        protected const string
            FileName_Msg = "Sky.ProGate.Service.Message.log",
            EvLog_Msg = "\r\n\r\n%msg%",
            EvLog_Ln_Sep = "\r\n";

        protected const int
            Timeout_MsgFile = 5000,
            Del_MsgFile = 250,
            Itv_ChkMsg = 100,
            Itv_CreMsg = 250;

        protected const string
            ErrMsg_TimeoutMsgFile = "Timeout opening the message file '%file%'",
            ErrMsg_MsgTimeEnab = "The message timer for reading messages is enabled",
            ErrMsg_MsgTimeNotEnab = "The message timer for reading messages is not enabled";

        public delegate void TaskProgressEventHandler(object sender, TaskProgressEventArgs e);
        public event TaskProgressEventHandler MessageCreated;

        protected FileWatcher FileWatcher { get; set; } = null;
        protected Timer MessageTimer { get; set; } = null;
        protected Library.Windows.StopWatch MessageWatch { get; set; } = null;

        public ProgressManager(string sLogPath)
        {
            //--- Init the file watcher
            FileWatcher = new FileWatcher(Path.Combine(sLogPath, FileName_Msg));

            //--- Init and start the mesage watch
            MessageWatch = new Library.Windows.StopWatch();
            MessageWatch.Start();

            //--- Init the message timer and set the event handler
            MessageTimer = new Timer();
            MessageTimer.Interval = Itv_ChkMsg;
            MessageTimer.Tick += OnMessageTimerTick;
        }

        public void Dispose()
        {
            //--- Disable the message timer and delete the message
            MessageTimer.Enabled = false;
            DeleteMessage();
        }

        #endregion Members

        #region Events

        protected void OnMessageTimerTick(object sender, EventArgs e)
        { CheckMessage(); }

        public void StartMessageWatching()
        { MessageTimer.Start(); }

        public void StopMessageWatching()
        { MessageTimer.Stop(); }

        #endregion Events

        #region Messages

        protected void CheckMessage()
        {
            string sFileTxt = null;
            StreamReader StrRead = null;

            //--- Check message time is not enabled
            if (!MessageTimer.Enabled)
                throw new Exception(ErrMsg_MsgTimeNotEnab);

            //--- Check file exists and updated
            if (FileWatcher.ExistsFile() ? !FileWatcher.CheckFileUpdated() : true)
                return;

            //--- Step the message timer
            MessageTimer.Stop();

            //--- Read the file text
            StrRead = new StreamReader(OpenMessageFile(FileAccess.Read));
            sFileTxt = StrRead.ReadToEnd();

            //--- Dispose the stream reader
            StrRead.Dispose();

            //--- Invoke the message sent event if exists and start the message timer
            MessageCreated?.Invoke(this, new TaskProgressEventArgs(new ProgressMessage(sFileTxt)));
            MessageTimer.Start();
        }

        public void CreateMessage(ProgressMessage Msg)
        {
            FileStream FileStr = null;
            StreamWriter StrWri = null;
            string sTxt = null;

            //--- Check message time is enabled
            if (MessageTimer.Enabled)
                throw new Exception(ErrMsg_MsgTimeEnab);

            //--- Wait for the next message
            MessageWatch.Wait(Itv_CreMsg, true);

            //--- Read the file text
            FileStr = OpenMessageFile(FileAccess.Write);
            StrWri = new StreamWriter(FileStr);

            //--- Set the start position and write the file with the message text
            FileStr.Position = 0;
            StrWri.Write(sTxt = Msg.ToText());

            //--- Set the json length and flush the stream writer
            FileStr.SetLength(sTxt.Length);
            StrWri.Flush();

            //--- Close the file
            StrWri.Dispose();
            FileStr.Dispose();
        }

        protected void DeleteMessage()
        {
            //--- Delete the message file if exists
            if (File.Exists(FileWatcher.FileName))
                File.Delete(FileWatcher.FileName);
        }

        protected FileStream OpenMessageFile(FileAccess FileAcc)
        {
            FileStream FileStr = null;
            Library.Windows.StopWatch FileWat = null;

            //--- Init and start the fgile watch
            FileWat = new Library.Windows.StopWatch();
            FileWat.Start();

            //--- Open the repository file or timeout
            while(true)
            {
                try
                {
                    //--- Open the repository file
                    FileStr = File.Open
                                (
                                    FileWatcher.FileName, 
                                    FileAcc == FileAccess.Read ? FileMode.Open : FileMode.OpenOrCreate, 
                                    FileAcc, 
                                    FileShare.None
                                );
                    break;
                }
                catch (IOException Exc)
                {
                    //--- Check timeout
                    if (FileWat.ElapsedMilliseconds > Timeout_MsgFile)
                        throw new Exception(ErrMsg_TimeoutMsgFile.Replace("%file%", FileWatcher.FileName));

                    //--- Pause for the next file access
                    Library.Windows.Windows.Pause(Del_MsgFile);

                    continue;
                }
            }

            return FileStr;
        }

        #endregion Messages
    }

    public class TaskProgressEventArgs : EventArgs
    {
        public ProgressMessage Message { get; protected set; } = null;

        public TaskProgressEventArgs(ProgressMessage Msg)
        { Message = Msg; }
    }
}


