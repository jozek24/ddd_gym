using Throw;

namespace dddGym.Domain;

public class TimeRange
{
    private readonly TimeOnly _start;
    private readonly TimeOnly _end;

    public TimeRange(TimeOnly start, TimeOnly end)
    {
        _start = start.Throw().IfGreaterThanOrEqualTo(end);
        _end = end;
    }
}