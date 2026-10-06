using System.Net;
using System.Net.Sockets;
using System.Text;

int port = 5000;

IPEndPoint endPoint = new(IPAddress.Any, port);
Socket socketListener = new(
    AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp);

try
{
    socketListener.Bind(endPoint);
    socketListener.Listen();
    Console.WriteLine("Listener listen...");

    while(true)
    {
        using Socket socketClient = await socketListener.AcceptAsync();

        // Send message to client
        string message = DateTime.Now.ToString() + " Message to client";
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        await socketClient.SendAsync(buffer);
        Console.WriteLine($"Client address: {socketClient.RemoteEndPoint} sending message");

        // Receive message from client
        buffer = new byte[1024];
        await socketClient.ReceiveAsync(buffer);
        message = Encoding.UTF8.GetString(buffer);
        Console.WriteLine($"Message from client: {message}");
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}





