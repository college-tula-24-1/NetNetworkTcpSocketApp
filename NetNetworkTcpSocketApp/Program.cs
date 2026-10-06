using System.Net;
using System.Net.Sockets;

int port = 80;
string url = "yandex.ru";

using Socket socket = new Socket(
    AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp);

try
{
    await socket.ConnectAsync(url, port);
    Console.WriteLine("Conecting!");
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}

////

int endPointPort = 5000;

IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, endPointPort);
Socket socketListener = new Socket(
    AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp);

socketListener.Bind(endPoint);
