using Warships.Game.Constants;
using Warships.Game.Engines.Players;
using Warships.Game.Engines.Warships;
using Warships.Game.Exceptions;
using Warships.Game.Interfaces.BoardRenderer;
using Warships.Game.Interfaces.InputOutput;
using Warships.Game.Objects;

namespace Warships.Game.Engines.Game
{
    public class GameEngine : IGameEngine
    {
        private readonly IGameInputOutput _gameInputOutput;
        private readonly IPlayerEngine _playerEngine;
        private readonly IWarshipsEngine _warshipEngine;
        private readonly IBoardRenderer _boardRenderer;

        public GameEngine(IGameInputOutput gameInputOutput, IPlayerEngine playerEngine, IWarshipsEngine warshipEngine, IBoardRenderer boardRenderer)
        {
            _gameInputOutput = gameInputOutput;
            _playerEngine = playerEngine;
            _warshipEngine = warshipEngine;
            _boardRenderer = boardRenderer;
        }

        public bool GameInProgress { get; private set; }

        public void PlayRound()
        {
            if (_playerEngine.Player == null || !_warshipEngine.Warships.Any())
            {
                throw new GameErrorException(Errors.GameIsNotInitializedProperly);
            }
            _boardRenderer.RenderBoard(_warshipEngine.Warships, _playerEngine.Player);
            if (_warshipEngine.AllShipsSink)
            {
                EndGame();
                return;
            }
            Field shoot = _playerEngine.AskForCoordinates();
            _warshipEngine.CheckShoot(shoot);
        }

        public void StartGame()
        {
            _playerEngine.CreatePlayer();
            _warshipEngine.Initialize();
            GameInProgress = true;
        }

        public void DisplayNotification(string message)
        {
            _gameInputOutput.RenderMessage(message);
        }

        private void EndGame()
        {
            GameInProgress = false;
            _gameInputOutput.RenderMessage(GameConstants.VictoryMessage);
            _gameInputOutput.AskForInput();
        }
    }
}