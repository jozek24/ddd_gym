using dddGym.Domain.Common.ValueObjects;
using ErrorOr;

namespace dddGym.Domain.Common.Entities;

public class Schedule : Entity
{
    private readonly Dictionary<DateOnly, List<TimeRange>> _callendar = new();

    public Schedule(Dictionary<DateOnly, List<TimeRange>>? callendar = null, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        _callendar = callendar ?? new();
    }

    public static Schedule Empty()
    {
        return new Schedule(id: Guid.NewGuid());
    }

    internal ErrorOr<Success> BookTimeSlot(DateOnly date, TimeRange timeRange)
    {
        if (!_callendar.TryGetValue(date, out var timeSlots))
        {
            _callendar[date] = [timeRange];

            return Result.Success;
        }

        if (timeSlots.Any(timeSlot => timeSlot.OverlapsWith(timeRange)))
        {
            return Error.Conflict();
        }

        timeSlots.Add(timeRange);

        return Result.Success;
    }
}