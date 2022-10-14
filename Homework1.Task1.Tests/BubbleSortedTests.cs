namespace Homework1.Task1.Tests;

using Task1;

[TestFixture]
public class BubbleSortedTests
{
    [Test]
    [TestCase(new int[0], TestName = "empty")]
    [TestCase(new[] { 1, 2, 3, 4, 5 }, TestName = "already sorted")]
    [TestCase(new[] { 5, 4, 3, 2, 1 }, TestName = "reversed")]
    [TestCase(new[] { 1, 2, 4, 3, 5 }, TestName = "two elements are out of place")]
    [TestCase(new[] { 2, 5, 5, 5, 1 }, TestName = "with repeated elements")]
    [TestCase(new[] { 2, 3, 5, 4, 1 }, TestName = "disordered")]
    public void Test(int[] elements)
    {
        Assert.AreEqual(
            elements.OrderBy(element => element).ToArray(),
            new BubbleSorted<int>(elements).ToArray()
        );
    }
}
