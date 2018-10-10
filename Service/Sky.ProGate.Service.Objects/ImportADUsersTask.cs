
using System;
using System.Collections.Generic;
using Sky.Library.Files;
using Sky.Library.SBM;
using Sky.ProGate.Application.Objects;


namespace Sky.ProGate.Service.Objects
{
    public class ImportADUsersTask : Task
    {
        #region Members

        public const int
            TaskID = 1;

        protected const string
            NodName_Tsk =  "ImportADUsersTask",
            NodName_ADUsrFile = "ADUserFile",
            NodName_DefUsr = "DefaultUser",
            NodName_AddUsr = "AddUser",
            NodName_UpdUsr = "UpdateUser",
            NodName_DelUsr = "DeleteUser",
            Sep_ADUsrFile_Ln = "\r\n",
            Sep_ADUsrFile_Val = ";";

        protected const string
            ProgMsg_StartImpADUsrs = "Start importing AD users",
            ProgMsg_FinImpADUsrs = "Finished importing AD users",
            ProgMsg_ReadADUsrsFile = "Read the AD users from the AD user file",
            ProgMsg_ReadUsrsSvr = "Read the users from the server '%svr%'",
            ProgMsg_ImpADUsrs = "Import the AD users",
            ProgMsg_AddOrUpdADUsrs = "Add opr update AD users",
            ProgMsg_CreUsr = "Create user with login '%log%' and name '%name%'",
            ProgMsg_UpdUsr = "Update user with login '%log%' and name '%name%'",
            ProgMsg_DelUsr = "Delete user with login '%log%' and name '%name%'",
            ErrMsg_DefUsrNotExi = "SBM default user with login ID '%log%' does not exist";

        protected FileWatcher ADUserFileWatcher { get; set; } = null;

        public ImportADUsersTask(ServiceTarget SvcTar)
            : base(SvcTar, TaskID, NodName_Tsk)
        {
            //--- Init the AD user file watcher
            ADUserFileWatcher = new FileWatcher(ServiceTarget.ConfigFile.GetString(GetConfigNodePath(NodName_ADUsrFile), true));

            //--- Set the event handlers
            ADUserFileWatcher.FileCreated += OnADUserFileChanged;
            ADUserFileWatcher.FileUpdated += OnADUserFileChanged;
        }

        #endregion Members

        #region Events

        protected void OnADUserFileChanged(object sender, FileWatcherEventArgs e)
        { InvokeTaskForRun(); }

        #endregion Events

        #region Actions

        protected void HanldeCleanUpFileChanged()
        { InvokeTaskForRun(); }

        public override void Start()
        {
            //--- Watch the AD user file
            ADUserFileWatcher.Watch(true);
        }

