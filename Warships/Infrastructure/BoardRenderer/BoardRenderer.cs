using System.Text;
using Warships.Game.Board.Description.Interfaces;
using Warships.Game.Exceptions;
using Warships.Game.Interfaces.BoardRenderer;
using Warships.Game.Objects;
using Warships.Infrastructure.Wrappers;

namespace Warships.Infrastructure.BoardRenderer
{
    public class BoardRenderer : IBoardRenderer
    {
        private readonly IConsole _console;
        private readonly IBoardDescription _boardDescription;

        public BoardRenderer(IConsole console, IBoardDescription boardDescription)
        {
            _console = console;
            _boardDescription = boardDescription;
        }

        public void RenderBoard(IEnumerable<Warship> warships, Player player)
        {
            if (!warships.Any() || warships.Any(w => w.Parts.Count == 0))
            {
                throw new GameErrorException(Errors.GameIsNotInitializedProperly);
            }

            _console.Clear();
            var strings = Enumerable.Range(0, _boardDescription.RowLimit).Select(i => new StringBuilder($"{i + 1:D2}" + new string(' ', _boardDescription.ColumnLimit))).ToList();
            strings.Insert(0, new StringBuilder($"  {_boardDescription.SupportedLetters}"));
            IEnumerable<ShipPart> parts = warships.SelectMany(w => w.Parts);
            foreach (Field shoot in player.Choises)
            {
                var shipPart = parts.FirstOrDefault(p => p.Field == shoot);
                if (shipPart != null)
                {
                    if (shipPart.Warship.Parts.All(p => p.Shooted))
                    {
                        strings[shoot.Row + 1][shoot.Column + 2] = 'S';
                    }
                    else
                    {
                        strings[shoot.Row + 1][shoot.Column + 2] = 'H';
                    }
                }
                else
                {
                    strings[shoot.Row + 1][shoot.Column + 2] = 'M';
                }
            }

            foreach (var line in strings)
            {
                if (line[0] == '0')
                {
                    line[0] = ' ';
                }
                _console.WriteLine(line.ToString());
            }
        }
    }
}