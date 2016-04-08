using System;
using System.Diagnostics;
using System.Linq;
using Akka.Actor;
using Thirst.Shared.Messages;

namespace Thirst.Shared.Actors
{
    public class AgentActor : ReceiveActor
    {
        public AgentActor()
        {
            Receive<InspectServices>(m =>
            {
                var processes = Process.GetProcesses();

                var requestedServices = processes.Select(p => p.ProcessName).Intersect(m.ServiceNames).ToList();

                var runningServices = new RunningServices(Environment.MachineName, requestedServices);

                Sender.Tell(runningServices);
            });
        }
    }
}
