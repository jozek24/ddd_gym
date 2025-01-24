﻿using dddGym.Domain.Common;

namespace dddGym.Domain.AdminAggregate;

public class Admin : AggregateRoot
{
    private readonly Guid _id;
    private readonly Guid _userId;
    private readonly Guid _subscriptionId;

    public Admin(
        Guid userId,
        Guid subscriptionId,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        _userId = userId;
        _subscriptionId = subscriptionId;
    }
}