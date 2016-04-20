using System.Collections.Generic;
using Akka.Actor;
using Akka.TestKit.NUnit;
using FluentAssertions;
using NUnit.Framework;
using Thirst.Core.Actors;
using Thirst.Core.Messages;

namespace Thirst.Core.UnitTests.Master
{
    [TestFixture, Ignore]
    public class When_handling_running_services_message : TestKit
    {
        private RunningServices runningServices;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var masterActor = Sys.ActorOf(Props.Create(() => new MasterActor(ActorRefs.Nobody)));

            masterActor.Tell(new InspectServices(new List<string>()));
            masterActor.Tell(new RunningServices(string.Empty, new List<string>()));

            runningServices = ExpectMsg<RunningServices>();
        }

        [Test]
        public void it_should_forward_the_message()
        {
            runningServices.Should().NotBeNull();
        }
    }
}
