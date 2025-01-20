using ErrorOr;

namespace dddGym.Domain;

public class Schedule
{
    private readonly Guid _id;
    private readonly Dictionary<DateOnly, List<TimeRange>> _callendar = new();

    public Schedule(Guid? id = null, Dictionary<DateOnly, List<TimeRange>>? callendar = null)
    {
        _id = id ?? Guid.NewGuid();
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

    private Schedule() { }
}