using System.Collections;

namespace Homework1.Task2.SuffixArray;

public class SuffixArray : IEnumerable<int>
{
    private readonly string _sequence;

    public SuffixArray(string sequence)
    {
        _sequence = sequence;
    }

    public IEnumerator<int> GetEnumerator()
    {
        return GetSuffixArray().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private IEnumerable<int> GetSuffixArray()
    {
        return Enumerable.Range(0, _sequence.Length)
            .OrderBy(i => _sequence[i..], StringComparer.Ordinal);
    }
}
