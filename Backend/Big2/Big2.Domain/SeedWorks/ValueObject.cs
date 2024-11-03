namespace Big2.Domain.SeedWorks;
public abstract class ValueObject
{
    /// <summary>
    /// 核心比較方法，列舉每個屬性
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<object> GetEqualityComponents();

    // 覆寫 Equals 方法
    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not ValueObject)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        ValueObject other = (ValueObject)obj;

        return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public static bool operator ==(ValueObject? left, ValueObject? right)
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

    // 重載 != 運算子
    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }

    // 覆寫 GetHashCode 方法
    public override int GetHashCode()
    {
        return this.GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    public ValueObject? GetCopy()
    {
        return this.MemberwiseClone() as ValueObject;
    }
}
