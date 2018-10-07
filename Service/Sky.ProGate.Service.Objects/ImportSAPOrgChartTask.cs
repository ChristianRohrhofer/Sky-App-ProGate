
using System;
using System.Collections;
using Sky.Library.Files;
using Sky.ProGate.Application.Objects;
using Sky.Library.SBM;
using Sky.Library.SAP.OrgChart;


namespace Sky.ProGate.Service.Objects
{
    public class ImportSAPOrgaChartTask : Task
    {
        #region Members

        public const int
            TaskID = 2;

        protected const string
            NodName_Tsk = "ImportSAPOrgChartTask",
            NodName_ObjFile = "ObjectFile",
            NodName_RefFile = "ReferenceFile",
            TabName_OrgCha = "USR_PROCAP_ORGCHARTS",
            TabName_Unit = "USR_PROCAP_UNITS",
            TabName_Pos = "USR_PROCAP_POSITIONS",
            TabName_Per = "USR_PROCAP_PERSONS",
            TabName_CosCen = "USR_PROCAP_COSTCENTERS",
            FldName_OrgCha_Num = "NUMBER",
            FldName_OrgObj_Num = "NUMBER",
            FldName_OrgObj_StartDat = "START_DATE",
            FldName_OrgObj_EndDat = "END_DATE",
            FldName_Pos_JobID = "JOB_ID",
            FldName_Per_Usr = "USER",
            FldName_ChaObj_Units = "UNITS",
            FldName_ChaObj_Poss = "POSITIONS",
            FldName_ChaObj_Pers = "PERSONS",
            FldName_ChaObj_CosCens = "COST_CENTERS",
            FldName_ChaObj_LeadPos = "LEAD_POSITION",
            ObjTypStr_OrgCha = "Org-Chart",
            SQLWhere_OrgCha_NumSky = "TS_" + FldName_OrgCha_Num + " = %num%";

        protected const int
            FldVal_OrgCha_Num_Sky = 1;

        protected const string
            ProgMsg_StartImpSAPOrgCha = "Start importing SAP Org-Chart",
            ProgMsg_FinImpADUsrs = "Finished importing SAP Org-Chart",
            ProgMsg_ReadSAPOrgChaFile = "Read the SAP Org-Chart from the SAP files",
            ProgMsg_ReadOrgObjsSvr = "Read the org-objects from the server '%svr%'",
            ProgMsg_UsrPerNumExi = "User '%usr%' with personnel number '%num%' already exists in the user table",
            ProgMsg_ImpSAPOrgObjs = "Import the SAP org-objects",
            ProgMsg_AddOrUpdSAPOrgObjs = "Add or update the SAP org-objects",
            ProgMsg_CreOrgObj = "Create %typ% with number '%num%' and name '%name%'",
            ProgMsg_UpdOrgObj = "Update %typ% with number '%num%' and name '%name%'",
            ProgMsg_DelOrgObj = "Delete %typ% with number '%num%' and name '%name%'",
            ProgMsg_SkipOrgObj = "Skip %typ% with number '%num%' and name '%name%'",
            ErrMsg_InvSAPObjTyp = "Invalid SAP object type '%typ%'";

        protected FileWatcher SAPObjectFileWatcher { get; set; } = null;
        protected FileWatcher SAPReferenceFileWatcher { get; set; } = null;

        public ImportSAPOrgaChartTask(ServiceTarget SvcTar)
            : base(SvcTar, TaskID, NodName_Tsk)
        {
            //--- Init the SAP file watcher
            SAPObjectFileWatcher = new FileWatcher(ServiceTarget.ConfigFile.GetString(GetConfigNodePath(NodName_ObjFile), true));
            SAPReferenceFileWatcher = new FileWatcher(ServiceTarget.ConfigFile.GetString(GetConfigNodePath(NodName_RefFile), true));
        }

        #endregion Members

        #region Actions

        public override bool CanRun()
        {

return true;

            bool bRun = false;

            //--- Get the run flag
            bRun = base.CanRun() && SAPObjectFileWatcher.IsFileUpdated() && SAPReferenceFileWatcher.IsFileUpdated();

            //--- Check run
            if (bRun)
            {
                //--- Update the SAP files
                SAPObjectFileWatcher.UpdateLastWriteTime();
                SAPReferenceFileWatcher.UpdateLastWriteTime();
            }

            return bRun;
        }

