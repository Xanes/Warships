using Warships.Game.Exceptions;
using Warships.Game.Ships.Descriptions.Interfaces;

namespace Warships.Game.Objects
{
    public class Warship
    {
        private ShipPart[] _shipParts;
        public Warship(IWarshipDescription description)
        {
            Description = description;
            _shipParts = new ShipPart[Description.Size];
        }

        public IWarshipDescription Description { get; }
        public IReadOnlyCollection<ShipPart> Parts => _shipParts;

        public void AssignPosition(IEnumerable<ShipPart> shipParts)
        {
            if(shipParts.Count() != Description.Size)
            {
                throw new GameErrorException(Errors.ComponentsAreNotMatchingWarShipDescription);
            }

            if(_shipParts.Any(p => p != null))
            {
                throw new GameErrorException(Errors.WarshipPositionCanNotBeChanged);
            }

            _shipParts = shipParts.ToArray();
        }


    }
}