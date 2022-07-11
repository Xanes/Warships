using System.Text.RegularExpressions;
using Warships.Game.Board.Description.Interfaces;
using Warships.Game.Constants;
using Warships.Game.Exceptions;
using Warships.Game.Interfaces.InputOutput;
using Warships.Game.Objects;

namespace Warships.Game.Engines.Players
{
    public class PlayerEngine : IPlayerEngine
    {
        private readonly IGameInputOutput _gameInputOutput;
        private readonly IBoardDescription _boardDescription;
        private readonly Regex _inputValidator;

        public PlayerEngine(IGameInputOutput gameInputOutput, IBoardDescription boardDescription)
        {
            _gameInputOutput = gameInputOutput;
            _boardDescription = boardDescription;
            _inputValidator = new Regex(@"^([" + _boardDescription.SupportedLetters.First() + "-" + _boardDescription.SupportedLetters.Last() + "][1-9]{1," + _boardDescription.RowLimit.ToString().Length + "})");
        }

        public Player? Player { get; private set; }

        public void CreatePlayer()
        {
            Player = new Player();
        }

        public Field AskForCoordinates()
        {
            if (Player == null)
            {
                throw new GameErrorException(Errors.PlayerIsNotCreated);
            }

            string? input = _gameInputOutput.AskForInput(
                String.Format(
                    GameConstants.QuestionForCoordinates,
                    _boardDescription.SupportedLetters.First(),
                    _boardDescription.SupportedLetters.Last(),
                    _boardDescription.RowLimit,
                    _boardDescription.SupportedLetters.First()));

            if (string.IsNullOrEmpty(input) || !_inputValidator.IsMatch(input) || !int.TryParse(input.Substring(1), out int rowNumber) || rowNumber > _boardDescription.RowLimit)
            {
                return AskForCoordinates();
            }

            Field field = new Field(_boardDescription.SupportedLetters.IndexOf(input[0]), int.Parse(input.Substring(1)) - 1);

            if (Player.Choises.Any(f => f == field))
            {
                return AskForCoordinates();
            }
            Player.AddChoice(field);
            return field;
        }
    }
}