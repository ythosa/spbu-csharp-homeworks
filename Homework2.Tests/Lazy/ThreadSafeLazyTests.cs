using Homework2.Lazy;

namespace Homework2.Tests.Lazy;

public class ThreadSafeLazy
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
    [Repeat(20)]
    public void GetSuccessTest<T>(ILazy<T> lazy, T expected)
    {
        Task<T?>[] tasks = Enumerable.Range(0, 20).Select(_ => Task<T>.Factory.StartNew(() => lazy.Get())).ToArray();

        foreach (var task in tasks)
        {
            Assert.AreEqual(expected, task.Result);
            Assert.AreEqual(expected, task.Result);
            Assert.AreEqual(expected, task.Result);
        }
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
    [Repeat(20)]
    public void GetExceptionTest<T>(ILazy<T> lazy, Exception expected)
    {
        var tasks = Enumerable.Range(0, 20)
            .Select(_ => Task<Exception?>.Factory.StartNew(
                () => Assert.Throws(expected.GetType(), () => lazy.Get())
            )).ToArray();

        foreach (var task in tasks)
        {
            task.Wait();
        }
    }
}
