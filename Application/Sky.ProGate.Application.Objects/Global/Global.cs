
using System;
using System.IO;


namespace Sky.ProGate.Application.Objects
{
    public static class Global
    {
        #region Members

        public const string
            DateTimeValueFormat = "yyyyMMddHHmmss",
            TimeSpanValueFormat = "hhmmss";

        const string
            Path_Root_Loc = @"C:\Development\ProGate",
            Path_Root_Svr = @"C:\Program Files\Sky\Tools\ProGate";

        const bool
#if DEBUG
        Val_Dbg = true;
#else
        Val_Dbg = false;
#endif

        public const long
            LongNull = -1L;

        public const int
            IntegerNull = -1;

        public const double
            DoubleNull = -1.0;

        public const bool
            BooleanNull = false;

        public const string
            StringNull = null;

        public static DateTime
            DateTimeNull = DateTime.MinValue;

        public static TimeSpan
            TimeSpanNull = TimeSpan.Zero;

        public static Guid
            GuidNull = Guid.Empty;

        #endregion Members

        #region Config

        public static bool IsDebug
        { get { return Val_Dbg; } }

        public static bool IsRelease
        { get { return !IsDebug; } }

        public static bool IsRunningLocal()
        { return Library.Windows.Windows.GetRunningApplicationPath().StartsWith(Path_Root_Loc); }

        public static string RootPath
        { get { return IsRunningLocal() ? Path_Root_Loc : Path_Root_Svr; } }

        public static string GetFileName(string sFileName)
        { return Path.Combine(RootPath, sFileName); }

        #endregion Config

    }
}
