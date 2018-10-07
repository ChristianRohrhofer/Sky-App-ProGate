
using System;
using Sky.ProGate.Application.Objects;


namespace Sky.ProGate.Service.Objects
{
    public class ProgressMessage
    {
        protected const string
            Pre_Para = "ProGate.Service.Message",
            Sep_Para = "\t";

        protected const int
            Cnt_Para = 8;

        protected const string
            ErrMsg_InvFileFmt = "Exception in the Task Progress Message object: invalid file format '%txt%'";

        public enum enRunState { None = 0, Start = 1, Step = 2, Finish = 3 }
        public enum enMessageState { None = 0, Info = 1, Warning = 2, Error = 3, Exception = 4 }

        public int TaskID { get; protected set; } = Global.IntegerNull;
        public enRunState RunState { get; set; } = enRunState.None;
        public enMessageState MessageState { get; set; } = enMessageState.None;
        public string Text { get; set; } = Global.StringNull;
        public int Step { get; protected set; } = Global.IntegerNull;
        public int Count { get; protected set; } = Global.IntegerNull;
        public DateTime TimeStamp { get; set; } = Global.DateTimeNull;

        public ProgressMessage
                        (
                            int nTskID, 
                            enRunState RunSta = enRunState.None, 
                            enMessageState MsgSta = enMessageState.None, 
                            string sTxt = null, 
                            int nStep = Global.IntegerNull, 
                            int nCnt = Global.IntegerNull
                        )
        { Init(nTskID, RunSta, MsgSta, sTxt, nStep, nCnt); }

        public ProgressMessage(int nTskID, Exception Exc)
        { Init(nTskID, enRunState.None, enMessageState.Exception, Exc.Message); }

        public ProgressMessage(string sParaTxt)
        { FromText(sParaTxt); }

        protected void Init(int nTskID, enRunState RunSta, enMessageState MsgSta, string sTxt, int nStep = Global.IntegerNull, int nCnt = Global.IntegerNull)
        {
            //--- Set the parameters
            TaskID = nTskID;
            RunState = RunSta;
            MessageState = MsgSta;
            Text = sTxt;
            Step = nStep;
            Count = nCnt;
            TimeStamp = DateTime.Now;
        }

        public void FromText(string sTxt)
        {
            string[] ParaArr = null;

            try
            {
                //--- Parse the messages parameters
                ParaArr = sTxt.Split(new string[] { Sep_Para }, StringSplitOptions.None);

                //--- Check the parameter count
                if (ParaArr.Length >= Cnt_Para ? Convert.ToString(ParaArr[0]) != Pre_Para : true)
                    throw new Exception();

                //--- Get the parameters
                TaskID = Convert.ToInt32(ParaArr[1]);
                RunState = (enRunState)Convert.ToInt32(ParaArr[2]);
                MessageState = (enMessageState)Convert.ToInt32(ParaArr[3]);
                Text = Convert.ToString(ParaArr[4]);
                Step = Convert.ToInt32(ParaArr[5]);
                Count = Convert.ToInt32(ParaArr[6]);
                TimeStamp = DateTime.ParseExact(Convert.ToString(ParaArr[7]), Global.DateTimeValueFormat, null);
            }
            catch (Exception Exc)
            { throw new Exception(ErrMsg_InvFileFmt.Replace("%txt%", sTxt), Exc); }
        }

        public string ToText()
        {
            //--- Join the string
            return String.Join<string>
                        (
                            Sep_Para,
                            new string[]
                                {
                                    Pre_Para,
                                    TaskID.ToString(),
                                    ((int)RunState).ToString(),
                                    ((int)MessageState).ToString(),
                                    !String.IsNullOrEmpty(Text) ? Text.Replace(Library.Text.Char.NewLine, Library.Text.Char.Space.ToString()) : null,
                                    Step.ToString(),
                                    Count.ToString(),
                                    TimeStamp.ToString(Global.DateTimeValueFormat)
                                }
                        );
        }
    }
}

