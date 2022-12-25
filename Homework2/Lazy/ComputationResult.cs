namespace Homework2.Lazy;

public class ComputationResult<T>
{
    private readonly T? _result;
    private readonly Exception? _cachedException;

    public T? Result => _cachedException == null ? _result : throw _cachedException;

    public ComputationResult(T result)
    {
        _result = result;
        _cachedException = null;
    }

    public ComputationResult(Exception exception)
    {
        _cachedException = exception;
        _result = default;
    }
}
