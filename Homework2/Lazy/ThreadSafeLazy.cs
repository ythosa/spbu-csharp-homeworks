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
        var result1 = _computationResult;
        if (result1 != null) return result1.Result;

        _mutex.WaitOne();
        var result2 = _computationResult;
        if (result2 != null)
        {
            _mutex.ReleaseMutex();
            return result2.Result;
        }

        _computationResult ??= ComputeValue();
        _mutex.ReleaseMutex();

        return _computationResult.Result;
    }
}
