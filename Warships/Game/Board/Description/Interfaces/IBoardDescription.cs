namespace Warships.Game.Board.Description.Interfaces
{
    public interface IBoardDescription
    {
        public int RowLimit { get; }
        public int ColumnLimit { get; }
        public string SupportedLetters { get; }
    }
}