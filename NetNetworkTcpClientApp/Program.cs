using System.Net;
using System.Net.Sockets;

using TcpClient client = new();
int port = 5000;

try
{
    await client.ConnectAsync(IPAddress.Loopback, port);

    if(client.Connected)
        Console.WriteLine("Client connected");
    else
        Console.WriteLine("Client not connected");
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}