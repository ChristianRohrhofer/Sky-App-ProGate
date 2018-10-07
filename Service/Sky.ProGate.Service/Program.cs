
using System;
using System.ServiceProcess;


namespace Sky.ProGate.Service
{
    static class Program
    {
        static void Main()
        {
            ServiceBase[] ServicesToRun;

            //--- Run the service
            ServicesToRun = new ServiceBase[] { new Service() };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
