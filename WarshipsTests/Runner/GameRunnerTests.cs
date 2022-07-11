using Moq;
using Warships.Game.Engines.Game;
using Warships.Game.Exceptions;
using Warships.Game.Runner;

namespace WarshipsTests.Runner
{
    public class GameRunnerTests
    {
        private GameRunner _gameRunner;
        private Mock<IGameEngine> _gameEngine;

        [SetUp]
        public void Setup()
        {
            _gameEngine = new Mock<IGameEngine>();
            _gameRunner = new GameRunner(_gameEngine.Object);
        }

        [Test]
        public void GameRunner_StartGame_PlayedThreeRounds()
        {
            //Arrange
            int roundCounter = 0;
            _gameEngine.Setup(x => x.GameInProgress).Returns(() =>
            {
                if (roundCounter < 3)
                {
                    roundCounter++;
                    return true;
                }

                return false;
            });

            //Act
            _gameRunner.StartGame();

            //Assert
            _gameEngine.Verify(g => g.StartGame(), Times.Once);
            _gameEngine.Verify(g => g.PlayRound(), Times.Exactly(3));
        }

        [Test]
        public void GameRunner_StartGame_DisplayCriticalError()
        {
            //Arrange
            _gameEngine.Setup(x => x.StartGame()).Throws(new Exception());

            //Act
            _gameRunner.StartGame();

            //Assert
            _gameEngine.Verify(g => g.DisplayNotification(Errors.CriticalError), Times.Once);
        }

        [Test]
        public void GameRunner_StartGame_DisplayExitMessage()
        {
            //Arrange
            ExitException exitException = new ExitException();
            _gameEngine.Setup(x => x.StartGame()).Throws(exitException);

            //Act
            _gameRunner.StartGame();

            //Assert
            _gameEngine.Verify(g => g.DisplayNotification(exitException.Message), Times.Once);
        }

        [Test]
        public void GameRunner_StartGame_DisplayGameErrorMessage()
        {
            //Arrange
            string gameError = "gameError";
            GameErrorException gameErrorMessage = new GameErrorException(gameError);
            _gameEngine.Setup(x => x.StartGame()).Throws(gameErrorMessage);

            //Act
            _gameRunner.StartGame();

            //Assert
            _gameEngine.Verify(g => g.DisplayNotification(gameError), Times.Once);
        }
    }
}