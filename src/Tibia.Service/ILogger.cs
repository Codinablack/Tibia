namespace Tibia.Service
{
    public interface ILogger
    {
        /// <summary>
        ///     Logs the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="args">The arguments.</param>
        void Log(string text, params object[] args);

        /// <summary>
        ///     Logs the done.
        /// </summary>
        void LogDone();

        /// <summary>
        ///     Logs the error.
        /// </summary>
        /// <param name="errorText">The error text.</param>
        void LogError(string errorText);

        /// <summary>
        ///     Logs the start.
        /// </summary>
        /// <param name="text">The text.</param>
        void LogStart(string text);
    }
}