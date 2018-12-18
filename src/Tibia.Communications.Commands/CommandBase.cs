using System;
using Tibia.Data;

namespace Tibia.Communications.Commands
{
    public abstract class CommandBase : ICommand
    {
        /// <summary>
        ///     Gets the keyword.
        /// </summary>
        /// <value>
        ///     The keyword.
        /// </value>
        public abstract string Keyword { get; }
        /// <summary>
        ///     Gets a value indicating whether the text should be posted in the channel.
        /// </summary>
        /// <param name="caster">The caster.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>
        ///     <see langword="true" /> if [post in channel]; otherwise, <see langword="false" />
        /// </returns>
        public abstract bool PostInChannel(ICharacterSpawn caster, params string[] args);
        /// <summary>
        ///     Determines whether this instance can be executed by the caster.
        /// </summary>
        /// <param name="caster">The caster.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>
        ///     <see langword="true" /> if this instance can be executed by the specified caster; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        public abstract bool CanExecute(ICharacterSpawn caster, params string[] args);
        /// <summary>
        ///     Executes the command from the specified caster.
        /// </summary>
        /// <param name="caster">The caster.</param>
        /// <param name="args">The arguments.</param>
        public void Execute(ICharacterSpawn caster, params string[] args)
        {
            ExecutingCommandEventArgs executingCommandEventArgs = new ExecutingCommandEventArgs(caster, args);
            Executing?.Invoke(this, executingCommandEventArgs);
            if (executingCommandEventArgs.Cancel)
                return;

            ExecuteCommand(executingCommandEventArgs.Caster, executingCommandEventArgs.Args);

            ExecutedCommandEventArgs executedCommandEventArgs = new ExecutedCommandEventArgs(executingCommandEventArgs.Caster, executingCommandEventArgs.Args);
            Executed?.Invoke(this, executedCommandEventArgs);
        }

        /// <summary>
        ///     Occurs when executing the command.
        /// </summary>
        public event EventHandler<ExecutingCommandEventArgs> Executing;

        /// <summary>
        ///     Occurs when the command executed.
        /// </summary>
        public event EventHandler<ExecutedCommandEventArgs> Executed;

        /// <summary>
        ///     Executes the command.
        /// </summary>
        /// <param name="caster">The caster.</param>
        /// <param name="args">The arguments.</param>
        protected abstract void ExecuteCommand(ICharacterSpawn caster, string[] args);
    }
}