using dddGym.Domain.UnitTests.TestConstants;

namespace dddGym.Domain.UnitTests.TestUtilities.Gyms;

public static class GymFactory
{
    public static Gym CreateGym(int maxRooms = Constants.Subscription.MaxRoomsFreeTier, Guid? id = null)
    {
        return new Gym(maxRooms, id ?? Constants.Gym.Id);
    }
}