
using System;
using System.Collections.Generic;
using System.Globalization;
using Sky.Library.SBM;
using Sky.ProGate.Application.Objects;


namespace Sky.ProGate.Application.Objects
{
    public class LineItem : SBMObject
    {
        #region Members

        internal const string
            TabName_LnItem = "USR_LINE_ITEMS";

        public LineItem()
            : base()
        { }

        public LineItem(Application App, AuxiliaryItem Item)
            : base(App, Item)
        { }

        #endregion Members
    }
}
