using System;

namespace Tibia.InteropServices
{
    public struct OSPlatform
    {
        private readonly ushort _value;

        /// <summary>
        ///     Initializes a new instance of the <see cref="OSPlatform" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        private OSPlatform(ushort value)
        {
            _value = value;
        }

        /// <summary>
        ///     Gets an object that represents the Linux operating system.
        /// </summary>
        /// <value>
        ///     The object that represents the Linux operating system.
        /// </value>
        public static OSPlatform Linux { get; } = new OSPlatform(1);

        /// <summary>
        ///     Gets an object that represents the Windows operating system.
        /// </summary>
        /// <value>
        ///     The object that represents the Windows operating system.
        /// </value>
        public static OSPlatform Windows { get; } = new OSPlatform(2);

        /// <summary>
        ///     Gets an object that represents the OSX operating system.
        /// </summary>
        /// <value>
        ///     The object that represents the OSX operating system.
        /// </value>
        public static OSPlatform OSX { get; } = new OSPlatform(3);

        /// <summary>
        /// Gets an object that represents the OT client.
        /// </summary>
        /// <value>
        /// The object that represents the OT client.
        /// </value>
        public static OSPlatform OTClient { get; } = new OSPlatform(10);

        /// <summary>
        ///     Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The operating system platform.</returns>
        public static OSPlatform Parse(ushort value)
        {
            if (Linux._value == value)
                return Linux;

            if (Windows._value == value)
                return Windows;

            if (OSX._value == value)
                return OSX;

            if (OTClient._value == value)
                return OTClient;

            throw new ArgumentException(nameof(value));
        }
    }
}