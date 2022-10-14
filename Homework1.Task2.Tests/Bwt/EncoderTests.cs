using Homework1.Task2.Bwt;

namespace homework1.task2.tests.Bwt;

public class EncoderTests
{
    private static object[] _encodeCases =
    {
        new object[] { "", new EncodedSequence("", 0) },
        new object[] { "mississippi", new EncodedSequence("pssmipissii", 4) },
        new object[] { "banana", new EncodedSequence("nnbaaa", 3) },
        new object[] { "ihatecsharp", new EncodedSequence("hhetsipraca", 6) },
    };

    [Test]
    [TestCaseSource(nameof(_encodeCases))]
    public void EncodeTest(string sequence, EncodedSequence expected)
    {
        Assert.AreEqual(expected, new Encoder(sequence).Encode());
    }
}
