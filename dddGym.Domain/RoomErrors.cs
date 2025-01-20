using ErrorOr;

namespace dddGym.Domain;

public class RoomErrors
{
    public readonly static Error CannotHaveMoreSessionsThanSubscriptionAllows = Error.Validation(
    code: "Gym.CannotHaveMoreSessions",
    description: "A room cannot have more sessions than the subscription allows");
}