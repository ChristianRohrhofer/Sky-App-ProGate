
using System;
using System.Collections.Generic;
using System.Globalization;
using Sky.Library.SBM;
using Sky.ProGate.Application.Objects;


namespace Sky.ProGate.Application.Objects
{
    public class Category : SBMObject
    {
        #region Members

        internal const string
            TabName_Cat = "USR_CATEGORIES";

        public Category()
            : base()
        { }

        public Category(Application App, AuxiliaryItem Item)
            : base(App, Item)
        { }

        #endregion Members
    }
}
