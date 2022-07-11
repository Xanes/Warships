using Warships.Game.Objects;

namespace Warships.Game.Interfaces.BoardRenderer
{
    public interface IBoardRenderer
    {
        void RenderBoard(IEnumerable<Warship> warships, Player player);
    }
}