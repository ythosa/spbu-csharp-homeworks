using Homework2.Lazy;

namespace Homework2;

internal static class Example
{
    private static readonly ILazyFactory<int> SuccessLazyFactory = new LazyFactory<int>(() => 2);

    private static readonly ILazyFactory<int> ExceptionLazyFactory = new LazyFactory<int>(
        () => throw new ApplicationException("ooops"));

    static void Main()
    {
        ComputingSuccess();
        ComputingException();
        ComputingThreadSafe();
    }

    private static void ComputingSuccess()
    {
        var computingSuccess = SuccessLazyFactory.CreateUnsafeLazy();
        Console.WriteLine($"{computingSuccess.Get()}");
        Console.WriteLine($"{computingSuccess.Get()}");
    }

    private static void ComputingException()
    {
        var computingException = ExceptionLazyFactory.CreateUnsafeLazy();
        try
        {
            Console.WriteLine(computingException.Get());
        }
        catch (ApplicationException e)
        {
            Console.WriteLine($"exception: {e.Message}");
        }

        try
        {
            Console.WriteLine(computingException.Get());
        }
        catch (ApplicationException e)
        {
            Console.WriteLine($"received exception: {e.Message}");
        }
    }

    private static void ComputingThreadSafe()
    {
        var computingSuccess = SuccessLazyFactory.CreateThreadSafeLazy();

        var t1 = new Task(() => Console.WriteLine(computingSuccess.Get()));
        var t2 = new Task(() => Console.WriteLine(computingSuccess.Get()));

        t1.Start();
        t2.Start();

        t1.Wait();
        t2.Wait();
    }
}
