using ErrorOr;

namespace dddGym.Domain;

public class Session
{
    private readonly DateOnly _date;
    private readonly TimeOnly _startTime;
    private readonly TimeOnly _endTime;
    private readonly int _maxParticipants;
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = [];
    private readonly Guid _roomId;
    public Guid Id { get; }

    public Session(DateOnly date, TimeOnly startTime, TimeOnly endTime, int maxParticipants, Guid trainerId, Guid? id = null)
    {
        _date = date;
        _startTime = startTime;
        _endTime = endTime;
        _maxParticipants = maxParticipants;
        _trainerId = trainerId;
        Id = id ?? Guid.NewGuid();
    }

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_participantIds.Count >= _maxParticipants)
            return SessionErrors.CannotHaveMoreReservationsThanParticipants;

        if (_participantIds.Contains(participant.Id))
            return Error.Conflict(description: "Participants cannot reserve twice to the same session");

        _participantIds.Add(participant.Id);

        return Result.Success;
    }

    public ErrorOr<Success> CancellReservation(Participant participant, IDateTimeProvider dateTimeProvider)
    {
        if (IsTooCloseToSession(dateTimeProvider.DateTimeUtcNow))
            return SessionErrors.CannotCancelReservationTooCloseToSession;

        if (!_participantIds.Remove(participant.Id))
            return Error.NotFound("Participant not found");

        return Result.Success;
    }

    private bool IsTooCloseToSession(DateTime dateTimeUtcNow)
    {
        const int MinHours = 24;

        return (_date.ToDateTime(_startTime) - dateTimeUtcNow).TotalHours < MinHours;
    }
}