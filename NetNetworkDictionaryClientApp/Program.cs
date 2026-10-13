using System.Text;
using System.Net;
using System.Net.Sockets;

int port = 10000;

using TcpClient client = new();
await client.ConnectAsync(IPAddress.Loopback, port);
NetworkStream stream = client.GetStream();

using StreamReader reader = new StreamReader(stream);
using StreamWriter writer = new StreamWriter(stream);

//List<byte> response = new();
//int requestPart = 1;

string word = "";
while (true)
{
    Console.Write("Input word: ");
    word = Console.ReadLine();

    //await stream.WriteAsync(Encoding.UTF8.GetBytes(word + "\n"));
    await writer.WriteLineAsync(word);
    await writer.FlushAsync();

    //while ((requestPart = stream.ReadByte()) != '\n')
    //    response.Add((byte)requestPart);

    string response = await reader.ReadLineAsync();

    Console.WriteLine($"{word}: {response}");

    

}
