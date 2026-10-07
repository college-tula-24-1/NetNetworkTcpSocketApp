using System.Text;
using System.Net;
using System.Net.Sockets;

int port = 10000;

using TcpClient client = new();
await client.ConnectAsync(IPAddress.Loopback, port);
NetworkStream stream = client.GetStream();

List<byte> response = new();
string word = "";
int requestPart = 1;

while(true)
{
    Console.Write("Input word: ");
    word = Console.ReadLine();

    if (word == "*end*") break;

    await stream.WriteAsync(Encoding.UTF8.GetBytes(word + "\n"));

    while ((requestPart = stream.ReadByte()) != '\n')
        response.Add((byte)requestPart);

    Console.WriteLine($"{word}: { Encoding.UTF8.GetString(response.ToArray()) }");

    response.Clear();
}
