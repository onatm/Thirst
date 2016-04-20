using Topshelf;

namespace Thirst.Web
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
                x.SetServiceName("Thirst.Web");
                x.SetDisplayName("Thirst Web - Seed Node");
                x.SetDescription("Thirst process monitoring tool");

                x.Service<IHostService>(h =>
                {
                    var settings = WebSettings.Current();

                    h.ConstructUsing(() => new HostService());
                    h.WhenStarted(s => s.Start(settings.BaseUrls.Split(',')));
                    h.WhenStopped(s => s.Stop());
                });

                x.UseAssemblyInfoForServiceInfo();
                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.EnableServiceRecovery(r => r.RestartService(1));
            });
        }
    }
}
