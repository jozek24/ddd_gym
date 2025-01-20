using dddGym.Domain.UnitTests.TestConstants;

namespace dddGym.Domain.UnitTests.TestUtilities.Rooms;

public static class RoomFactory
{
    public static Room CreateRoom(
        int maxDailySessions = Constants.Rooms.MaxDailySessions,
        Guid? gymId = null,
        Guid? id = null)
    {
        return new Room(
            maxDailySessions: maxDailySessions,
            gymId: gymId ?? Constants.Gym.Id,
            id: id ?? Constants.Rooms.Id);
    }
}