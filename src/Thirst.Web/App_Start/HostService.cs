using System;
using Microsoft.Owin.Hosting;

namespace Thirst.Web
{
    public interface IHostService
    {
        void Start(string[] baseUrls);
        void Stop();
    }

    public class HostService : IHostService, IDisposable
    {
        private IDisposable webApp;

        public void Start(string[] baseUrls)
        {
            var options = new StartOptions();

            foreach (var baseUrl in baseUrls)
            {
                options.Urls.Add(baseUrl);
            }

            webApp = WebApp.Start(options);
        }

        public void Stop()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (webApp != null)
            {
                webApp.Dispose();
            }
        }
    }
}
