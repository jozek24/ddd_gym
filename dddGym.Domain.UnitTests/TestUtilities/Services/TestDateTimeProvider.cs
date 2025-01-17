namespace dddGym.Domain.UnitTests.TestUtilities.Services;

internal class TestDateTimeProvider : IDateTimeProvider
{
    private readonly DateTime? _fixedDateTime;

    public TestDateTimeProvider(DateTime? fixedDateTime)
    {
        _fixedDateTime = fixedDateTime;
    }

    public DateTime DateTimeUtcNow => _fixedDateTime ?? DateTime.UtcNow;
}