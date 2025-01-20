using ErrorOr;

namespace dddGym.Domain;

public class GymErrors
{
    public readonly static Error CannotHaveMoreRoomsThanSubscriptionAllows = Error.Validation(
        code: "Gym.YouCannotHaveMoreRooms",
        description: "A gym cannot have more rooms than the subscription allows");
}