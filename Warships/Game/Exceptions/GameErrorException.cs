namespace Warships.Game.Exceptions
{
    public class GameErrorException : Exception
    {
        public GameErrorException(string? message) : base(message)
        {
        }
    }
}