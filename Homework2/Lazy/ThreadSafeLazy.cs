namespace Homework2.Lazy;

public class ThreadSafeLazy<T> : Lazy<T>, ILazy<T>
{
    private ComputationResult<T>? _computationResult;
    private readonly Mutex _mutex = new();

    public ThreadSafeLazy(Func<T> initializer) : base(initializer)
    {
    }

    public override T? Get()
    {
        if (_computationResult != null) return _computationResult.Result;

        _mutex.WaitOne();
        _computationResult ??= ComputeValue();
        _mutex.ReleaseMutex();

        return _computationResult.Result;
    }
}
