using Warships.Game.Board.Description.Interfaces;

namespace Warships.Game.Board.Description
{
    public class DefaultBoardDescription : IBoardDescription
    {
        public int RowLimit => 10;

        public int ColumnLimit => 10;

        public string SupportedLetters => "ABCDEFGHIJ";
    }
}