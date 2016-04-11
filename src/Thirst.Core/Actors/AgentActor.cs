using System;
using System.Linq;
using Akka.Actor;
using Thirst.Core.Messages;
using Thirst.Core.Services;

namespace Thirst.Core.Actors
{
    public class AgentActor : ReceiveActor
    {
        public AgentActor(IProcessService processService)
        {
            Receive<InspectServices>(m =>
            {
                var processes = processService.GetProcessNames();

                var requestedServices = processes.Intersect(m.ServiceNames).ToList();

                var runningServices = new RunningServices(Environment.MachineName, requestedServices);

                Sender.Tell(runningServices);
            });
        }
    }
}
