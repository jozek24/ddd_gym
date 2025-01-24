using dddGym.Domain.SubscriptionAggregate;
using dddGym.Domain.UnitTests.TestConstants;

namespace dddGym.Domain.UnitTests.TestUtilities.Subscriptions;

public class SubscriptionFactory
{
    public static Subscription CreateSubscription(
        SubscriptionType? subscriptionType = null,
        Guid? adminId = null,
        Guid? id = null)
    {
        return new Subscription(
            adminId ?? Constants.Admin.Id,
            subscriptionType: subscriptionType ?? Constants.Subscription.DefaultSubscriptionType,
            id ?? Constants.Subscription.Id);
    }
}