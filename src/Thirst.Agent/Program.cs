using Topshelf;

namespace Thirst.Agent
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
                x.SetServiceName("Agent");
                x.SetDisplayName("Thirst Agent");
                x.SetDescription("Thirst Process Monitoring - Thirst Agent.");

                x.UseAssemblyInfoForServiceInfo();
                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.Service<AgentService>();
                x.EnableServiceRecovery(r => r.RestartService(1));
            });
        }
    }
}
