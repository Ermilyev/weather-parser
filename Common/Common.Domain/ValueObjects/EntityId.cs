namespace Common.Domain.ValueObjects;

public readonly struct EntityId : 
    IEquatable<EntityId>, 
    IEquatable<Guid>, 
    IComparable<EntityId>,
    IConvertible
{
    public Guid Value { get; }
    
    public EntityId()
    {
        Value = Guid.NewGuid();
    }
    
    public EntityId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException(nameof(Guid.Empty), nameof(value));
        }

        Value = value;
    }

    public override int GetHashCode() => Value.GetHashCode();
    
    public bool Equals(EntityId other) => Value == other.Value;

    public bool Equals(Guid other) => Value == other;

    public bool Equals(string? other) => other != null && Value == new Guid(other);

    public int CompareTo(EntityId other) => Value.CompareTo(other.Value);

    public TypeCode GetTypeCode() => Value.ToString().GetTypeCode();

    public bool ToBoolean(IFormatProvider? provider) => ((IConvertible)Value.ToString()).ToBoolean(provider);

    public byte ToByte(IFormatProvider? provider) => ((IConvertible)Value.ToString()).ToByte(provider);

    public char ToChar(IFormatProvider? provider) => ((IConvertible)Value.ToString()).ToChar(provider);

    public DateTime ToDateTime(IFormatProvider? provider) => ((IConvertible)Value.ToString()).ToDateTime(provider);

    public decimal ToDecimal(IFormatProvider? provider) => ((IConvertible)Value.ToString()).ToDecimal(provider);

    public double ToDouble(IFormatProvider? provider) => ((IConvertible)Value.ToString()).ToDouble(provider);

    public short ToInt16(IFormatProvider? provider) => ((IConvertible)Value.ToString()).ToInt16(provider);

    public int ToInt32(IFormatProvider? provider) => ((IConvertible)Value.ToString()).ToInt32(provider);

    public long ToInt64(IFormatProvider? provider) => ((IConvertible)Value.ToString()).ToInt64(provider);

    public sbyte ToSByte(IFormatProvider? provider) => ((IConvertible)Value.ToString()).ToSByte(provider);

    public float ToSingle(IFormatProvider? provider) => ((IConvertible)Value.ToString()).ToSingle(provider);

    public string ToString(IFormatProvider? provider) => Value.ToString();

    public object ToType(Type conversionType, IFormatProvider? provider)  => ((IConvertible)Value.ToString()).ToType(conversionType, provider);

    public ushort ToUInt16(IFormatProvider? provider) => ((IConvertible)Value.ToString()).ToUInt16(provider);

    public uint ToUInt32(IFormatProvider? provider) => ((IConvertible)Value.ToString()).ToUInt32(provider);

    public ulong ToUInt64(IFormatProvider? provider) => ((IConvertible)Value.ToString()).ToUInt64(provider);

    public static bool operator ==(EntityId left, EntityId right) => left.Value == right.Value;

    public static bool operator !=(EntityId left, EntityId right) => left.Value != right.Value;
    
    public static implicit operator Guid(in EntityId entity) => entity.Value;
    
    public static implicit operator EntityId(Guid id) => new(id);
}