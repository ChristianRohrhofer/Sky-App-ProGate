
using System;
using System.ServiceProcess;
using Sky.ProGate.Service.Objects;


namespace Sky.ProGate.Service
{
    partial class Service : ServiceBase
    {
        #region Members

        protected ServiceTarget ServiceTarget { get; set; } = new ServiceTarget();

        public Service()
        { InitializeComponent(); }

        #endregion Members

        #region EventHandlers

        protected override void OnStart(string[] ArgArr)
        { ServiceTarget.Start(); }

        protected override void OnStop()
        { ServiceTarget.Stop(); }

        #endregion EventHandlers
    }
}

