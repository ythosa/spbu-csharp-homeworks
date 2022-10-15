using Homework2.Lazy;

namespace Homework2.Tests.Lazy;

public class UnsafeLazy
{
    private static object[] _getSuccessCases =
    {
        new object?[] { new ThreadSafeLazy<int?>(() => null), null },
        new object[] { new ThreadSafeLazy<int>(() => 2 * 2), 4 },
        new object[] { new ThreadSafeLazy<string>(() => "i" + "hate" + "csharp"), "ihatecsharp" },
        new object[] { new ThreadSafeLazy<int[]>(() => new[] { 1, 2 }), new[] { 1, 2 } },
    };

    [Test]
    [TestCaseSource(nameof(_getSuccessCases))]
    public void GetSuccessTest<T>(ILazy<T> lazy, T expected)
    {
        Assert.That(lazy.Get(), Is.EqualTo(expected));
        Assert.That(lazy.Get(), Is.EqualTo(expected));
        Assert.That(lazy.Get(), Is.EqualTo(expected));
    }

    private static object[] _getExceptionCases =
    {
        new object?[]
        {
            new ThreadSafeLazy<int?>(() => throw new Exception("ooooooooooops")),
            new Exception("ooooooooooops")
        },
        new object?[]
        {
            new ThreadSafeLazy<int?>(() => throw new InvalidCastException("i hate csharp")),
            new InvalidCastException("i hate csharp")
        },
    };

    [Test]
    [TestCaseSource(nameof(_getExceptionCases))]
    public void GetExceptionTest<T>(ILazy<T> lazy, Exception expected)
    {
        Assert.Throws(expected.GetType(), () => lazy.Get());
        Assert.Throws(expected.GetType(), () => lazy.Get());
        Assert.Throws(expected.GetType(), () => lazy.Get());
    }
}
