
using System;
using Sky.Library.Xml;


namespace Sky.ProGate.Application.Objects
{
    public class ConfigFile : XmlTree
    {
        #region Members

        protected const string
            ErrMsg_InvValFmt = "Excpetion in the Xml object: invalid Xml value format '%val%' of Xml node path '%path%' in the config file '%file%'";

        public ConfigFile(string sFileName)
            : base(sFileName)
        { }

        #endregion Members

        #region Values

        protected T GetValue<T>(string sNodPath, bool bReq, T NullVal, Converter<string, T> Conv)
        {
            XmlNode Nod = null;
            T Val = default(T);

            //--- Get the node
            if ((Nod = GetNodeFromPath(sNodPath)) == null)
            {
                //--- Throw an exception if value required
                if (bReq)
                    throw new XmlInvalidNodePathException(this, sNodPath);

                //--- Set the null value                
                Val = NullVal;
            }
            //--- Check value not exist
            else if (String.IsNullOrEmpty(Nod.Value))
            {
                //--- Throw an exception if value required
                if (bReq)
                    throw new XmlNodeValueNullException(Nod);

                //--- Set the null value                
                Val = NullVal;
            }
            else
            {
                //--- Convert the json value
                try
                { Val = Conv(Nod.Value); }
                catch (Exception Exc)
                {
                    throw new Exception
                            (
                                ErrMsg_InvValFmt
                                            .Replace("%val%", Nod.Value.ToString())
                                            .Replace("%path%", sNodPath)
                                            .Replace("%file%", FileName), Exc
                            );
                }
            }

            return Val;
        }

        public int GetInteger(string sNodPath, bool bReq = false)
        { return GetValue(sNodPath, bReq, Global.IntegerNull, Convert.ToInt32); }

        public double GetDouble(string sNodPath, bool bReq = false)
        { return GetValue(sNodPath, bReq, Global.DoubleNull, Convert.ToDouble); }

        public bool GetBoolean(string sNodPath, bool bReq = false)
        { return GetValue(sNodPath, bReq, Global.BooleanNull, Convert.ToBoolean); }

        public string GetString(string sNodPath, bool bReq = false)
        { return GetValue(sNodPath, bReq, Global.StringNull, Convert.ToString); }

        public DateTime GetDateTime(string sNodPath, bool bReq = false)
        { return GetValue(sNodPath, bReq, Global.DateTimeNull, ConvertToDateTime); }

        public TimeSpan GetTimeSpan(string sNodPath, bool bReq = false)
        { return GetValue(sNodPath, bReq, Global.TimeSpanNull, ConvertToTimeSpan); }

        public Guid GetGuid(string sNodPath, bool bReq = false)
        { return GetValue(sNodPath, bReq, Global.GuidNull, ConvertToGuid); }

        protected void SetValue<T>(string sNodPath, T Val)
        {
            XmlNode Nod = null;

            //--- Get or add the node path and set the string value
            Nod = (Nod = GetNodeFromPath(sNodPath)) == null ? AddNodeFromPath(sNodPath) : Nod;
            Nod.Value = Convert.ToString(Val);
        }

        public void SetInteger(string sNodPath, int nVal)
        { SetValue(sNodPath, nVal); }

        public void SetDouble(string sNodPath, double dVal)
        { SetValue(sNodPath, dVal); }

        public void SetBoolean(string sNodPath, bool bVal)
        { SetValue(sNodPath, bVal); }

        public void SetString(string sNodPath, string sVal)
        { SetValue(sNodPath, sVal); }

        public void SetDateTime(string sNodPath, DateTime dtVal)
        { SetString(sNodPath, dtVal.ToString(Global.DateTimeValueFormat)); }

        public void SetTimeSpan(string sNodPath, TimeSpan tsVal)
        { SetString(sNodPath, (tsVal.Days * 24 + tsVal.Hours).ToString("00") + tsVal.Minutes.ToString("00") + tsVal.Seconds.ToString("00")); }

        public void SetGuid(string sNodPath, Guid ID)
        { SetString(sNodPath, ID.ToString()); }

        #endregion Values

        #region Convert

        static DateTime ConvertToDateTime(string sVal)
        { return DateTime.ParseExact(sVal, Global.DateTimeValueFormat, null); }

        static TimeSpan ConvertToTimeSpan(string sVal)
        {
            try
            {
                return new TimeSpan
                            (
                                Convert.ToInt32(sVal.Substring(0, 2)),
                                Convert.ToInt32(sVal.Substring(2, 2)),
                                Convert.ToInt32(sVal.Substring(4, 2))
                            );
            }
            catch(Exception)
            { throw new InvalidCastException(); }
        }

        static Guid ConvertToGuid(string sVal)
        { return new Guid(sVal); }

        #endregion Convert

        #region File

        public void Update()
        { Write(); }

        #endregion File


    }
}
