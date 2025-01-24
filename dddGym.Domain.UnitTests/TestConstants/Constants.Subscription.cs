using dddGym.Domain.SubscriptionAggregate;

namespace dddGym.Domain.UnitTests.TestConstants;

public partial class Constants
{
    public class Subscription
    {
        public static readonly SubscriptionType DefaultSubscriptionType = SubscriptionType.Free;
        public static readonly Guid Id = Guid.NewGuid();
        public const int MaxDailySessionsFreeTier = 4;
        public const int MaxRoomsFreeTier = 1;
        public const int MaxGymsFreeTier = 1;
    }
}