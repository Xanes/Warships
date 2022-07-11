namespace Warships.Game.Objects
{
    public class ShipPart
    {
        public ShipPart(Field field, Warship warship)
        {
            Warship = warship;
            Field = field;
        }

        public Field Field { get; }

        public bool Shooted { get; set; }
        
        public Warship Warship { get; }
    }
}