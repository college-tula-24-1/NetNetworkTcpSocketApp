using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

int port = 5000;

TcpListener listener = new(System.Net.IPAddress.Any, port);

try
{
    listener.Start();
    Console.WriteLine("Server started...");

    while(true)
    {
        using TcpClient client = await listener.AcceptTcpClientAsync();
        Console.WriteLine($"Accept client: {client.Client.RemoteEndPoint}");

        NetworkStream stream = client.GetStream();

        // Send to Client
        string message = $"[{DateTime.Now.ToString()}] Server message";
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        byte[] bufferSize = BitConverter.GetBytes(buffer.Length);

        await stream.WriteAsync(bufferSize);
        await stream.WriteAsync(buffer);
        Console.WriteLine("Server sended message to Client");

        // Receive from Client
        await stream.ReadAsync(bufferSize);
        int size = BitConverter.ToInt32(bufferSize);
        buffer = new byte[size];
        await stream.ReadAsync(buffer);

        Console.WriteLine($"Client message: { Encoding.UTF8.GetString(buffer) }");
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    listener.Stop();
}
