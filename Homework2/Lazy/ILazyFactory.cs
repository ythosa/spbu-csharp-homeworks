namespace Homework2.Lazy;

public interface ILazyFactory<out T>
{
    ILazy<T> CreateUnsafeLazy();

    ILazy<T> CreateThreadSafeLazy();
}
