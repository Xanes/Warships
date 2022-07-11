using Warships.Game.Constants;
using Warships.Game.Exceptions;
using Warships.Game.Interfaces.InputOutput;
using Warships.Infrastructure.Wrappers;

namespace Warships.Infrastructure.InputOutput
{
    public class ConsoleGameInputOutput : IGameInputOutput
    {
        private readonly IConsole _console;

        public ConsoleGameInputOutput(IConsole console)
        {
            _console = console;
        }

        public string? AskForInput(string? question = null)
        {
            if (question != null)
            {
                _console.WriteLine(question);
            }

            string? input = _console.ReadLine();

            if(string.Equals(input, GameConstants.ExitRequest, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new ExitException();
            }

            return input;
        }

        public void RenderMessage(string message)
        {
            _console.WriteLine(message);
        }
    }
}