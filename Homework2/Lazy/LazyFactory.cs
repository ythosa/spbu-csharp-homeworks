namespace Homework2.Lazy;

public class LazyFactory<T> : ILazyFactory<T>
{
    private readonly Func<T> _initializer;

    public LazyFactory(Func<T> initializer)
    {
        _initializer = initializer;
    }

    public ILazy<T> CreateUnsafeLazy()
    {
        return new UnsafeLazy<T>(_initializer);
    }

    public ILazy<T> CreateThreadSafeLazy()
    {
        return new ThreadSafeLazy<T>(_initializer);
    }
}