        public override void Run()
        {
            OrgChart SAPOrgCha = null;
            OrgObject SAPOrgObj = null;
            Table Tab = null;
            AuxiliaryItem OrgChaItem = null, OrgObjItem = null;
            UserList UsrLst = null;
            User Usr = null;
            Hashtable UsrTab = null;
            ItemList
                OrgChaItemLst = null, 
                UnitItemLst = null, 
                PosItemLst = null, 
                PerItemLst = null, 
                CosCenItemLst = null, 
                OrgObjItemLst = null;
            int 
                nPerNum = Global.IntegerNull, 
                nProgIdx = Global.IntegerNull, 
                nAllCnt = Global.IntegerNull;

            //--- Create a new start import message
            CreateMessage(ProgressMessage.enMessageState.Info, ProgMsg_StartImpSAPOrgCha);

            //--- Read the SAP org-chart 
            SAPOrgCha = new OrgChart();
            SAPOrgCha = OrgChart.FromSAPFiles(SAPObjectFileWatcher.FileName, SAPReferenceFileWatcher.FileName);

            //--- Create a new read server users message
            CreateMessage(ProgressMessage.enMessageState.Info, ProgMsg_ReadOrgObjsSvr.Replace("%svr%", GetServer().ServerName));

            //--- Read the users
            UsrLst = ReadAllSBMUsers();

            //--- Init the user table and the AD user info
            UsrTab = new Hashtable();

            //--- Add the user to the user table
            for (int nIdx = 0; nIdx < UsrLst.Count; nIdx++)
            {
                //--- Get the user and the personnel number
                Usr = UsrLst[nIdx];
                nPerNum = String.IsNullOrEmpty(Usr.GetEmailCC()) ? 0 : Convert.ToInt32(Usr.GetEmailCC());

                //--- Check personnel number not exists
                if (nPerNum == 0)
                    continue;
                //--- Check user exists in the user table
                else if (UsrTab[nPerNum] == null)
                    UsrTab.Add(nPerNum, Usr);
                else
                {
                    //--- Create a new start import message
                    CreateMessage
                        (
                            ProgressMessage.enMessageState.Warning, 
                            ProgMsg_UsrPerNumExi
                                        .Replace("%usr%", Usr.GetDisplayName())
                                        .Replace("%num%", nPerNum.ToString())
                        );
                }
            }

            //--- Read the orag objects
            OrgChaItemLst = ReadSBMItems(TabName_OrgCha, SQLWhere_OrgCha_NumSky.Replace("%num%", FldVal_OrgCha_Num_Sky.ToString()));
            UnitItemLst = ReadSBMItems(TabName_Unit);
            PosItemLst = ReadSBMItems(TabName_Pos);
            PerItemLst = ReadSBMItems(TabName_Per);
            CosCenItemLst = ReadSBMItems(TabName_CosCen);

            //--- Find the org-chart item wit the Sky number
            OrgChaItem = (AuxiliaryItem)OrgChaItemLst.FindByItemFieldValue(FldName_OrgCha_Num, FieldList.enFieldIdent.DatabaseName, FldVal_OrgCha_Num_Sky);

            //--- Check org-chart item exists
            if (OrgChaItem == null)
            {
                //--- Init the Sky org-chart and add to the org-chart items
                OrgChaItem = GetSBMTable(TabName_OrgCha).NewAuxiliaryItem();
                OrgChaItemLst.Add(OrgChaItem);

                //--- Tag the org-chart item for create
                OrgChaItem.Tag = enAction.Create;
            }
            //--- Tag the org-chart item for update
            else
                OrgChaItem.Tag = enAction.Update;

            //--- Update the org-chart item values
            GetSBMItemFieldByDatabaseName(OrgChaItem, FldName_OrgCha_Num).SetIntegerValue(FldVal_OrgCha_Num_Sky);
            OrgChaItem.SetActive(true);

            //--- Create a new read AD users message
            CreateMessage(ProgressMessage.enMessageState.Info, ProgMsg_ImpSAPOrgObjs);

            //--- Import the SAP org-objects
            for (int nIdx = 0; nIdx < SAPOrgCha.OrgObjects.Count; nIdx++)
            {
                //--- Get the SAP object and the table
                SAPOrgObj = SAPOrgCha.OrgObjects[nIdx];
                Tab = GetOrgObjectTable(SAPOrgObj.ObjectType);

                //--- Get the table name dependend on the SAP object type
                if (SAPOrgObj.ObjectType == OrgObject.enObjectType.Unit)
                    OrgObjItemLst = UnitItemLst;
                else if (SAPOrgObj.ObjectType == OrgObject.enObjectType.Position)
                    OrgObjItemLst = PosItemLst;
                else if (SAPOrgObj.ObjectType == OrgObject.enObjectType.Person)
                    OrgObjItemLst = PerItemLst;
                else if (SAPOrgObj.ObjectType == OrgObject.enObjectType.CostCenter)
                    OrgObjItemLst = CosCenItemLst;
                else
                    throw new Exception(ErrMsg_InvSAPObjTyp.Replace("%typ%", SAPOrgObj.ObjectType.ToString()));

                //--- Find the orga object item
                OrgObjItem = (AuxiliaryItem)OrgObjItemLst.FindByItemFieldValue(FldName_OrgObj_Num, FieldList.enFieldIdent.DatabaseName, SAPOrgObj.ObjectNumber);

                //--- Check org-object item exists
                if (OrgObjItem == null)
                {
                    //--- Init the orga object and add to the org-object items
                    OrgObjItem = Tab.NewAuxiliaryItem();
                    OrgObjItemLst.Add(OrgObjItem);

                    //--- Tag the org-object item with the crsate action
                    OrgObjItem.Tag = enAction.Create;
                }
                //--- Tag the org-object item with the update action
                else
                    OrgObjItem.Tag = enAction.Update;

                //--- Set the values
                OrgObjItem.SetTitle(SAPOrgObj.LongName);
                OrgObjItem.SetActive(SAPOrgObj.IsActiveNow());
                GetSBMItemFieldByDatabaseName(OrgObjItem, FldName_OrgObj_Num).SetIntegerValue(SAPOrgObj.ObjectNumber);
                SetOrgObjectItemDate(OrgObjItem, FldName_OrgObj_StartDat, SAPOrgObj.StartDate);
                SetOrgObjectItemDate(OrgObjItem, FldName_OrgObj_EndDat, SAPOrgObj.EndDate);

                //--- Check position object type
                if (SAPOrgObj.ObjectType == OrgObject.enObjectType.Position)
                {
                    try
                    { GetSBMItemFieldByDatabaseName(OrgObjItem, FldName_Pos_JobID).SetIntegerValue(Convert.ToInt32(SAPOrgObj.ShortName)); }
                    catch(Exception Exc)
                    {; }
                }
                    
                //--- Check person object type
                else if (SAPOrgObj.ObjectType == OrgObject.enObjectType.Person)
                {
                    //--- Get the user
                    Usr = (User)UsrTab[SAPOrgObj.ObjectNumber];

                    //--- Set the user in the person item
                    if (Usr == null)
                        GetSBMItemFieldByDatabaseName(OrgObjItem, FldName_Per_Usr).SetNullValue();
                    else
                        GetSBMItemFieldByDatabaseName(OrgObjItem, FldName_Per_Usr).SetRelationalID(Usr.GetID());

                    //--- Set the user active dependend if the user exists and is active
                    OrgObjItem.SetActive(Usr != null ? !Usr.IsDeleted() : false);
                }

                //--- Tag the SAP org-object with the org-object item
                SAPOrgObj.Tag = OrgObjItem;
            }

            //--- Set the chart objects of the org-chart
            SetChartObjects(SAPOrgCha.ChartObjects, OrgChaItem);

            //--- Activate and tag the item lists
            ActivateOrgObjectItems(UnitItemLst, false, enAction.Delete);
            ActivateOrgObjectItems(PosItemLst, false, enAction.Delete);
            ActivateOrgObjectItems(PerItemLst, false, enAction.Delete);
            ActivateOrgObjectItems(CosCenItemLst, true, enAction.None);

            //--- Create a new read AD users message
            CreateMessage(ProgressMessage.enMessageState.Info, ProgMsg_AddOrUpdSAPOrgObjs);

            //--- Init the progress index and calculate the all count 
            nProgIdx = 0;
            nAllCnt = OrgChaItemLst.Count + UnitItemLst.Count + PosItemLst.Count + PerItemLst.Count + CosCenItemLst.Count;

            //--- Add or update the org-objects
            AddOrUpdateOrgObjItems(OrgChaItemLst, ObjTypStr_OrgCha, ref nProgIdx, nAllCnt);
            AddOrUpdateOrgObjItems(UnitItemLst, OrgObject.enObjectType.Unit.ToString(), ref nProgIdx, nAllCnt);
            AddOrUpdateOrgObjItems(PosItemLst, OrgObject.enObjectType.Position.ToString(), ref nProgIdx, nAllCnt);
            AddOrUpdateOrgObjItems(PerItemLst, OrgObject.enObjectType.Person.ToString(), ref nProgIdx, nAllCnt);
            AddOrUpdateOrgObjItems(CosCenItemLst, OrgObject.enObjectType.CostCenter.ToString(), ref nProgIdx, nAllCnt);

            //--- Create a new message
            CreateMessage(ProgressMessage.enMessageState.Info, ProgMsg_FinImpADUsrs, nAllCnt, nAllCnt);
        }

