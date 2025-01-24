using dddGym.Domain.Common;
using dddGym.Domain.Common.ValueObjects;
using dddGym.Domain.ParticipantAggregate;
using ErrorOr;

namespace dddGym.Domain.SessionRoot;

public class Session : AggregateRoot
{
    private readonly int _maxParticipants;
    private readonly Guid _trainerId;
    private readonly List<Reservation> _reservations = [];
    private readonly Guid _roomId;
    public DateOnly Date { get; }
    public TimeRange Time { get; }

    public Session(DateOnly date, TimeRange time, int maxParticipants, Guid trainerId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Date = date;
        Time = time;
        _maxParticipants = maxParticipants;
        _trainerId = trainerId;
    }

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_reservations.Count >= _maxParticipants)
            return SessionErrors.CannotHaveMoreReservationsThanParticipants;

        if (_reservations.Any(reservation => reservation.ParticipantId == participant.Id))
            return Error.Conflict(description: "Participants cannot reserve twice to the same session");

        _reservations.Add(new Reservation(participant.Id));

        return Result.Success;
    }

    public ErrorOr<Success> CancellReservation(Participant participant, IDateTimeProvider dateTimeProvider)
    {
        if (IsTooCloseToSession(dateTimeProvider.DateTimeUtcNow))
            return SessionErrors.CannotCancelReservationTooCloseToSession;

        var reservation = _reservations.Find(reservation => reservation.ParticipantId == participant.Id);
        if (reservation is null)
            return Error.NotFound("Participant not found");

        _reservations.Remove(reservation);

        return Result.Success;
    }

    private bool IsTooCloseToSession(DateTime dateTimeUtcNow)
    {
        const int MinHours = 24;

        return (Date.ToDateTime(Time.Start) - dateTimeUtcNow).TotalHours < MinHours;
    }
}