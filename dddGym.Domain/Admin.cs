namespace dddGym.Domain;

public class Admin
{
    private readonly Guid _id;
    private readonly Guid _userId;
    private readonly Guid _subscriptionId;
    private readonly bool _isSubscriptionActive;
    private readonly List<Guid> _gymIds;
}