﻿namespace dddGym.Domain;

public class Room
{
    private readonly Guid _id;
    private readonly List<Guid> _sessionIds = [];
}