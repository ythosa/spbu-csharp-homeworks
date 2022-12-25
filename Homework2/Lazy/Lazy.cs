namespace Homework2.Lazy;

public abstract class Lazy<T> : ILazy<T>
{
    private Func<T>? _initializer;

    protected Lazy(Func<T> initializer)
    {
        _initializer = initializer;
    }

    public abstract T? Get();

    protected ComputationResult<T> ComputeValue()
    {
        ComputationResult<T> computationResult;

        try
        {
            computationResult = new ComputationResult<T>(_initializer!.Invoke());
        }
        catch (Exception receivedException)
        {
            computationResult = new ComputationResult<T>(receivedException);
        }

        _initializer = null;

        return computationResult;
    }
}
