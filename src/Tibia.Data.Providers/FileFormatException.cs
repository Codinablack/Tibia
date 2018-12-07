using System;

namespace Tibia.Data.Providers
{
    public class FileFormatException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Data.Providers.FileFormatException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public FileFormatException(string message)
            : base(message)
        {
        }
    }
}