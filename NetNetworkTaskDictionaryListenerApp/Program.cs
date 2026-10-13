// Task Listener

using System.Net;
using System.Net.Sockets;
using System.Text;

int port = 10000;
TcpListener listener = new(IPAddress.Any, port);
Dictionary<string, string> dictionary = new()
{
    { "table", "стол" },
    { "red", "красный" },
    { "people", "люди" },
    { "computer", "компьютер" },
};

try
{
    listener.Start();
    Console.WriteLine("Listener waiting...");

    while(true)
    {
        var client = await listener.AcceptTcpClientAsync();
        Console.WriteLine($"Conected client {client.Client.RemoteEndPoint}");

        Task.Run(async () => await ClientPrcessingAsync(client));
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


async Task ClientPrcessingAsync(TcpClient client)
{
    NetworkStream stream = client.GetStream();

    using StreamReader reader = new StreamReader(stream);
    using StreamWriter writer = new StreamWriter(stream);

    //List<byte> requestBuffer = new();
    //int requestPart = 1;

    string response = "";

    while (true)
    {
        //requestBuffer.Clear();

        //while ((requestPart = stream.ReadByte()) != '\n')
        //    requestBuffer.Add((byte)requestPart);
        //string word = Encoding.UTF8.GetString(requestBuffer.ToArray());

        string word = await reader.ReadLineAsync();

        Console.WriteLine($"Client {client.Client.RemoteEndPoint} sended word {word}");

        if (word == "*end*") break;

        if (!dictionary.ContainsKey(word))
            response = $"Not found word {word}";
        else
            response = dictionary[word];

        Console.WriteLine($"Server answered {client.Client.RemoteEndPoint}: {response}");

        response += "\n";
        //await stream.WriteAsync(Encoding.UTF8.GetBytes(response));
        await writer.WriteLineAsync(response);
        await writer.FlushAsync();
    }

    client.Close();
}