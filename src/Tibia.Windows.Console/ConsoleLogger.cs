using System;
using System.Diagnostics;
using System.Globalization;

namespace Tibia.Windows.Console
{
    public class ConsoleLogger : ILogger
    {
        private int _startTextLength;
        private readonly Stopwatch _taskTimer = new Stopwatch();

        /// <inheritdoc />
        /// <summary>
        ///     Logs the start.
        /// </summary>
        /// <param name="text">The text.</param>
        public void LogStart(string text)
        {
            _taskTimer.Restart();
            string formattedText = $"[{DateTime.UtcNow.ToString("dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture)}] {text}...";
            System.Console.Write(formattedText);
            _startTextLength = formattedText.Length;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Logs the done.
        /// </summary>
        public void LogDone()
        {
            const string done = "Done";
            string doneTime = _taskTimer.Elapsed.ToString(@"hh\:mm\:ss", null);

            System.Console.Write(".".Repeat(System.Console.WindowWidth - _startTextLength - done.Length - 12));
            System.Console.ForegroundColor = ConsoleColor.DarkGreen;
            System.Console.Write(done);
            System.Console.ResetColor();
            System.Console.Write(" ".Repeat(11 - doneTime.Length));
            System.Console.Write(doneTime);
            System.Console.WriteLine();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Logs the error.
        /// </summary>
        /// <param name="errorText">The error text.</param>
        public void LogError(string errorText)
        {
            const string error = "Error";
            System.Console.Write(".".Repeat(System.Console.WindowWidth - _startTextLength - error.Length - 12));
            System.Console.ForegroundColor = ConsoleColor.DarkRed;
            System.Console.WriteLine(error);
            System.Console.ResetColor();
            System.Console.WriteLine(errorText);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Logs the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="args">The arguments.</param>
        public void Log(string text, params object[] args)
        {
            System.Console.WriteLine("[{0}] {1}", DateTime.UtcNow.ToString("dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture), string.Format(text, args));
        }
    }
}