namespace dddGym.Domain;

public class Session
{
    private readonly int _maxNumberOfParticipants;
    private readonly Guid _id;
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = [];
    private readonly Guid _roomId;

    public Session(int maxNumberOfParticipants, Guid trainerId, Guid? id = null)
    {
        _maxNumberOfParticipants = maxNumberOfParticipants;
        _trainerId = trainerId;
        _id = id ?? Guid.NewGuid();
    }


    public void ReserveSpot(Participant participant)
    {
        if (_participantIds.Count >= _maxNumberOfParticipants)
            throw new Exception("Cannot have more reservations.");

        _participantIds.Add(participant.Id);
    }
}