using ErrorOr;

namespace dddGym.Domain;

public class Session
{
    private readonly int _maxParticipants;
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = [];
    private readonly Guid _roomId;
    public Guid Id { get; }
    public DateOnly Date { get; }
    public TimeRange Time { get; }

    public Session(DateOnly date, TimeRange time, int maxParticipants, Guid trainerId, Guid? id = null)
    {
        Date = date;
        Time = time;
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

        return (Date.ToDateTime(Time.Start) - dateTimeUtcNow).TotalHours < MinHours;
    }
}