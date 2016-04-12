using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.TestKit.NUnit;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Thirst.Core.Actors;
using Thirst.Core.Messages;
using Thirst.Core.Services;

namespace Thirst.Core.UnitTests.When_handling_inspect_services_message
{
    [TestFixture(true)]
    [TestFixture(false)]
    public class When_handling_empty_inspect_services_message : TestKit
    {
        private readonly IEnumerable<string> serviceNames;

        private RunningServices runningServices;

        public When_handling_empty_inspect_services_message(bool isNull = false)
        {
            if (!isNull)
            {
                serviceNames = new List<string>();
            }
        }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var mockProcessService = new Mock<IProcessService>();

            mockProcessService.Setup(x => x.GetProcessNames()).Returns(new List<string> { "RandomProcessName" });

            var agentActor = Sys.ActorOf(Props.Create(() => new AgentActor(mockProcessService.Object)));

            agentActor.Tell(new InspectServices(serviceNames));

            runningServices = ExpectMsg<RunningServices>();
        }

        [Test]
        public void it_should_send_running_services_message()
        {
            runningServices.Should().NotBeNull();
        }

        [Test]
        public void the_message_should_have_host_name()
        {
            runningServices.HostName.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void the_message_should_have_correct_running_services_count()
        {
            runningServices.ServiceNames.Count().Should().Be(0);
        }
    }
}
