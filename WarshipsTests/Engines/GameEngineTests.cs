using Moq;
using Warships.Game.Engines.Game;
using Warships.Game.Engines.Players;
using Warships.Game.Engines.Warships;
using Warships.Game.Exceptions;
using Warships.Game.Interfaces.BoardRenderer;
using Warships.Game.Interfaces.InputOutput;
using Warships.Game.Objects;
using Warships.Game.Ships.Descriptions.Interfaces;

namespace WarshipsTests.Engines
{
    public class GameEngineTests
    {
        private Mock<IGameInputOutput> _gameInputOutput;
        private Mock<IPlayerEngine> _playerEngine;
        private Mock<IWarshipsEngine> _warshipEngine;
        private Mock<IBoardRenderer> _boardRenderer;
        private GameEngine _gameEngine;

        [SetUp]
        public void Setup()
        {
            _gameInputOutput = new Mock<IGameInputOutput>();
            _playerEngine = new Mock<IPlayerEngine>();
            _warshipEngine = new Mock<IWarshipsEngine>();
            _boardRenderer = new Mock<IBoardRenderer>();
            _gameEngine = new GameEngine(_gameInputOutput.Object, _playerEngine.Object, _warshipEngine.Object, _boardRenderer.Object);
        }

        [Test]
        public void GameEngine_StartGame_GameIsStarted()
        {
            //Act
            _gameEngine.StartGame();

            //Assert
            _playerEngine.Verify(p => p.CreatePlayer(), Times.Once);
            _warshipEngine.Verify(w => w.Initialize(), Times.Once);
            Assert.That(_gameEngine.GameInProgress, Is.EqualTo(true));
        }

        [Test]
        public void GameEngine_PlayRound_Throw_Exception_If_Game_Is_Not_Initialized()
        {
            //Act && Assert
            Assert.Throws<GameErrorException>(() => _gameEngine.PlayRound());
        }

        [Test]
        public void GameEngine_PlayRound_EndGame_If_All_Ships_Are_Sink()
        {
            //Arrange
            _warshipEngine.Setup(w => w.Warships).Returns(new List<Warship>() { new Warship(new Mock<IWarshipDescription>().Object) });
            _warshipEngine.Setup(w => w.AllShipsSink).Returns(true);
            _playerEngine.Setup(p => p.Player).Returns(new Player());

            //Act
            _gameEngine.PlayRound();

            //Assert
            _gameInputOutput.Verify(g => g.AskForInput(null), Times.Once);
            _gameInputOutput.Verify(g => g.RenderMessage(It.IsAny<string>()), Times.Once);
            Assert.That(_gameEngine.GameInProgress, Is.EqualTo(false));
        }

        [Test]
        public void GameEngine_PlayRound_play()
        {
            //Arrange
            Player player = new Player();
            List<Warship> warships = new List<Warship>() { new Warship(new Mock<IWarshipDescription>().Object) };
            _warshipEngine.Setup(w => w.Warships).Returns(warships);
            _warshipEngine.Setup(w => w.AllShipsSink).Returns(false);
            _playerEngine.Setup(p => p.Player).Returns(player);
            Field field = new Field(0, 0);
            _playerEngine.Setup(p => p.AskForCoordinates()).Returns(field);

            //Act
            _gameEngine.PlayRound();

            //Assert
            _boardRenderer.Verify(b => b.RenderBoard(warships, player), Times.Once);
            _warshipEngine.Verify(w => w.CheckShoot(field), Times.Once);
            Assert.That(_gameEngine.GameInProgress, Is.EqualTo(false));
        }

        [Test]
        public void GameEngine_DisplayNotification_Should_Invoke_InputOutput()
        {
            //Arrange
            string input = "test";

            //Act
            _gameEngine.DisplayNotification(input);

            _gameInputOutput.Verify(g => g.RenderMessage(input), Times.Once);
        }
    }
}