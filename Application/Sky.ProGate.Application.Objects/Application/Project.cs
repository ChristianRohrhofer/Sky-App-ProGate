
using System;
using System.Collections.Generic;
using System.Globalization;
using Sky.Library.SBM;
using Sky.ProGate.Application.Objects;


namespace Sky.ProGate.Application.Objects
{
    public class Project : SBMObject
    {
        #region Members

        internal const string
            TabName_Prj = "USR_PROCAP_PROJECTS_TAB",
            TabName_Plan = "USR_PROCAP_PROJECTS",
            TabName_Cat = "USR_CATEGORIES",
            FldName_Cat = "CATEGORY_6",
            TabName_LnItem = "USR_LINE_ITEMS",
            FldName_LnItem = "LINE_ITEM_6",
            TabName_CosCen = "USR_PROCAP_COSTCENTERS",
            FldName_CosCen = "COST_CENTER",
            FldName_PrjStartDat = "PROJECT_START_DATE",
            FldName_PrjEndDat = "PROJECT_END_DATE",
            FldName_CapStartDat = "CAPITALIZATION_START_DATE",
            FldName_CapEndDat = "CAPITALIZATION_END_DATE",
            FldName_BudID_Txt = "BUDGET_ID_TEXT",
            SQLWhere_Plns = "TS_" + Planning.FldName_Prj + " = %id%";

        protected Category Category { get; set; } = null;
        protected LineItem LineItem { get; set; } = null;
        protected CostCenter CostCenter { get; set; } = null;
        protected SBMObjectList<Planning> Plannings { get; set; } = null;

        public Project()
            : base()
        { }

        public Project(Application App, PrimaryItem Item)
            : base(App, Item)
        { }

        #endregion Members

        #region Objects

        public int GetNumber()
        { return Convert.ToInt32(GetItemID()); }

        public string GetNamer()
        { return GetItemTitle(); }

        public Category GetCategory()
        { return Category == null ? Category = FindObjectByItemID(Category, Application.GetCategories(), GetItemFieldRelationalID(FldName_Cat)) : Category; }

        public LineItem GetLineItem()
        { return LineItem == null ? LineItem = FindObjectByItemID(LineItem, Application.GetLineItems(), GetItemFieldRelationalID(FldName_LnItem)) : LineItem; }

        public CostCenter GetCostCenter()
        { return CostCenter == null ? CostCenter = FindObjectByItemID(CostCenter, Application.GetCostCenters(), GetItemFieldRelationalID(FldName_CosCen)) : CostCenter; }

        public DateTime GetProjectStartDate()
        { return GetItemFieldDateTimeValue(FldName_PrjStartDat, false); }

        public DateTime GetProjectEndDate()
        { return GetItemFieldDateTimeValue(FldName_PrjEndDat, false); }

        public DateTime GetCapitalizationStartDate()
        { return GetItemFieldDateTimeValue(FldName_CapStartDat, false); }

        public DateTime GetCapitalizationEndDate()
        { return GetItemFieldDateTimeValue(FldName_CapEndDat, false); }

        public string GetBudgetID()
        { return GetItemFieldTextValue(FldName_BudID_Txt); }

        public SBMObjectList<Planning> GetPlannings()
        { return Plannings = ReadObjects(Plannings, Planning.TabName_Pln, SQLWhere_Plns.Replace("%id%", GetID().ToString())); }

        #endregion Objects
    }
}
