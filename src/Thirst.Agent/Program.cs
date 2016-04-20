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
                x.SetServiceName("Thirst.Agent");
                x.SetDisplayName("Thirst Agent - Agent Node");
                x.SetDescription("Thirst process monitoring tool");

                x.UseAssemblyInfoForServiceInfo();
                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.Service<AgentService>();
                x.EnableServiceRecovery(r => r.RestartService(1));
            });
        }
    }
}
