
using System;
using System.Collections.Generic;
using Sky.Library.SBM;
using Sky.ProGate.Application.Objects;


namespace Sky.ProGate.Application.Objects
{
    public class AppObject
    {
        #region Members

        public Application Application { get; internal set; } = null;
        internal AppObject Parent { get; set; } = null;
        public object Tag { get; internal set; } = null;

        public AppObject()
        { }

        public AppObject(Application App)
        {
            //--- Set the application and clear the parent
            Application = App;
            Parent = null;
        }

        public AppObject(AppObject Par)
        {
            //--- Set the application and the parent
            Application = Par.Application;
            Parent = Par;
        }

        #endregion Members
    }

    public class SBMObject : AppObject
    {
        #region Members

        protected const string
            FldName_Tit = "TITLE",
            SQLWhere_All = "TS_ID > 0";

        protected const string
            ErrMsg_ItemFldNotExi = "Exception in the SBM object: item field with name '%name%' does not exist",
            ErrMsg_TabNotExi = "Exception in the SBM object: table with name '%tab%' does not exist",
            ErrMsg_ObjNotExi = "Exception in the SBM object: object of type '%cls%' with ID '%id%' does not exist in the object list";

        internal Item Item { get; set; } = null;

        internal SBMObject()
            : base()
        { }

        public SBMObject(Application App, Item Item)
            : base(App)
        { this.Item = Item; }

        public SBMObject(AppObject Par, Item Item)
            : base(Par)
        { this.Item = Item; }

        public override string ToString()
        { return GetItemTitle(); }

        #endregion Members

        #region Item

        protected int GetID()
        { return Item.GetID(); }

        protected string GetItemID()
        { return Item.GetItemID(); }

        protected string GetItemTitle()
        { return Item.GetTitle(); }

        public bool IsActive()
        { return Item.IsActive(); }

        protected ItemField GetItemField(Item Item, string sFldName)
        {
            ItemField ItemFld = null;

            //--- Get the item field
            if ((ItemFld = Item.Fields.FindByDatabaseName(sFldName)) == null)
                throw new Exception(ErrMsg_ItemFldNotExi.Replace("%name%", sFldName));

            return ItemFld;
        }

        protected User ReadUser(int nUsrId)
        {
            User Usr = null;

            //--- Read the user with the create user ID
            Usr = Item.Server.NewUser();
            Usr.ReadByID(nUsrId);

            return Usr;
        }

        public DateTime GetCreateDate()
        { return Item.GetCreateDate(); }

        public User GetCreator()
        { return ReadUser(Item.GetCreatedByID()); }

        public DateTime ModifiedDate()
        { return Item.GetModifiedDate(); }

        public User GetModifier()
        { return ReadUser(Item.GetModifiedByID()); }

        protected string SetItemTitle(string sTit)
        { return Item.SetTitle(sTit); }

        protected int GetItemFieldIntegerValue(string sFldName)
        { return GetItemField(Item, sFldName).GetIntegerValue(); }

        protected double GetItemFloatValue(string sFldName)
        { return GetItemField(Item, sFldName).GetFloatValue(); }

        protected string GetItemFieldTextValue(string sFldName)
        { return GetItemField(Item, sFldName).GetTextValue(); }

        protected bool GetItemFieldBooleanValue(string sFldName)
        { return GetItemField(Item, sFldName).GetBinaryValue(); }

        protected DateTime GetItemFieldDateTimeValue(string sFldName, bool bTime)
        {
            ItemField ItemFld = null;

            //--- Get the item field and the date only or date time value
            ItemFld = GetItemField(Item, sFldName);
            return ItemFld.IsNullValue() ?  Global.DateTimeNull : bTime ? ItemFld.GetDateTimeValue() : ItemFld.GetDateOnlyValue();
        }

        protected int GetItemFieldRelationalID(string sFldName)
        { return GetItemField(Item, sFldName).GetRelationalID(); }

