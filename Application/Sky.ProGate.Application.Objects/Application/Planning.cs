
using System;
using System.Collections.Generic;
using System.Globalization;
using Sky.Library.SBM;
using Sky.ProGate.Application.Objects;


namespace Sky.ProGate.Application.Objects
{
    public class Planning : SBMObject
    {
        #region Members

        internal const string
            TabName_Pln = "USR_PROCAP_PLANNING",
            FldName_Prj = "PROJECT",
            FldName_Res = "RESSOURCE",
            FldName_RatLst = "RATELIST";

        protected Person Ressource { get; set; } = null;
        protected AppObjectList<PlanningRate> PlanningRates { get; set; } = null;

        public Planning()
            : base()
        { }

        public Planning(Project Prj, AuxiliaryItem PlanItem)
            : base(Prj, PlanItem)
        { }

        public new Project Parent
        { get { return (Project)base.Parent; } }

        #endregion Members

        #region Objects

        public Person GetRessource()
        { return Ressource = FindObjectByItemID(Ressource, Application.GetRessources(), GetItemFieldRelationalID(FldName_Res)); }

        public AppObjectList<PlanningRate> GetPlanningRates()
        {
            CalendarWeek CalWk = null;
            PlanningRate PlnRat = null;
            string sRatLst = null;
            int nPos = -1;

            //--- Get the planning rates
            sRatLst = GetItemFieldTextValue(FldName_RatLst);

            if (PlanningRates == null)
            {
                PlanningRates = new AppObjectList<PlanningRate>();

                for (int nIdx = 0; nIdx < Application.GetCalendar().GetCalendarWeeks().Count; nIdx++)
                {
                    CalWk = Application.GetCalendar().GetCalendarWeeks().GetAt(nIdx);

                    nPos = nIdx * 4;

                    PlnRat = new PlanningRate
                                    (
                                        this, 
                                        nPos < sRatLst.Length ? Int32.Parse(sRatLst.Substring(nPos, 3), NumberStyles.HexNumber) : 0,
                                        CalWk.OrderIndex
                                    );

                    PlanningRates.Add(PlnRat);
                }
            }

            return PlanningRates;
        }

        #endregion Objects

    }
}
