namespace Homework1.Task2.Tests.SuffixArray;

using Homework1.Task2.SuffixArray;

public class SuffixArrayTests
{
    [Test]
    [TestCase("", new int[] { }, TestName = "empty string")]
    [TestCase("abcde", new[] { 0, 1, 2, 3, 4 }, TestName = "sorted string")]
    [TestCase("ihatecsharp", new[] { 8, 2, 5, 4, 7, 1, 0, 10, 9, 6, 3 }, TestName = "random string")]
    public void Test(string sequence, int[] expected)
    {
        Assert.That(new SuffixArray(sequence).ToArray(), Is.EqualTo(expected));
    }
}
