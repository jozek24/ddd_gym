using dddGym.Domain.UnitTests.TestConstants;
using dddGym.Domain.UnitTests.TestUtilities.Participants;
using dddGym.Domain.UnitTests.TestUtilities.Services;
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

        [Fact]
        public void CancellReservation_WhenCancellationIsTooCloseToSesssion_ShouldFailCancellation()
        {
            // Arrange
            var session = SessionFactory.CreateSession(1);
            var participant = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());
            session.ReserveSpot(participant);

            var cancellationDateTime = Constants.Session.Date.ToDateTime(TimeOnly.MinValue);

            // Act
            var action = () => session.CancellSpotReservation(
                participant, 
                new TestDateTimeProvider(fixedDateTime: cancellationDateTime));

            // Assert
            action.Should().Throw<Exception>();
        }
    }
}