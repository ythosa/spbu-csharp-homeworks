using System.Net;
using System.Net.Sockets;
using System.Text;
using Homework4.Ftp.Extensions;

namespace Homework4.Ftp;

public class Server
{
    private const int GetAnswerBufferSize = 8;
    private const int ListAnswerBufferSize = 4;
    private const int IncorrectRequestLengthAnswer = -1;

    private readonly int _port;

    public Server(int port)
    {
        _port = port;
    }

    public async Task Run(CancellationToken cancellationToken)
    {
        var listener = new TcpListener(IPAddress.Any, _port);
        listener.Start();

        var taskList = new LinkedList<Task>();

        try
        {
            while (true)
            {
                var socket = await listener.AcceptSocketAsync(cancellationToken).ConfigureAwait(false);

                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                taskList.AddLast(Task.Run(async () =>
                    await ReadAndExecuteRequests(socket).ConfigureAwait(false), cancellationToken));
            }
        }
        finally
        {
            foreach (var task in taskList)
            {
                await task.ConfigureAwait(false);
            }

            listener.Stop();
        }
    }

    private static async Task ListAsync(string path, NetworkStream stream)
    {
        var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory() + path);

        if (!currentDirectory.Exists)
        {
            await stream.ConfigureWriteAsyncFromZero(
                BitConverter.GetBytes(IncorrectRequestLengthAnswer), ListAnswerBufferSize);

            await stream.ConfigureFlushAsync();

            return;
        }

        var size = currentDirectory.GetFiles().Length + currentDirectory.GetDirectories().Length;

        await stream.ConfigureWriteAsyncFromZero(BitConverter.GetBytes(size), ListAnswerBufferSize);

        foreach (var file in currentDirectory.EnumerateFiles())
        {
            await stream.ConfigureWriteAsync(Encoding.Unicode.GetBytes(file.Name));
            await stream.ConfigureWriteWhiteSpaceAsync();

            await stream.ConfigureWriteFalseAsync();
            await stream.ConfigureWriteWhiteSpaceAsync();
        }

        foreach (var directory in currentDirectory.EnumerateDirectories())
        {
            await stream.ConfigureWriteAsync(Encoding.Unicode.GetBytes(directory.Name));
            await stream.ConfigureWriteWhiteSpaceAsync();

            await stream.ConfigureWriteTrueAsync();
            await stream.ConfigureWriteWhiteSpaceAsync();
        }

        await stream.ConfigureFlushAsync();
    }

    private static async Task GetAsync(string inputPath, NetworkStream stream)
    {
        var path = Directory.GetCurrentDirectory() + inputPath;

        if (!File.Exists(path))
        {
            await stream.ConfigureWriteAsyncFromZero(
                BitConverter.GetBytes(IncorrectRequestLengthAnswer), GetAnswerBufferSize);

            await stream.ConfigureFlushAsync();

            return;
        }

        var file = new FileInfo(path);

        await stream.ConfigureWriteAsyncFromZero(
            BitConverter.GetBytes(file.Length), GetAnswerBufferSize);

        var fileStream = file.Open(FileMode.Open);
        await fileStream.CopyToAsync(stream).ConfigureAwait(false);
        fileStream.Close();

        await stream.ConfigureFlushAsync();
    }

    private static async Task ReadAndExecuteRequests(Socket socket)
    {
        using (socket)
        {
            var stream = new NetworkStream(socket);
            var reader = new StreamReader(stream);
            var command = await reader.ConfigureReadLineAsync();
            var dividedCommand = command?.Split() ?? Array.Empty<string>();

            var (commandType, path) = (dividedCommand[0], dividedCommand[1]);

            var transmissionTask = commandType switch
            {
                "1" => Task.Run(
                    async () => await ListAsync(path, stream).ConfigureAwait(false)),
                "2" => Task.Run(
                    async () => await GetAsync(path, stream).ConfigureAwait(false)),
                _ => Task.Run(async () =>
                    await stream.ConfigureWriteAsyncFromZero(BitConverter.GetBytes(-1), GetAnswerBufferSize))
            };

            await transmissionTask;
        }
    }
}
