using System.Net;
using System.Net.Sockets;
using System.Text;

using TcpClient client = new();
int port = 5000;

try
{
    await client.ConnectAsync(IPAddress.Loopback, port);

    if(client.Connected)
        Console.WriteLine("Client connected");
    else
        Console.WriteLine("Client not connected");

    NetworkStream stream = client.GetStream();

    // Receive from server
    byte[] buffer = new byte[4];
    await stream.ReadAsync(buffer);

    int size = BitConverter.ToInt32(buffer, 0);
    buffer = new byte[size];

    await stream.ReadAsync(buffer);
    Console.WriteLine($"Message from Server: { Encoding.UTF8.GetString(buffer) }");

    // Send to server
    string message = $"[{DateTime.Now.ToString()}]: Client message";
    buffer = Encoding.UTF8.GetBytes(message);
    byte[] bufferSize = new byte[4];
    bufferSize = BitConverter.GetBytes(buffer.Length);

    await stream.WriteAsync(bufferSize);
    await stream.WriteAsync(buffer);
    Console.WriteLine("Clent sended message to Server");
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}