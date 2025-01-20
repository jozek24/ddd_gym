﻿using ErrorOr;

namespace dddGym.Domain;

public class Gym
{
    private readonly Guid _id;
    private readonly List<Guid> _roomIds = [];
    private readonly int _maxRooms;

    public Gym(int maxRooms, Guid? id = null)
    {
        _maxRooms = maxRooms;
        _id = id ?? Guid.NewGuid();
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