using System;
using System.Collections.Generic;
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

                IEnumerable<string> requestedServices;

                if (m.ServiceNames == null)
                {
                    requestedServices = new List<string>();
                }
                else
                {
                    requestedServices = processes.Intersect(m.ServiceNames);
                }

                var runningServices = new RunningServices(Environment.MachineName, requestedServices.ToList());

                Sender.Tell(runningServices);
            });
        }
    }
}
