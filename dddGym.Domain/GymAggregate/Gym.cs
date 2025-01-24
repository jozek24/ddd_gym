using dddGym.Domain.Common;
using dddGym.Domain.RoomAggregate;
using ErrorOr;

namespace dddGym.Domain.GymAggregate;

public class Gym : AggregateRoot
{
    private readonly List<Guid> _roomIds = [];
    private readonly int _maxRooms;

    public Gym(int maxRooms, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        _maxRooms = maxRooms;
    }

    public ErrorOr<Success> AddRoom(Room room)
    {
        if (_roomIds.Contains(room.Id))
            return Error.Conflict(description: "Room already exists in gym");

        if (_roomIds.Count() >= _maxRooms)
            return GymErrors.CannotHaveMoreRoomsThanSubscriptionAllows;

        _roomIds.Add(room.Id);

        return Result.Success;
    }
}