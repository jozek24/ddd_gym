using dddGym.Domain.UnitTests.TestConstants;
using dddGym.Domain.UnitTests.TestUtilities.Gyms;
using dddGym.Domain.UnitTests.TestUtilities.Rooms;
using FluentAssertions;

namespace dddGym.Domain.UnitTests;

public class GymTests
{
    [Fact]
    public void AddRoom_WhenSubscriptionNotAllowsMoreRooms_ShouldFail()
    {
        // Arrange
        var gym = GymFactory.CreateGym(Constants.Subscription.MaxRoomsFreeTier);
        var room1 = RoomFactory.CreateRoom(id: Guid.NewGuid());
        var room2 = RoomFactory.CreateRoom(id: Guid.NewGuid());

        // Act
        var addRoomResult1 = gym.AddRoom(room1);
        var addRoomResult2 = gym.AddRoom(room2);

        // Assert
        addRoomResult1.IsError.Should().BeFalse();

        addRoomResult2.IsError.Should().BeTrue();
        addRoomResult2.FirstError.Should().Be(GymErrors.CannotHaveMoreRoomsThanSubscriptionAllows);
    }
}