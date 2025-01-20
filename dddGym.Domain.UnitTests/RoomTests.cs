using dddGym.Domain.UnitTests.TestUtilities.Rooms;
using dddGym.Domain.UnitTests.TestUtilities.Sessions;
using FluentAssertions;

namespace dddGym.Domain.UnitTests;

public class RoomTests
{
    [Fact]
    public void AddSession_WhenSubscriptionNotAllowsMoreSessions_ShouldFail()
    {
        // Arrange
        var room = RoomFactory.CreateRoom(maxDailySessions: 1);
        var session1 = SessionFactory.CreateSession(id: Guid.NewGuid());
        var session2 = SessionFactory.CreateSession(id: Guid.NewGuid());

        // Act
        var addSessionResult1 = room.AddSession(session1);
        var addSessionResult2 = room.AddSession(session2);

        // Assert
        addSessionResult1.IsError.Should().BeFalse();

        addSessionResult2.IsError.Should().BeTrue();
        addSessionResult2.FirstError.Should().Be(RoomErrors.CannotHaveMoreSessionsThanSubscriptionAllows);
    }
}