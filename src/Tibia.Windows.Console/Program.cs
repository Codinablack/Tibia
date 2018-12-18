using System.Threading.Tasks;

namespace Tibia.Windows.Console
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            GameService gameService = new GameService();
            gameService.Logger = new ConsoleLogger();
            await gameService.Start();

            System.Console.ReadLine();
        }
    }
}