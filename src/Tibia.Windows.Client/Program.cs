using System;

namespace Tibia.Windows.Client
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            // Present the debug window
            DebugWindow dbw = new DebugWindow();
            dbw.Show();

            // Then run the game
            using (Game game = new Game())
            {
                game.Run();
            }
        }
    }
#endif
}

