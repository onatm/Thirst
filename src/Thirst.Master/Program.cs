using Topshelf;

namespace Thirst.Master
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsWindowsService();
        }

        private static void RunAsWindowsService()
        {
            HostFactory.Run(x =>
            {
                x.SetServiceName("Master");
                x.SetDisplayName("Thirst Master");
                x.SetDescription("Thirst Process Monitoring - Thirst Master.");

                x.UseAssemblyInfoForServiceInfo();
                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.Service<MasterService>();
                x.EnableServiceRecovery(r => r.RestartService(1));
            });
        }
    }
}
