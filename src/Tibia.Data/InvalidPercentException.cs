using System;

namespace Tibia.Data
{
    public class InvalidPercentException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Data.InvalidPercentException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidPercentException(string message)
            : base(message)
        {
        }
    }
}