using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warships.Game.Exceptions
{
    public class ExitException : Exception
    {
        public override string Message => Errors.ExitMessage;
    }
}
