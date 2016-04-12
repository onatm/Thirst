using Akka.Actor;
using Thirst.Core.Messages;

namespace Thirst.Core.Actors
{
    public class MasterActor : ReceiveActor
    {
        private readonly IActorRef masterBroadcaster;

        private IActorRef lastRequester;

        public MasterActor(IActorRef masterBroadcaster)
        {
            this.masterBroadcaster = masterBroadcaster;

            Context.Watch(masterBroadcaster);

            Receive<InspectServices>(m =>
            {
                lastRequester = Sender;
                masterBroadcaster.Tell(m);
            });

            Receive<RunningServices>(m =>
            {
                lastRequester.Tell(m);
            });
        }

        protected override void PostStop()
        {
            Context.Stop(masterBroadcaster);

            base.PostStop();
        }
    }
}
