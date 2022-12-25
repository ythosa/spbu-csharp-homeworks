namespace Homework1.Task2;

internal static class Example
{
    static void Main()
    {
        const string sequence = "ABACABA";
        Console.WriteLine($"Sequence: {sequence}");

        var encodedSequence = new Bwt.Encoder(sequence).Encode();
        Console.WriteLine($"Encoded: {encodedSequence.TransformedSequence}"); // ipssmpissii
        Console.WriteLine($"EOF index: {encodedSequence.EofIndex}"); // 5
        Console.WriteLine($"With EOF: {encodedSequence.WithEof()}"); // amnnn$lcpmnapaaaaaaala

        var decodedSequence = new Bwt.Decoder(encodedSequence).Decode();
        Console.WriteLine($"Decoded: {decodedSequence}"); // amanaplanacanalpanama

        Console.WriteLine(decodedSequence == sequence);
    }
}
