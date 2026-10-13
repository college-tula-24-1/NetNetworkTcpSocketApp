using System.Net.Sockets;
using System.Net;
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
        using TcpClient client = await listener.AcceptTcpClientAsync();
        NetworkStream stream = client.GetStream();

        List<byte> requestBuffer = new();
        int requestPart = 1;

        string response = "";

        while(true)
        {
            requestBuffer.Clear();

            while ((requestPart = stream.ReadByte()) != '\n')
                requestBuffer.Add((byte)requestPart);
            string word = Encoding.UTF8.GetString(requestBuffer.ToArray());

            Console.WriteLine($"Client sended word {word}");

            if (word == "*end*") break;

            if (!dictionary.ContainsKey(word))
                response = $"Not found word {word}";
            else 
                response = dictionary[word];
            
            Console.WriteLine($"Server answered {response}");

            response += "\n";
            await stream.WriteAsync(Encoding.UTF8.GetBytes(response));
        }

        client.Close();
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
