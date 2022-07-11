namespace Warships.Game.Engines.Game
{
    public interface IGameEngine
    {
        public bool GameInProgress { get; }

        public void StartGame();

        public void PlayRound();

        void DisplayNotification(string message);
    }
}