        protected Table GetTable(string sTabName)
        {
            Table Tab = null;

            //--- Get the category table
            if ((Tab = Application.GetServer().Tables.FindByDatabaseName(sTabName)) == null)
                throw new Exception(ErrMsg_TabNotExi.Replace("%tab%", sTabName));

            return Tab;
        }

        protected SBMObjectList<T> ReadObjects<T>(SBMObjectList<T> ObjLst, string sTabName, string sSQLWhere = null) where T : SBMObject, new()
        {
            ItemList ItemLst = null;

            //--- Check object list exists
            if (ObjLst == null)
            {
                //--- Read the item list
                ItemLst = GetTable(sTabName).NewItemList();
                ItemLst.ReadBySQL(String.IsNullOrEmpty(sSQLWhere) ? SQLWhere_All : sSQLWhere);

                //--- Init the objects
                ObjLst = new SBMObjectList<T>(this, ItemLst);
            }

            return ObjLst;
        }

        protected T FindObjectByItemID<T>(T Obj, SBMObjectList<T> ObjLst, int nID) where T : SBMObject, new()
        {
            //--- Check object exists
            if (Obj == null)
            {
                //--- Find the object imn the objects
                if ((Obj = (T)ObjLst.FindByItemID(nID)) == null)
                    throw new Exception(ErrMsg_ObjNotExi.Replace("%cls%", GetType().Name).Replace("%id%", nID.ToString()));
            }

            return Obj;
        }

        #endregion Item
    }

    public class SAPObject : SBMObject
    {
        #region Members

        protected const string
            FldName_Num = "NUMBER",
            FldName_StartDat = "START_DATE",
            FldName_EndDat = "END_DATE";

        public SAPObject()
            : base()
        { }

        public SAPObject(Application App, AuxiliaryItem Item)
            : base(App, Item)
        { }

        public SAPObject(AppObject Par, AuxiliaryItem Item)
            : base(Par, Item)
        { }

        #endregion Members

        #region Item

        public int Number
        { get { return GetItemFieldIntegerValue(FldName_Num); } }

        public string Name
        { get { return GetItemTitle(); } }

        public DateTime StartDate
        { get { return GetItemFieldDateTimeValue(FldName_StartDat, false); } }

        public DateTime EndDate
        { get { return GetItemFieldDateTimeValue(FldName_EndDat, false); } }

        #endregion Item
    }

    public class AppObjectList<T> : List<T> where T : AppObject, new()
    {
        #region Members

        internal Application Application { get; set; } = null;
        internal AppObject Parent { get; set; } = null;

        public AppObjectList()
        { }

        public AppObjectList(Application App)
        {
            //--- Set the application and clear the parent
            Application = App;
            Parent = null;
        }

        public AppObjectList(AppObject Par)
        {
            //--- Set the application and the parent
            Application = Par.Application;
            Parent = Par;
        }

        #endregion Members

        #region Elements

        public T GetAt(int nIdx)
        { return this[nIdx]; }

        public new int IndexOf(T Obj)
        { return base.IndexOf(Obj); }
        
        #endregion Elements
    }

    public class SBMObjectList<T> : AppObjectList<T> where T : SBMObject, new()
    {
        #region Members

        public SBMObjectList()
            : base()
        { }

        public SBMObjectList(Application App, ItemList ItemLst)
            : base(App)
        { Init(ItemLst); }

        public SBMObjectList(AppObject Par, ItemList ItemLst)
            : base(Par)
        { Init(ItemLst); }

        protected void Init(ItemList ItemLst)
        {
            T Obj = null;

            //--- Add the objects
            for (int nIdx = 0; nIdx < ItemLst.Count; nIdx++)
            {
                //--- Init the object
                Obj = new T();

                //--- Set the applicationa nd the parent and the item list
                Obj.Application = Application;
                Obj.Parent = Parent;
                Obj.Item = ItemLst[nIdx];

                //--- Add the object
                Add(Obj);
            }
        }

        #endregion Members

        #region Elements

        public SBMObject FindByItemID(int nID)
        { return Find(Obj => Obj.Item.GetID() == nID);  }

        #endregion Elements

    }

}
