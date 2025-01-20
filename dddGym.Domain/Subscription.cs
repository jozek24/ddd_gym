namespace dddGym.Domain;

public class Subscription
{
    private readonly List<Guid> _gymIds = [];
    private readonly Guid _adminId;
    private readonly SubscriptionType _subscriptionType;
    private readonly Guid _id;

    public Subscription(
        Guid adminId,
        SubscriptionType subscriptionType,
        Guid? id = null)
    {
        _adminId = adminId;
        _subscriptionType = subscriptionType;
        _id = id ?? Guid.NewGuid();
    }
}