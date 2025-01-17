namespace dddGym.Domain;

public interface IDateTimeProvider
{
    public DateTime DateTimeUtcNow { get; }
}