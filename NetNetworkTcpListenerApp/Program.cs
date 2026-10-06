using System.Net.Sockets;

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
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
