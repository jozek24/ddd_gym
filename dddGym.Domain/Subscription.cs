using ErrorOr;

namespace dddGym.Domain;

public class Subscription
{
    private readonly List<Guid> _gymIds = [];
    private readonly Guid _adminId;
    private readonly SubscriptionType _subscriptionType;
    private readonly Guid _id;
    private readonly int _maxGyms;

    public Subscription(
        Guid adminId,
        SubscriptionType subscriptionType,
        Guid? id = null)
    {
        _adminId = adminId;
        _subscriptionType = subscriptionType;
        _maxGyms = GetMaxGyms();
        _id = id ?? Guid.NewGuid();
    }

    public int GetMaxGyms() => _subscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 1,
        nameof(SubscriptionType.Pro) => 3,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxRooms() => _subscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 3,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxDailySessions() => _subscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 4,
        nameof(SubscriptionType.Starter) => int.MaxValue,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public ErrorOr<Success> AddGym(Gym gym)
    {
        if (_gymIds.Contains(gym.Id))
        {
            return Error.Conflict(description: "Gym already exists");
        }

        if (_gymIds.Count >= _maxGyms)
        {
            return SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows;
        }

        _gymIds.Add(gym.Id);

        return Result.Success;
    }
}