        public override void Run()
        {

Library.Windows.Windows.Pause(60000);
return;

            UserList UsrLst = null;
            User ImpUsr = null, DefUsr = null;
            string sDefUsrLogID = Global.StringNull, sMsg = Global.StringNull;
            object[] ValMat = null;
            ADUserInfo ADUsrInf = null;
            List<ADUserInfo> ADUsrInfLst = null;
            bool bAddUsr = Global.BooleanNull, bUpdUsr = Global.BooleanNull, bDelUsr = Global.BooleanNull;

            //--- Create a new start import message
            CreateMessage(ProgressMessage.enMessageState.Info, ProgMsg_StartImpADUsrs);

            //--- Get the action flags
            bAddUsr = ServiceTarget.ConfigFile.GetBoolean(GetConfigNodePath(NodName_AddUsr));
            bUpdUsr = ServiceTarget.ConfigFile.GetBoolean(GetConfigNodePath(NodName_UpdUsr));
            bDelUsr = ServiceTarget.ConfigFile.GetBoolean(GetConfigNodePath(NodName_DelUsr));

            //--- Create a new read AD users message
            CreateMessage(ProgressMessage.enMessageState.Info, ProgMsg_ReadADUsrsFile);

            //--- Parse the AD user value matrix and remove the header line
            ValMat = Files.ParseCsvFile(ADUserFileWatcher.FileName, Sep_ADUsrFile_Ln, Sep_ADUsrFile_Val);
            Library.Arrays.Array.RemoveAt(ref ValMat, 0);

            //--- Init the AD user infos
            ADUsrInfLst = new List<ADUserInfo>();

            //--- Add the AD user infos
            for (int nIdx = 0; nIdx < ValMat.Length; nIdx++)
            {
                //--- Init the AD user info
                ADUsrInf = new ADUserInfo((string[])ValMat[nIdx]);

                //--- Check person user
                if (!ADUsrInf.IsPersonUser())
                    continue;

                //--- Add to the AD unser infos
                ADUsrInfLst.Add(ADUsrInf);
            }

            //--- Create a new read server users message
            CreateMessage(ProgressMessage.enMessageState.Info, ProgMsg_ReadUsrsSvr.Replace("%svr%", GetServer().ServerName));

            //--- Get the default user login ID and read the users
            sDefUsrLogID = ServiceTarget.ConfigFile.GetString(GetConfigNodePath(NodName_DefUsr), true);
            UsrLst = ReadAllSBMUsers();

            //--- Find the default user and remove from the users if exists
            if ((DefUsr = UsrLst.FindByLoginID(sDefUsrLogID)) == null)
                throw new SBMException(ErrMsg_DefUsrNotExi.Replace("%log%", sDefUsrLogID));

            //--- Create a new read AD users message
            CreateMessage(ProgressMessage.enMessageState.Info, ProgMsg_ImpADUsrs);

            //--- Add or updste the AD users
            for (int nUsrIdx = 0; nUsrIdx < ADUsrInfLst.Count; nUsrIdx++)
            {
                //--- Get the AD user and clear the user
                ADUsrInf = ADUsrInfLst[nUsrIdx];
                ImpUsr = null;

                //--- Find the user with the login ID
                if ((ImpUsr = UsrLst.FindByLoginID(ADUsrInf.LoginID)) == null)
                {
                    //--- Check add user
                    if (bAddUsr)
                    {
                        //--- Init the user and set the user values
                        ImpUsr = GetServer().NewUser();
                        SetUserValues(ImpUsr, DefUsr, ADUsrInf, true);

                        //--- Tag the user and add the user to the users
                        ImpUsr.Tag = enAction.Create;
                        UsrLst.Add(ImpUsr);
                    }
                }
                else
                {
                    //--- Check update user
                    if (bUpdUsr)
                    {
                        //--- Update the and tag the user
                        ImpUsr.Tag = enAction.Update;
                        SetUserValues(ImpUsr, DefUsr, ADUsrInf, false);
                    }
                    //--- Remove the user
                    else
                        UsrLst.Remove(ImpUsr);
                }
            }
            
            //--- Delete the users
            for (int nUsrIdx = UsrLst.Count - 1; nUsrIdx >= 0; nUsrIdx--)
            {
                //--- Get the user
                ImpUsr = UsrLst[nUsrIdx];

                //--- Check user tag not exists
                if (ImpUsr.Tag != null)
                    continue;
                //--- Remove the user if not deleted and not an AD user and delete user
                else if (ImpUsr.IsDeleted() || String.IsNullOrEmpty(ImpUsr.GetEmailCC()) || !bDelUsr)
                    UsrLst.RemoveAt(nUsrIdx);
                else
                {
                    //--- Tag the user and set the user deleted
                    ImpUsr.Tag = enAction.Delete;
                    ImpUsr.SetDeleted(true);
                }
            }

            //--- Create a new read AD users message
            CreateMessage(ProgressMessage.enMessageState.Info, ProgMsg_AddOrUpdADUsrs);

            //--- Add, update or delete the users
            for (int nUsrIdx = 0; nUsrIdx < UsrLst.Count; nUsrIdx++)
            {
                //--- Get the user and the message
                ImpUsr = UsrLst[nUsrIdx];
                sMsg =
                    (enAction)ImpUsr.Tag == enAction.Create ? ProgMsg_CreUsr :
                    (enAction)ImpUsr.Tag == enAction.Update ? ProgMsg_UpdUsr :
                    (enAction)ImpUsr.Tag == enAction.Delete ? ProgMsg_DelUsr :
                    null;

                //--- Create a new message
                CreateMessage
                    (
                        ProgressMessage.enMessageState.Info, 
                        sMsg.Replace("%log%", ImpUsr.GetLoginID()).Replace("%name%", ImpUsr.GetDisplayName()), 
                        nUsrIdx + 1, 
                        UsrLst.Count
                    );

                try
                {
                    //--- Create the user
                    if ((enAction)ImpUsr.Tag == enAction.Create)
                        ImpUsr.Create();
                    //--- Update the user
                    else if (((enAction)ImpUsr.Tag == enAction.Update) || ((enAction)ImpUsr.Tag == enAction.Delete))
                        ImpUsr.Update();
                }
                //--- Log the exception message
                catch (Exception Exc)
                { CreateMessage(Exc); }
            }

            //--- Create a new message
            CreateMessage(ProgressMessage.enMessageState.Info, ProgMsg_FinImpADUsrs, UsrLst.Count, UsrLst.Count);
        }