        protected void SetChartObjects(ChartObjectList SAPChaObjLst, AuxiliaryItem OrgObjItem)
        {
            ChartObject SAPChaObj = null;
            AuxiliaryItem ChaObjItem = null;
            string sFldName = null;
            ItemIdentList IdentLst = null;
            ItemField Fld = null;

            //--- Clear the chart object references fields
            ClearChartObjectReferences(OrgObjItem, FldName_ChaObj_Units);
            ClearChartObjectReferences(OrgObjItem, FldName_ChaObj_Poss);
            ClearChartObjectReferences(OrgObjItem, FldName_ChaObj_Pers);
            ClearChartObjectReferences(OrgObjItem, FldName_ChaObj_CosCens);

            //--- Import the SAP objects
            for (int nIdx = 0; nIdx < SAPChaObjLst.Count; nIdx++)
            {
                //--- Get the SAP chart object and the tagged org-object item
                SAPChaObj = SAPChaObjLst[nIdx];
                ChaObjItem = (AuxiliaryItem)SAPChaObj.OrgObject.Tag;

                //--- Get the chart object field name with the SAP object type
                if (SAPChaObj.OrgObject.ObjectType == OrgObject.enObjectType.Unit)
                    sFldName = FldName_ChaObj_Units;
                else if (SAPChaObj.OrgObject.ObjectType == OrgObject.enObjectType.Position)
                    sFldName = FldName_ChaObj_Poss;
                else if (SAPChaObj.OrgObject.ObjectType == OrgObject.enObjectType.Person)
                    sFldName = FldName_ChaObj_Pers;
                else if (SAPChaObj.OrgObject.ObjectType == OrgObject.enObjectType.CostCenter)
                    sFldName = FldName_ChaObj_CosCens;
                else
                    throw new Exception(ErrMsg_InvSAPObjTyp.Replace("%typ%", SAPChaObj.OrgObject.ObjectType.ToString()));

                //--- Get the field and the items idents
                Fld = GetSBMItemFieldByDatabaseName(OrgObjItem, sFldName);
                IdentLst = Fld.GetItemIdentValues();

                //--- Add the chart object item ident and set the item idents
                IdentLst.Add(new ItemIdent(ChaObjItem));
                Fld.SetItemIdentValues(IdentLst);

                //--- Set the item references 
                SetChartObjects(SAPChaObj.ChartObjects, ChaObjItem);
            }
        }

