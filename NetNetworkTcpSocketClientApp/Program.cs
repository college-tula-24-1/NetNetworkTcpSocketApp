using System.Net;
using System.Net.Sockets;
using System.Text;

int portListener = 5000;

using Socket socketClient = new Socket(
    AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp);

try
{
    Console.WriteLine("Client conecting...");
    
    await socketClient.ConnectAsync(IPAddress.Loopback, portListener);
    
    // Receive message from server
    byte[] buffer = new byte[1024];
    await socketClient.ReceiveAsync(buffer);
    string message = Encoding.UTF8.GetString(buffer);
    Console.WriteLine($"We response meassage from server: {message}");


    // Send message to server
    message = "Hello from Client!";
    buffer = Encoding.UTF8.GetBytes(message);
    await socketClient.SendAsync(buffer);
    Console.WriteLine("We sending message to server");
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}