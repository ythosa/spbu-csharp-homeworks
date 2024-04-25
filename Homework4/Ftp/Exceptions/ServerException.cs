namespace Homework4.Ftp.Exceptions;

[Serializable]
public class ServerException : Exception
{
    public ServerException()
    {
    }

    public ServerException(string message)
        : base(message)
    {
    }

    public ServerException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
