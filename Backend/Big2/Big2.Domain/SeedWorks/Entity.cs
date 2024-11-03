namespace Big2.Domain.SeedWorks;

public abstract class Entity
{
    Guid _id;

    public virtual Guid Id
    {
        get
        {
            return _id;
        }
        protected init
        {
            _id = value;
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Entity)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        Entity item = (Entity)obj;

        return item.Id == Id;
    }

    public static bool operator ==(Entity? left, Entity? right)
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Entity? left, Entity? right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
