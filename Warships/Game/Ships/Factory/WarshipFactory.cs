using Warships.Game.Board.Description.Interfaces;
using Warships.Game.Exceptions;
using Warships.Game.Objects;
using Warships.Game.Ships.Descriptions.Interfaces;
using Warships.Game.Ships.Factory.Interfaces;

namespace Warships.Game.Ships.Factory
{
    public class WarshipFactory : IWarshipFactory
    {
        private readonly IEnumerable<IWarshipDescription> _descriptions;
        private readonly IBoardDescription _boardDescription;

        public WarshipFactory(IEnumerable<IWarshipDescription> descriptions, IBoardDescription boardDescription)
        {
            _descriptions = descriptions;
            _boardDescription = boardDescription;
        }

        public IEnumerable<Warship> CreateWarships()
        {
            if (!_descriptions.Any())
            {
                throw new GameErrorException(Errors.NoDescriptionRegistered);
            }
            if (_descriptions.Any(d => d.Size > _boardDescription.RowLimit || d.Size > _boardDescription.ColumnLimit))
            {
                throw new GameErrorException(Errors.AtLeastOneShipIsToBig);
            }

            return _descriptions.Select(d => new Warship(d));
        }
    }
}