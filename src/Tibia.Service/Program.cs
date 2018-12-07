using System;
using System.Threading.Tasks;

namespace Tibia.Service
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            GameService gameService = new GameService();
            gameService.Logger = new ConsoleLogger();
            await gameService.Start();

            Console.ReadLine();
        }
    }
}