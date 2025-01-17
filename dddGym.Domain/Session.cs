namespace dddGym.Domain;

public class Session
{
    private readonly DateOnly _date;
    private readonly TimeOnly _startTime;
    private readonly TimeOnly _endTime;
    private readonly int _maxParticipants;
    private readonly Guid _id;
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = [];
    private readonly Guid _roomId;

    public Session(DateOnly date, TimeOnly startTime, TimeOnly endTime, int maxParticipants, Guid trainerId, Guid? id = null)
    {
        _date = date;
        _startTime = startTime;
        _endTime = endTime;
        _maxParticipants = maxParticipants;
        _trainerId = trainerId;
        _id = id ?? Guid.NewGuid();
    }

    public void ReserveSpot(Participant participant)
    {
        if (_participantIds.Count >= _maxParticipants)
            throw new Exception("Cannot have more reservations");

        _participantIds.Add(participant.Id);
    }

    public void CancellSpotReservation(Participant participant, IDateTimeProvider dateTimeProvider)
    {
        if (IsTooCloseToSession(dateTimeProvider.DateTimeUtcNow))
            throw new Exception("Cannot cancell reservations. Too close to session");

        if (!_participantIds.Remove(participant.Id))
            throw new Exception("Reservation not found");
    }

    private bool IsTooCloseToSession(DateTime dateTimeUtcNow)
    {
        const int MinHours = 24;

        return (_date.ToDateTime(_startTime) - dateTimeUtcNow).TotalHours < MinHours;
    }
}