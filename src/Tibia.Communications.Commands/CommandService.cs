using System.Collections.Generic;
using System.Linq;
using Tibia.Data;
using Tibia.Data.Services;

namespace Tibia.Communications.Commands
{
    public class CommandService : IService
    {
        private readonly Dictionary<string, ICommand> _commandsByKeyword;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandService" /> class.
        /// </summary>
        public CommandService()
        {
            _commandsByKeyword = new Dictionary<string, ICommand>();
        }

        /// <summary>
        ///     Registers the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void Register(ICommand command)
        {
            if (_commandsByKeyword.ContainsKey(command.Keyword))
                return;

            _commandsByKeyword.Add(command.Keyword, command);
        }

        /// <summary>
        ///     Executes the specified text and returns whether it can be posted in the channel.
        /// </summary>
        /// <param name="caster">The caster.</param>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <see langword="true" /> if [post in channel]; otherwise, <see langword="false" />
        /// </returns>
        public bool Execute(ICharacterSpawn caster, string text)
        {
            text = text.Trim().ToLower();
            List<string> parameters = text.Split(' ').ToList();
            string keyword = parameters.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(keyword) || !_commandsByKeyword.ContainsKey(keyword))
                return false;

            if (!_commandsByKeyword.TryGetValue(keyword, out ICommand command))
                return false;

            string[] args = parameters.Skip(1).ToArray();
            bool postInChannel = command.PostInChannel(caster, args);
            if (!command.CanExecute(caster, args))
                return postInChannel;

            command.Execute(caster, args);
            return postInChannel;
        }
    }
}