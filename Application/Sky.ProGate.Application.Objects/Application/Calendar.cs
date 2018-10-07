
using System;
using System.Collections.Generic;
using System.Globalization;
using Sky.Library.SBM;
using Sky.ProGate.Application.Objects;


namespace Sky.ProGate.Application.Objects
{
    public class Calendar : AppObject
    {
        #region Members

        internal static DateTime FirstPlanningDay = new DateTime(2016, 1, 4);
        internal static DateTime LastPlanningDay = new DateTime(2029, 12, 30);
        protected AppObjectList<CalendarWeek> CalendarWeeks { get; set; } = null;

        public Calendar(Application App)
            : base(App)
        { }

        public AppObjectList<CalendarWeek> GetCalendarWeeks()
        {
            CalendarWeek CalWk = null;
            DateTime dtDay = Global.DateTimeNull;
            int nWkIdx = Global.IntegerNull;

            //--- Check calendar weeks not exist
            if (CalendarWeeks == null)
            {
                //--- Init the calendar weeks
                CalendarWeeks = new AppObjectList<CalendarWeek>();

                //--- Init and add the calendar weeks
                for (dtDay = FirstPlanningDay, nWkIdx = 0; dtDay <= LastPlanningDay; dtDay = dtDay.AddDays(7), nWkIdx++)
                {
                    //--- Init the calendar week and add to the calendar weeks
                    CalWk = new CalendarWeek(this, dtDay, nWkIdx);
                    CalendarWeeks.Add(CalWk);
                }
            }

            return CalendarWeeks;
        }

        #endregion Members

        #region Calendar

        public int IndexOfCalendarWeek(DateTime dtDat)
        { return (dtDat - FirstPlanningDay).Days / 7; }

        public CalendarWeek GetCalendarWeekByDate(DateTime dtDat)
        { return GetCalendarWeeks().GetAt((dtDat - FirstPlanningDay).Days / 7); }

        #endregion Calendar


    }
}
