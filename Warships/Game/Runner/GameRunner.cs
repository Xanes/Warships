using Warships.Game.Engines.Game;
using Warships.Game.Exceptions;

namespace Warships.Game.Runner
{
    public class GameRunner : IGameRunner
    {
        private readonly IGameEngine _gameEngine;

        public GameRunner(IGameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        public void StartGame()
        {
            try
            {
                _gameEngine.StartGame();
                while (_gameEngine.GameInProgress)
                {
                    _gameEngine.PlayRound();
                }
            }
            catch (GameErrorException gameError)
            {
                _gameEngine.DisplayNotification(gameError.Message);
            }
            catch (ExitException exitRequest)
            {
                _gameEngine.DisplayNotification(exitRequest.Message);
            }
            catch (Exception)
            {
                _gameEngine.DisplayNotification(Errors.CriticalError);
            }
        }
    }
}