using dddGym.Domain.RoomAggregate;
using dddGym.Domain.UnitTests.TestConstants;
using dddGym.Domain.UnitTests.TestUtilities.Common;
using dddGym.Domain.UnitTests.TestUtilities.Rooms;
using dddGym.Domain.UnitTests.TestUtilities.Sessions;
using FluentAssertions;

namespace dddGym.Domain.UnitTests.RoomAggregate;

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
        var addSessionResult1 = room.ScheduleSession(session1);
        var addSessionResult2 = room.ScheduleSession(session2);

        // Assert
        addSessionResult1.IsError.Should().BeFalse();

        addSessionResult2.IsError.Should().BeTrue();
        addSessionResult2.FirstError.Should().Be(RoomErrors.CannotHaveMoreSessionsThanSubscriptionAllows);
    }

    [Theory]
    [InlineData(1, 3, 1, 3)] // exact overlap
    [InlineData(1, 3, 2, 3)] // second session inside first session
    [InlineData(1, 3, 2, 4)] // second session ends after session, but overlaps
    [InlineData(1, 3, 0, 2)] // second session starts before second session, but overlaps
    public void ScheduleSession_WhenSessionOverlapsWithAnotherSession_ShouldFail(
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2)
    {
        // Arrange
        var room = RoomFactory.CreateRoom(
            maxDailySessions: 2);

        var session1 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1),
            id: Guid.NewGuid());

        var session2 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2),
            id: Guid.NewGuid());

        // Act
        var scheduleSession1Result = room.ScheduleSession(session1);
        var scheduleSession2Result = room.ScheduleSession(session2);

        // Assert
        scheduleSession1Result.IsError.Should().BeFalse();

        scheduleSession2Result.IsError.Should().BeTrue();
        scheduleSession2Result.FirstError.Should().Be(RoomErrors.CannotHaveTwoOrMoreOverlappingSessions);
    }
}