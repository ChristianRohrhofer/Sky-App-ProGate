
using System;
using System.Collections.Generic;
using Sky.Library.SBM;
using Sky.ProGate.Application.Objects;


namespace Sky.ProGate.Application.Objects
{
    public class CalendarWeek : AppObject
    {
        #region Members

        protected const string
            ErrMsg_NotPlanDat = "Error in the CalendarWeek object: Date '%dat%' is not a planning date";

        protected static DateTime[] CalWkDats = 
                            {
                                new DateTime(2016, 01, 04),
                                new DateTime(2017, 01, 02),
                                new DateTime(2018, 01, 01),
                                new DateTime(2018, 12, 31),
                                new DateTime(2019, 12, 30),
                                new DateTime(2021, 01, 04),
                                new DateTime(2022, 01, 03),
                                new DateTime(2023, 01, 02),
                                new DateTime(2024, 01, 01),
                                new DateTime(2024, 12, 30),
                                new DateTime(2025, 12, 29),
                                new DateTime(2027, 01, 04),
                                new DateTime(2028, 01, 03),
                                new DateTime(2029, 01, 01),
                                new DateTime(2029, 12, 31),

                            };

        public int Year { get; protected set; } = Global.IntegerNull;
        public int WeekNumber { get; protected set; } = Global.IntegerNull;
        public DateTime FirstDay{ get; protected set; } = Global.DateTimeNull;
        public DateTime LastDay { get; protected set; } = Global.DateTimeNull;
        public int OrderIndex { get; protected set; } = Global.IntegerNull;

        public CalendarWeek()
            : base()
        { }

        public CalendarWeek(Calendar Cal, DateTime dtFirstDay, int nOrdIdx)
            : base(Cal)
        {
            //--- Set the values
            Year = dtFirstDay.Year;
            WeekNumber = GetWeekNumberFromDate(dtFirstDay);
            FirstDay = dtFirstDay;
            LastDay = FirstDay.AddDays(6);
            OrderIndex = nOrdIdx;
        }

        #endregion Members

        #region Calendar

        public bool IsNow()
        {
            DateTime dtNow = Global.DateTimeNull;

            //--- Get the now date and check between first day and last day
            dtNow = DateTime.Now;
            return (dtNow >= FirstDay) && (dtNow <= LastDay);
        }

        public static int GetWeekNumberFromDate(DateTime dtDat)
        {
            DateTime CalWkDat = Global.DateTimeNull;

            //--- Check date in the planning dates
            if (dtDat > CalWkDats[CalWkDats.Length - 1])
                throw new IndexOutOfRangeException(ErrMsg_NotPlanDat.Replace("%dat%", dtDat.ToString()));

            //--- Find the planning date intreval
            for (int nIdx = CalWkDats.Length - 1; nIdx >= 0; nIdx--)
            {
                //--- Calculate the week if the date in the planning interval
                if (dtDat >= CalWkDats[nIdx])
                    return (dtDat - CalWkDats[nIdx]).Days / 7 + 1;
            }

            //--- Throw a new excpetion
            throw new IndexOutOfRangeException(ErrMsg_NotPlanDat.Replace("%dat%", dtDat.ToString()));
        }

        #endregion Calendar
    }
}
