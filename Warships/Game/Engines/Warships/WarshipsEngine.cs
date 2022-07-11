using Warships.Game.Board.Description.Interfaces;
using Warships.Game.Constants;
using Warships.Game.Enums;
using Warships.Game.Exceptions;
using Warships.Game.Objects;
using Warships.Game.Ships.Factory.Interfaces;

namespace Warships.Game.Engines.Warships
{
    public class WarshipsEngine : IWarshipsEngine
    {
        private readonly IWarshipFactory _warshipFactory;
        private readonly IBoardDescription _boardDescription;

        private IEnumerable<ShipPart>? shipParts;

        public List<Warship> Warships { get; } = new List<Warship>();
        public bool AllShipsSink => shipParts?.All(p => p.Shooted) ?? throw new GameErrorException(Errors.ShipsAreNotPlaced);

        public WarshipsEngine(IWarshipFactory warshipFactory, IBoardDescription boardDescription)
        {
            _warshipFactory = warshipFactory;
            _boardDescription = boardDescription;
        }

        public void Initialize()
        {
            Warships.Clear();
            Warships.AddRange(_warshipFactory.CreateWarships());
            CalculateWarshipPositions();
        }

        public void CheckShoot(Field shoot)
        {
            if (shipParts == null)
            {
                throw new GameErrorException(Errors.ShipsAreNotPlaced);
            }
            var point = shipParts.FirstOrDefault(p => p.Field == shoot);
            if (point != null)
            {
                point.Shooted = true;
            }
        }

        private void CalculateWarshipPositions()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            foreach (var warship in Warships)
            {
                warship.AssignPosition(GetWatshipPosition(warship, rnd));
            }
            shipParts = Warships.SelectMany(w => w.Parts);
        }

        private ShipPart[] GetWatshipPosition(Warship warship, Random rnd, int numberOfTry = 0)
        {
            if (numberOfTry >= GameConstants.LimitOfTry)
            {
                throw new GameErrorException(Errors.CanNotFindFleetFormation);
            }
            var orientation = (Orientation)(rnd.Next(0, GameConstants.LimitOfTry) % 2);
            IEnumerable<ShipPart> parts;
            if (orientation == Orientation.Vertical)
            {
                parts = CreateVerticalPositionCandidate(warship, rnd);
            }
            else
            {
                parts = CreateHorizontalPositionCandidate(warship, rnd);
            }
            if (PositionCandidateHaveCollision(warship, parts))
            {
                return GetWatshipPosition(warship, rnd, ++numberOfTry);
            }

            return parts.ToArray();
        }

        private IEnumerable<ShipPart> CreateVerticalPositionCandidate(Warship warship, Random rnd)
        {
            IEnumerable<ShipPart> parts;
            var Column = rnd.Next(0, _boardDescription.ColumnLimit);
            var Row = rnd.Next(0, _boardDescription.RowLimit - warship.Description.Size + 1);
            parts = Enumerable.Range(Row, warship.Description.Size).Select(row => new ShipPart(new Field(row, Column), warship));
            return parts;
        }

        private IEnumerable<ShipPart> CreateHorizontalPositionCandidate(Warship warship, Random rnd)
        {
            IEnumerable<ShipPart> parts;
            var Column = rnd.Next(0, _boardDescription.ColumnLimit - warship.Description.Size + 1);
            var Row = rnd.Next(0, _boardDescription.RowLimit);
            parts = Enumerable.Range(Column, warship.Description.Size).Select(column => new ShipPart(new Field(Row, column), warship));
            return parts;
        }

        private bool PositionCandidateHaveCollision(Warship warship, IEnumerable<ShipPart> parts)
        {
            return Warships.Where(w => w != warship && w.Parts.All(p => p != null)).Any(w => w.Parts.Any(p => parts.Any(c => c.Field == p.Field)));
        }
    }
}