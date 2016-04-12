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

namespace Thirst.Core.UnitTests.When_handling_inspect_services_message.with_one_expected_service
{
    [TestFixture]
    public class and_it_is_present : TestKit
    {
        private const string ExpectedProcessName = "ExpectedProcessName";
        private RunningServices runningServices;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var mockProcessService = new Mock<IProcessService>();

            mockProcessService.Setup(x => x.GetProcessNames()).Returns(new List<string> { ExpectedProcessName });

            var agentActor = Sys.ActorOf(Props.Create(() => new AgentActor(mockProcessService.Object)));

            agentActor.Tell(new InspectServices(new List<string> { ExpectedProcessName }));

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
            runningServices.ServiceNames.Count().Should().Be(1);
        }

        [Test]
        public void the_message_should_have_expected_service_name()
        {
            runningServices.ServiceNames.Contains(ExpectedProcessName).Should().BeTrue();
        }
    }
}
