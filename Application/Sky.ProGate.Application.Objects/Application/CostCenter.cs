
using System;
using System.Collections.Generic;
using System.Globalization;
using Sky.Library.SBM;
using Sky.ProGate.Application.Objects;


namespace Sky.ProGate.Application.Objects
{
    public class CostCenter : SAPObject
    {
        #region Members

        internal const string
            TabName_CosCen = "USR_PROCAP_COSTCENTERS";

        public CostCenter()
            : base()
        { }

        public CostCenter(Application App, AuxiliaryItem Item)
            : base(App, Item)
        { }

        #endregion Members
    }
}
