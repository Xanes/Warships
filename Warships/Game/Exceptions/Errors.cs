using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warships.Game.Exceptions
{
    public static class Errors
    {
        public static string AtLeastOneShipIsToBig => "Sorry at least one Ship cannot fit into the board.";
        public static string ShipsAreNotPlaced => "Ships are not placed.";
        public static string CanNotFindFleetFormation => "Can not find fleet formation.";
        public static string PlayerIsNotCreated => "Player Is Not Created.";
        public static string GameIsNotInitializedProperly => "Game is not initialized properly";
        public static string CriticalError => "Something went wrong, please contact support.";
        public static string NoDescriptionRegistered => "No ship description registered";
        public static string ExitMessage => "You choose to leave game!";
        public static string ComponentsAreNotMatchingWarShipDescription => "Components are not maching warship description";
        public static string WarshipPositionCanNotBeChanged => "Warship Position can not be changed";
    }
}
