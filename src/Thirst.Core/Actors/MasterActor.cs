using System.Linq;
using Akka.Actor;
using Akka.Event;
using Thirst.Core.Messages;

namespace Thirst.Core.Actors
{
    public class MasterActor : ReceiveActor
    {
        private readonly ILoggingAdapter logger = Context.GetLogger();

        private readonly IActorRef masterBroadcaster;

        public MasterActor(IActorRef masterBroadcaster)
        {
            this.masterBroadcaster = masterBroadcaster;
            Context.Watch(this.masterBroadcaster);

            Receive<RunningServices>(m =>
            {
                logger.Info("Hostname: {0}, Services: {1}", m.HostName, m.ServiceNames.Aggregate((x, y) => x + ", " + y));
            });
        }

        protected override void PostStop()
        {
            Context.Stop(masterBroadcaster);

            base.PostStop();
        }
    }
}
