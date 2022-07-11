using Warships.Game.Objects;

namespace Warships.Game.Engines.Players
{
    public interface IPlayerEngine
    {
        void CreatePlayer();

        Field AskForCoordinates();

        Player? Player { get; }
    }
}