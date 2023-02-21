namespace EasyRent.EventSourcing;

public abstract record ValueObject;

public abstract record ValueObject<T>
{
    public T Value { get; init; }

    public static implicit operator T(ValueObject<T> instance) => instance.Value;
}