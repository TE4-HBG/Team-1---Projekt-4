public class Reference<T> where T : struct
{
    private T value;
    public void Set(T value) => this.value = value;
    private Reference(T value)
    {
        this.value = value;
    }

    public static implicit operator T(Reference<T> reference) => reference.value;
    public static implicit operator Reference<T>(T value) => new Reference<T>(value);
}
