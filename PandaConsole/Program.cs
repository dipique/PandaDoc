using System;
using System.Threading.Tasks;

using PandaDoc;

namespace PandaConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                var client = new PandaDocHttpClient();
                client.SetApiKey("");
                var response = await client.GetDocument("");
                Console.ReadKey();
            }).Wait();   
        }
    }
}
