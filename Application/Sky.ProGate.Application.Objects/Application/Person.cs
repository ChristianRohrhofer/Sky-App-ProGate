
using System;
using System.Collections.Generic;
using System.Globalization;
using Sky.Library.SBM;
using Sky.ProGate.Application.Objects;


namespace Sky.ProGate.Application.Objects
{
    public class Person : SAPObject
    {
        #region Members

        internal const string
            TabName_Per = "USR_PROCAP_PERSONS";

        public Person()
            : base()
        { }

        public Person(Application App, AuxiliaryItem Item)
            : base(App, Item)
        { }

        #endregion Members
    }
}
