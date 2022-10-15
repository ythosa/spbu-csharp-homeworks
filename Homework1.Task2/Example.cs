namespace Homework1.Task2;

internal static class Example
{
    static void Main()
    {
        const string sequence = "mississippi";
        Console.WriteLine($"Sequence: {sequence}");

        var encodedSequence = new Bwt.Encoder(sequence).Encode();
        Console.WriteLine($"Encoded: {encodedSequence.TransformedSequence}"); // pssmipissii
        Console.WriteLine($"EOF index: {encodedSequence.EofIndex}"); // 4

        var decodedSequence = new Bwt.Decoder(encodedSequence).Decode();
        Console.WriteLine($"Decoded: {decodedSequence}"); // mississippi
    }
}
