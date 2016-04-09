using System;
using Nancy.Hosting.Self;

namespace Thirst.Web
{
    public interface IHostService
    {
        void Start();
        void Stop();
    }

    public class HostService : IHostService, IDisposable
    {
        private readonly NancyHost nancyHost;

        public HostService(NancyHost nancyHost)
        {
            this.nancyHost = nancyHost;
        }

        public void Start()
        {
            nancyHost.Start();
        }

        public void Stop()
        {
            nancyHost.Stop();
        }

        public void Dispose()
        {
            if (nancyHost != null)
            {
                nancyHost.Dispose();
            }
        }
    }
}