        protected Table GetOrgObjectTable(OrgObject.enObjectType SAPObjTyp)
        {
            string sTabName;

            //--- Get the table name dependend on the SAP object type
            if (SAPObjTyp == OrgObject.enObjectType.Unit)
                sTabName = TabName_Unit;
            else if (SAPObjTyp == OrgObject.enObjectType.Position)
                sTabName = TabName_Pos;
            else if (SAPObjTyp == OrgObject.enObjectType.Person)
                sTabName = TabName_Per;
            else if (SAPObjTyp == OrgObject.enObjectType.CostCenter)
                sTabName = TabName_CosCen;
            else
                throw new Exception(ErrMsg_InvSAPObjTyp.Replace("%typ%", SAPObjTyp.GetType().Name));

            //--- Get the table
            return GetSBMTable(sTabName);
        }

        protected void SetOrgObjectItemDate(AuxiliaryItem OrgObjItem, string sItemFldName, DateTime dtDat)
        {
            ItemField Fld = null;

            //--- Get the item field name
            Fld = GetSBMItemFieldByDatabaseName(OrgObjItem, sItemFldName);

            //--- Set the null value or date value to the item field
            if (OrgChart.IsNullDate(dtDat))
                Fld.SetNullValue();
            else
                Fld.SetDateOnlyValue(dtDat);
        }

