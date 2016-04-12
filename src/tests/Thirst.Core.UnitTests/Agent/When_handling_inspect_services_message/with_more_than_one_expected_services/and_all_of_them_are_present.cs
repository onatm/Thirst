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

namespace Thirst.Core.UnitTests.When_handling_inspect_services_message.with_more_than_one_expected_services
{
    [TestFixture]
    public class and_all_of_them_are_present : TestKit
    {
        private const string ExpectedProcessName1 = "ExpectedProcessName1";
        private const string ExpectedProcessName2 = "ExpectedProcessName2";
        private RunningServices runningServices;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var mockProcessService = new Mock<IProcessService>();

            mockProcessService.Setup(x => x.GetProcessNames()).Returns(new List<string> { ExpectedProcessName1, ExpectedProcessName2 });

            var agentActor = Sys.ActorOf(Props.Create(() => new AgentActor(mockProcessService.Object)));

            agentActor.Tell(new InspectServices(new List<string> { ExpectedProcessName1, ExpectedProcessName2 }));

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
            runningServices.ServiceNames.Count().Should().Be(2);
        }

        [Test]
        public void the_message_should_have_first_expected_service_name()
        {
            runningServices.ServiceNames.Contains(ExpectedProcessName1).Should().BeTrue();
        }

        [Test]
        public void the_message_should_have_second_expected_service_name()
        {
            runningServices.ServiceNames.Contains(ExpectedProcessName2).Should().BeTrue();
        }
    }
}
