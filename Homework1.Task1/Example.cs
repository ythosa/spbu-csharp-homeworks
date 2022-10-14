namespace Homework1.Task1;

internal static class Example
{
    static void Main()
    {
        var randomGenerator = new Random();
        IEnumerable<int> elements = Enumerable.Repeat(0, 10).Select(_ => randomGenerator.Next(0, 1000)).ToArray();
        IEnumerable<int> sortedElements = new BubbleSorted<int>(elements); // have initialized but not computed yet

        Console.WriteLine(string.Join(", ", elements));
        Console.WriteLine(string.Join(", ", sortedElements)); // computing here only once
        Console.WriteLine(string.Join(", ", sortedElements)); // using already computed
    }
}