        protected void ActivateOrgObjectItems(ItemList OrgObjItemLst, bool bAct, enAction Act)
        {
            AuxiliaryItem OrgObjItem = null;

            //--- Tag the items for delete
            for (int nIdx = 0; nIdx < OrgObjItemLst.Count; nIdx++)
            {
                //--- Get the org-object item and tag the item for delete if tag not exists
                OrgObjItem = (AuxiliaryItem)OrgObjItemLst[nIdx];

                //--- Check item tag not exists
                if (OrgObjItem.Tag == null)
                {
                    //--- Set the active flag and tag the action
                    OrgObjItem.SetActive(bAct);
                    OrgObjItem.Tag = Act;
                }
            }
        }

        protected void AddOrUpdateOrgObjItems(ItemList OrgObjItemLst, string sObjTyp, ref int nProgIdx, int nAllCnt)
        {
            AuxiliaryItem OrgObjItem = null;
            string sMsg = null;

            //--- Add, update or delete the users
            for (int nIdx = 0; nIdx < OrgObjItemLst.Count; nIdx++)
            {
                //--- Get the user and the message
                OrgObjItem = (AuxiliaryItem)OrgObjItemLst[nIdx];
                sMsg =
                    (enAction)OrgObjItem.Tag == enAction.Create ? ProgMsg_CreOrgObj :
                    (enAction)OrgObjItem.Tag == enAction.Update ? ProgMsg_UpdOrgObj :
                    (enAction)OrgObjItem.Tag == enAction.Delete ? ProgMsg_DelOrgObj :
                    ProgMsg_SkipOrgObj;

                //--- Create a new message
                CreateMessage
                    (
                        ProgressMessage.enMessageState.Info,
                        sMsg
                            .Replace("%typ%", sObjTyp)
                            .Replace("%num%", GetSBMItemFieldByDatabaseName(OrgObjItem, FldName_OrgObj_Num).GetIntegerValue().ToString())
                            .Replace("%name%", OrgObjItem.GetTitle()),
                        nProgIdx + 1,
                        nAllCnt
                    );

                try
                {
                    //--- Create the user
                    if ((enAction)OrgObjItem.Tag == enAction.Create)
                        OrgObjItem.Create();
                    //--- Update the user
                    else if (((enAction)OrgObjItem.Tag == enAction.Update) || ((enAction)OrgObjItem.Tag == enAction.Delete))
                        OrgObjItem.Update();
                }
                //--- Log the exception message
                catch (Exception Exc)
                { CreateMessage(Exc); }

                //--- Increment the org-object index
                nProgIdx++;
            }
        }

        protected void ClearChartObjectReferences(AuxiliaryItem ChaObjItem, string sFldName)
        {
            ItemField Fld = null;

            //--- Get the field and check exists
            if ((Fld = ChaObjItem.Fields.FindByDatabaseName(sFldName)) == null)
                return;

            //--- Clear the field value
            Fld.SetNullValue();
        }




        #endregion Actions

    }
}












