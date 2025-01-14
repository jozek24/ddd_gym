using dddGym.Domain.UnitTests.TestUtilities.Participants;
using dddGym.Domain.UnitTests.TestUtilities.Sessions;
using FluentAssertions;

namespace dddGym.Domain.UnitTests
{
    public class SessionTests
    {
        [Fact]
        public void ReserveSpot_WhenNoMoreRooms_ShouldFailReservation()
        {
            var session = SessionFactory.CreateSession(1);
            var participant1 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());
            var participant2 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());
            
            session.ReserveSpot(participant1);
            var action = () => session.ReserveSpot(participant2);

            action.Should().ThrowExactly<Exception>();
        }
    }
}