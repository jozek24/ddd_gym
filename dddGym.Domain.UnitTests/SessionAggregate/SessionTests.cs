using dddGym.Domain.SessionRoot;
using dddGym.Domain.UnitTests.TestConstants;
using dddGym.Domain.UnitTests.TestUtilities.Participants;
using dddGym.Domain.UnitTests.TestUtilities.Services;
using dddGym.Domain.UnitTests.TestUtilities.Sessions;
using FluentAssertions;

namespace dddGym.Domain.UnitTests.SessionAggregate;

public class SessionTests
{
    [Fact]
    public void ReserveSpot_WhenNoMoreRooms_ShouldFailReservation()
    {
        // Arrange
        var session = SessionFactory.CreateSession(maxParticipants: 1);
        var participant1 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());
        var participant2 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());

        // Act
        var reserveSpotResult1 = session.ReserveSpot(participant1);
        var reserveSpotResult2 = session.ReserveSpot(participant2);

        // Assert
        reserveSpotResult1.IsError.Should().BeFalse();

        reserveSpotResult2.IsError.Should().BeTrue();
        reserveSpotResult2.FirstError.Should().Be(SessionErrors.CannotHaveMoreReservationsThanParticipants);
    }

    [Fact]
    public void CancellReservation_WhenCancellationIsTooCloseToSesssion_ShouldFailCancellation()
    {
        // Arrange
        var session = SessionFactory.CreateSession(maxParticipants: 1);
        var participant = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid());
        var reserveSpotResult = session.ReserveSpot(participant);

        var cancellationDateTime = Constants.Session.Date.ToDateTime(TimeOnly.MinValue);

        // Act
        var cancellReservationResult = session.CancellReservation(
            participant,
            new TestDateTimeProvider(fixedDateTime: cancellationDateTime));

        // Assert
        reserveSpotResult.IsError.Should().BeFalse();

        cancellReservationResult.IsError.Should().BeTrue();
        cancellReservationResult.FirstError.Should().Be(SessionErrors.CannotCancelReservationTooCloseToSession);
    }
}