        protected void SetUserValues(User Usr, User DefUsr, ADUserInfo ADUsrInf, bool bUsrPro)
        {
            IDList GrpIDLst = null, DefGrpIDLst = null;
            int nID = Global.IntegerNull;

            //--- Check user profile
            if (bUsrPro)
            {
                //--- Set the profile values
                Usr.SetChangeHistoryMask(DefUsr.GetChangeHistoryMask());
                Usr.SetDatePreference(DefUsr.GetDatePreference());
                Usr.SetTimePreference(DefUsr.GetTimePreference());
                Usr.SetOffsetFromGMT(DefUsr.GetOffsetFromGMT());
                Usr.SetTimeZone(DefUsr.GetTimeZone());
                Usr.SetLocale(DefUsr.GetLocale());
                Usr.SetDstSavings(DefUsr.GetDstSavings());
                Usr.SetBrowserMask(DefUsr.GetBrowserMask());
                Usr.SetFieldsMask(DefUsr.GetFieldsMask());
                Usr.SetNotesMask(DefUsr.GetNotesMask());
                Usr.SetMaxChangeHistory(DefUsr.GetMaxChangeHistory());
                Usr.SetMaxItemsPerPage(DefUsr.GetMaxItemsPerPage());
                Usr.SetMaxNotes(DefUsr.GetMaxNotes());
                Usr.SetNamespaceName(DefUsr.GetNamespaceName());
            }

            //--- Set the user ident values
            Usr.SetLoginID(ADUsrInf.LoginID.ToUpper());
            Usr.SetDisplayName(ADUsrInf.DisplayName);
            Usr.SetPhoneNumber(ADUsrInf.TelephoneNumber);
            Usr.SetEmail(ADUsrInf.Email);
            Usr.SetEmailCC(ADUsrInf.PersonnelNumber.ToString());

            GrpIDLst = Usr.GetGroupIDs();
            DefGrpIDLst = DefUsr.GetGroupIDs();

            //--- Add the default group IDs to the group IDs
            for (int nGrpIdx = 0; nGrpIdx < DefGrpIDLst.Count; nGrpIdx++)
            {
                //--- Get the default group ID
                nID = DefGrpIDLst[nGrpIdx];

                //--- Add the default group ID to the group IDs if not exists
                if (!GrpIDLst.Contains(nID))
                    GrpIDLst.Add(nID);
            }

            //--- Set the group IDs
            Usr.SetGroupIDs(GrpIDLst);

            //--- Set the default access type if the default access type is more
            if (DefUsr.GetAccessType() > Usr.GetAccessType())
                Usr.SetAccessType(DefUsr.GetAccessType());

            //--- Clear the deleted flag
            Usr.SetDeleted(false);
        }

        #endregion Actions
    }

    public class ADUserInfo
    {
        protected const string
            Sep_Memo_Ln = "\r",
            ChrStr_LogID = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789",
            ChrStr_UsrName = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZzÄäÖöÜüßÁáÀàÉéÈèÍíÌìÓóÒòÚúÙùÝý -'";

        protected const int
            ValIdx_LogID = 1,
            ValIdx_LastName = 2,
            ValIdx_FirstName = 3,
            ValIdx_DispName = 4,
            ValIdx_TelNum = 5,
            ValIdx_Email = 6,
            ValIdx_ComName = 7,
            ValIdx_Street = 8,
            ValIdx_PosCod = 9,
            ValIdx_City = 10,
            ValIdx_Dep = 11,
            ValIdx_OffNum = 12,
            ValIdx_ObjCat = 13,
            ValIdx_PerNum = 15,
            ValIdx_MobNum = 17;

        protected string[] Values { get; set; } = null;

        public ADUserInfo(string[] ValArr)
        { Values = ValArr; }

        public override string ToString()
        { return DisplayName; }

        public string LoginID
        { get { return Values[ValIdx_LogID]; } }

        public string LastName
        { get { return Values[ValIdx_LastName]; } }

        public string FirstName
        { get { return Values[ValIdx_FirstName]; } }

        public string DisplayName
        { get { return Values[ValIdx_DispName]; } }

        public string TelephoneNumber
        { get { return Values[ValIdx_TelNum]; } }

        public string MobileNumber
        { get { return Values[ValIdx_MobNum]; } }

        public string Email
        { get { return Values[ValIdx_Email]; } }

        public string Company
        { get { return Values[ValIdx_ComName]; } }

        public string Street
        { get { return Values[ValIdx_Street]; } }

        public string PostalCode
        { get { return Values[ValIdx_PosCod]; } }

        public string City
        { get { return Values[ValIdx_City]; } }

        public string Department
        { get { return Values[ValIdx_Dep]; } }

        public string Office
        { get { return Values[ValIdx_OffNum]; } }

        public int PersonnelNumber
        {
            get
            {
                int nVal;

                //--- Parse the value
                return
                    String.IsNullOrEmpty(Values[ValIdx_PerNum]) ? 0 :
                    Int32.TryParse(Values[ValIdx_PerNum], out nVal) ? nVal :
                    0;
            }
        }

        public bool IsPersonUser()
        {
            //--- Check valid login ID and valid first name and last name
            return
                IsValueValid(LoginID, ChrStr_LogID, true) &&
                IsValueValid(FirstName, ChrStr_UsrName, false) &&
                IsValueValid(LastName, ChrStr_UsrName, true);
        }

        protected static bool IsValueValid(string sVal, string sValChrStr, bool bMustExi)
        {
            char[] ValChrArr;

            //--- Get the value characters
            ValChrArr = sValChrStr.ToCharArray();

            //--- Check named exists and must exists
            if (String.IsNullOrEmpty(sVal))
                return !bMustExi;

            //--- Check name contains of namr characters
            for (int nIdx = 0; nIdx < sVal.Length; nIdx++)
            {
                //--- Check character exists in the character set 
                if (!Library.Arrays.Array.Contains(ValChrArr, sVal[nIdx]))
                    return false;
            }

            return true;
        }

    }

}












