using System.Text;

namespace Homework1.Task2.Bwt;

public class Decoder
{
    private readonly string _transformedSequence;

    private readonly int _eofIndex;

    private int SequenceSize => _transformedSequence.Length;

    public Decoder(EncodedSequence encodedSequence)
    {
        (_transformedSequence, _eofIndex) = encodedSequence;
    }

    public string Decode()
    {
        if (_transformedSequence == "")
            return "";

        var reversedSequence = new StringBuilder(SequenceSize);

        var transitionArray = GetTransitionArray();
        var index = transitionArray[_eofIndex];
        for (var i = 0; i < SequenceSize; i++)
        {
            reversedSequence.Append(_transformedSequence[index]);
            index = transitionArray[index];
        }

        return reversedSequence.ToString();
    }

    private int[] GetTransitionArray()
    {
        var transitionArray = new int[SequenceSize];

        var occurrencesByRune = GetOccurrencesByRune();
        for (var i = 0; i < SequenceSize; i++)
            transitionArray[occurrencesByRune[_transformedSequence[i]]++] = i;

        return transitionArray;
    }

    private int[] GetOccurrencesByRune()
    {
        var alphabetSize = _transformedSequence.Max() + 1;

        var occurrencesByRune = new int[alphabetSize + 1];
        foreach (var rune in _transformedSequence)
            occurrencesByRune[rune + 1]++;

        for (var i = 0; i < alphabetSize; i++)
            occurrencesByRune[i + 1] += occurrencesByRune[i];

        return occurrencesByRune;
    }
}
