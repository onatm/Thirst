using Akka.Actor;

namespace Thirst.Web.Actors
{
    public static class SystemActors
    {
        public static IActorRef MasterActor = ActorRefs.NoSender;
    }
}
