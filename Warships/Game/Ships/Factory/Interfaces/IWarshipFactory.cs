using Warships.Game.Objects;
using Warships.Game.Ships.Descriptions.Interfaces;

namespace Warships.Game.Ships.Factory.Interfaces
{
    public interface IWarshipFactory
    {
        IEnumerable<Warship> CreateWarships();
    }
}