
using System;
using System.Collections.Generic;
using System.Globalization;
using Sky.Library.SBM;
using Sky.ProGate.Application.Objects;


namespace Sky.ProGate.Application.Objects
{
    public class PlanningRate : AppObject
    {
        #region Members

        protected CalendarWeek CalendarWeek { get; set; } = null;
        protected int Rate { get; set; } = Global.IntegerNull;
        public int OrderIndex { get; protected set; } = Global.IntegerNull;

        public PlanningRate()
            : base()
        { }

        public PlanningRate(Planning Pln, int nRat, int nOrdIdx)
            : base(Pln)
        {
            //--- Set the rate and the order index
            Rate = nRat;
            OrderIndex = nOrdIdx;
        }

        public CalendarWeek GetCalendarWeek()
        { return CalendarWeek = Application.GetCalendar().GetCalendarWeeks().GetAt(OrderIndex); }

        #endregion Members
    }
}
