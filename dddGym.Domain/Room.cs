using ErrorOr;

namespace dddGym.Domain;

public class Room
{
    public Guid Id { get; }
    private readonly List<Guid> _sessionIds = [];
    private readonly int _maxDailySessions;
    private readonly Guid _gymId;

    public Room(int maxDailySessions, Guid gymId, Guid? id = null)
    {
        _maxDailySessions = maxDailySessions;
        _gymId = gymId;
        Id = id ?? Guid.NewGuid();
    }

    public ErrorOr<Success> AddSession(Session session)
    {
        if (_sessionIds.Contains(session.Id))
            return Error.Conflict("Session already exists in room");

        if (_sessionIds.Count() >= _maxDailySessions)
            return RoomErrors.CannotHaveMoreSessionsThanSubscriptionAllows;

        _sessionIds.Add(session.Id);

        return Result.Success;
    }
}