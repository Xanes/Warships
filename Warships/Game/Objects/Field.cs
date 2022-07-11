namespace Warships.Game.Objects
{
    public record Field
    {
        public Field(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public int Column { get; }
        public int Row { get; }
    }
}