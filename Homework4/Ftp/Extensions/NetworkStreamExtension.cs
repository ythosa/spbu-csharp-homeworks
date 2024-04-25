using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace Homework4.Ftp.Extensions;

using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using Exceptions;

/// <summary>
/// Some <see cref="NetworkStream"/> and <see cref="StreamReader"/> extensions.
/// </summary>
public static class NetworkStreamExtensions
{
    private const int CharCodeLength = 2;

    private static readonly byte[] FalseInBytes = { Convert.ToByte(false) };
    private static readonly byte[] TrueInBytes = { Convert.ToByte(true) };

    private static readonly byte[] WhiteSpaceBytes = Encoding.Unicode.GetBytes(" ");

    public static ConfiguredTaskAwaitable ConfigureWriteAsyncFromZero(this NetworkStream stream, byte[] buffer, int count)
        => stream.WriteAsync(buffer, 0, count).ConfigureAwait(false);

    public static ConfiguredTaskAwaitable ConfigureWriteAsync(this NetworkStream stream, byte[] buffer)
        => stream.WriteAsync(buffer, 0, buffer.Length).ConfigureAwait(false);

    public static ConfiguredTaskAwaitable ConfigureWriteWhiteSpaceAsync(this NetworkStream stream)
        => stream.WriteAsync(WhiteSpaceBytes, 0, CharCodeLength).ConfigureAwait(false);

    public static async Task ConfigureReadAsyncWithCheck(this NetworkStream stream, byte[] buffer)
    {
        if (await stream.ReadAsync(buffer).ConfigureAwait(false) != buffer.Length)
        {
            throw new DataLossException("data loss during transmission, incomplete specifier");
        }
    }

    public static ConfiguredTaskAwaitable ConfigureWriteTrueAsync(this NetworkStream stream)
        => stream.WriteAsync(TrueInBytes, 0, FalseInBytes.Length).ConfigureAwait(false);

    public static ConfiguredTaskAwaitable ConfigureWriteFalseAsync(this NetworkStream stream)
        => stream.WriteAsync(FalseInBytes, 0, FalseInBytes.Length).ConfigureAwait(false);

    public static ConfiguredTaskAwaitable ConfigureFlushAsync(this NetworkStream stream)
        => stream.FlushAsync().ConfigureAwait(false);

    public static ConfiguredTaskAwaitable<string?> ConfigureReadLineAsync(this StreamReader stream)
        => stream.ReadLineAsync().ConfigureAwait(false);
}
