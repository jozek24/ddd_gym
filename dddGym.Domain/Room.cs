﻿using ErrorOr;

namespace dddGym.Domain;

public class Room
{
    public Guid Id { get; }
    private readonly List<Guid> _sessionIds = [];
    private readonly int _maxDailySessions;
    private readonly Guid _gymId;
    private readonly Schedule _schedule = Schedule.Empty();

    public Room(int maxDailySessions, Guid gymId, Schedule? schedule = null, Guid? id = null)
    {
        _maxDailySessions = maxDailySessions;
        _gymId = gymId;
        _schedule = schedule ?? Schedule.Empty();
        Id = id ?? Guid.NewGuid();
    }

    public ErrorOr<Success> ScheduleSession(Session session)
    {
        if (_sessionIds.Any(id => id == session.Id))
        {
            return Error.Conflict(description: "Session already exists in room");
        }

        if (_sessionIds.Count >= _maxDailySessions)
        {
            return RoomErrors.CannotHaveMoreSessionsThanSubscriptionAllows;
        }

        var addEventResult = _schedule.BookTimeSlot(session.Date, session.Time);

        if (addEventResult.IsError && addEventResult.FirstError.Type == ErrorType.Conflict)
        {
            return RoomErrors.CannotHaveTwoOrMoreOverlappingSessions;
        }

        _sessionIds.Add(session.Id);

        return Result.Success;
    }
}