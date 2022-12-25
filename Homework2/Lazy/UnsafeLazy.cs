namespace Homework2.Lazy;

public class UnsafeLazy<T> : Lazy<T>, ILazy<T>
{
    private ComputationResult<T>? _computationResult;

    public UnsafeLazy(Func<T> initializer) : base(initializer)
    {
    }

    public override T? Get()
    {
        _computationResult ??= ComputeValue();

        return _computationResult.Result;
    }
}
