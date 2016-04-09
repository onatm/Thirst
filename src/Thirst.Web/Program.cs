using Nancy.Hosting.Self;
using Thirst.Web.Configuration;
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
                x.SetDisplayName("Thirst Web");
                x.SetDescription("Thirst Process Monitoring - Thirst Web.");

                x.Service<IHostService>(h =>
                {
                    var settings = WebSettings.Current();

                    var bootstrapper = new Bootstrapper();

                    var nancyHost = new NancyHost(bootstrapper, new HostConfiguration
                    {
                        UrlReservations = new UrlReservations
                        {
                            CreateAutomatically = true
                        },
                    }, settings.BaseUrlListArray());

                    h.ConstructUsing(() => new HostService(nancyHost));
                    h.WhenStarted(s => s.Start());
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
