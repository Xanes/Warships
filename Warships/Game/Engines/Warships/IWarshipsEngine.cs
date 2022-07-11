using Warships.Game.Objects;

namespace Warships.Game.Engines.Warships
{
    public interface IWarshipsEngine
    {
        List<Warship> Warships { get; }

        bool AllShipsSink { get; }

        void Initialize();

        void CheckShoot(Field shoot);
    }
}