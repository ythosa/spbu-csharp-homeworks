using System.Text;

namespace Homework1.Task2.Bwt;

public class Encoder
{
    private readonly string _sequence;

    public Encoder(string sequence)
    {
        _sequence = sequence;
    }

    public EncodedSequence Encode()
    {
        var transformedSequenceBuilder = new StringBuilder(_sequence.Length);
        var eofIndex = 0;

        var suffixArray = new SuffixArray.SuffixArray(_sequence).ToArray();
        for (var i = 0; i < _sequence.Length; i++)
        {
            var index = suffixArray[i] - 1;
            if (index >= 0)
            {
                transformedSequenceBuilder.Append(_sequence[index]);
            }
            else
            {
                eofIndex = i;
                transformedSequenceBuilder.Append(_sequence[^1]);
            }
        }

        return new EncodedSequence(transformedSequenceBuilder.ToString(), eofIndex);
    }
}
