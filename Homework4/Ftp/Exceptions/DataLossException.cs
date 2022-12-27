namespace Homework4.Ftp.Exceptions;

[Serializable]
public class DataLossException : Exception
{
    public DataLossException()
    {
    }

    public DataLossException(string message)
        : base(message)
    {
    }

    public DataLossException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
