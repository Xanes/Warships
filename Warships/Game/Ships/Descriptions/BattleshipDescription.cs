using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warships.Game.Ships.Descriptions.Interfaces;

namespace Warships.Game.Ships.Descriptions
{
    public class BattleshipDescription : IWarshipDescription
    {
        public int Size => 5;
    }
}
