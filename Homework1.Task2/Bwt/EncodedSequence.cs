namespace Homework1.Task2.Bwt;

public readonly struct EncodedSequence
{
    public readonly string TransformedSequence;
    public readonly int EofIndex;

    public EncodedSequence(string transformedSequence, int eofIndex)
    {
        TransformedSequence = transformedSequence;
        EofIndex = eofIndex;
    }

    public string WithEof(char eofRune = '$')
    {
        return TransformedSequence[..EofIndex] + eofRune + TransformedSequence[EofIndex..];
    }

    public override string ToString()
    {
        return $"{TransformedSequence} {EofIndex}";
    }

    public void Deconstruct(out string transformedSequence, out int eofIndex)
    {
        transformedSequence = TransformedSequence;
        eofIndex = EofIndex;
    }
}
