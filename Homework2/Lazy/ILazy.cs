namespace Homework2.Lazy;

public interface ILazy<out T>
{
    T? Get();
}
