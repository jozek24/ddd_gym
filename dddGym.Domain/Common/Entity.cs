namespace dddGym.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public override bool Equals(object? other)
    {
        if (other == null || other.GetType() != GetType())
            return false;

        return ((Entity)other).Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}