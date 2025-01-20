namespace dddGym.Domain.UnitTests.TestConstants;

public static partial class Constants
{
    public static class Rooms
    {
        public static readonly Guid Id = Guid.NewGuid();
        public const int MaxDailySessions = Subscription.MaxDailySessionsFreeTier;
    }
}