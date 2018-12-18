using System;
using Tibia.Data;

namespace Tibia.Communications.Commands
{
    public class ExecutedCommandEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Communications.Commands.ExecutedCommandEventArgs" />
        ///     class.
        /// </summary>
        /// <param name="caster">The caster.</param>
        /// <param name="args">The arguments.</param>
        public ExecutedCommandEventArgs(ICharacterSpawn caster, string[] args)
        {
            Caster = caster;
            Args = args;
        }

        /// <summary>
        ///     Gets the caster.
        /// </summary>
        /// <value>
        ///     The caster.
        /// </value>
        public ICharacterSpawn Caster { get; }

        /// <summary>
        ///     Gets the arguments.
        /// </summary>
        /// <value>
        ///     The arguments.
        /// </value>
        public string[] Args { get; }
    }
}