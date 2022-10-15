using Homework1.Task2.Bwt;

namespace Homework1.Task2.Tests.Bwt;

public class DecoderTests
{
    private static object[] _encodeCases =
    {
        new object[] { new EncodedSequence("", 0), "" },
        new object[] { new EncodedSequence("pssmipissii", 4), "mississippi" },
        new object[] { new EncodedSequence("nnbaaa", 3), "banana", },
        new object[] { new EncodedSequence("hhetsipraca", 6), "ihatecsharp", },
    };

    [Test]
    [TestCaseSource(nameof(_encodeCases))]
    public void DecodeTest(EncodedSequence sequence, string expected)
    {
        Assert.AreEqual(expected, new Decoder(sequence).Decode());
    }
}
