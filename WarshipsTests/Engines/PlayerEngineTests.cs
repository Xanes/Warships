using Moq;
using Warships.Game.Board.Description.Interfaces;
using Warships.Game.Engines.Players;
using Warships.Game.Exceptions;
using Warships.Game.Interfaces.InputOutput;
using Warships.Game.Objects;

namespace WarshipsTests.Engines
{
    public class PlayerEngineTests
    {
        private Mock<IGameInputOutput> _gameInputOutput;
        private Mock<IBoardDescription> _boardDescription;
        private PlayerEngine _playerEngine;

        [SetUp]
        public void Setup()
        {
            _boardDescription = new Mock<IBoardDescription>();
            _boardDescription.Setup(b => b.SupportedLetters).Returns("AB");
            _boardDescription.Setup(b => b.RowLimit).Returns(2);
            _boardDescription.Setup(b => b.ColumnLimit).Returns(2);
            _gameInputOutput = new Mock<IGameInputOutput>();

            _playerEngine = new PlayerEngine(_gameInputOutput.Object, _boardDescription.Object);
        }

        [Test]
        public void PlayerEngine_CreatePlayer()
        {
            //Act
            _playerEngine.CreatePlayer();

            //Assert
            Assert.NotNull(_playerEngine.Player);
        }

        [Test]
        public void PlayerEngine_AskCoordinates_throw_Error_If_Player_Is_Not_Created()
        {
            //Act && Assert
            Assert.Throws<GameErrorException>(() => _playerEngine.AskForCoordinates());
        }

        [Test]
        public void PlayerEngine_AskForCoordinates_Choise_Is_Added_To_player_List()
        {
            //Arrange
            _gameInputOutput.Setup(g => g.AskForInput(It.IsAny<string>())).Returns("A1");

            //Act
            _playerEngine.CreatePlayer();
            _playerEngine.AskForCoordinates();

            //Assert
            Assert.IsNotNull(_playerEngine.Player);
            Assert.That(_playerEngine.Player.Choises.First(), Is.EqualTo(new Field(0, 0)));
        }

        [Test]
        public void PlayerEngine_AskForCoordinates_Try_Again_If_Player_Repeat_Choise()
        {
            //Arrange
            int i = 0;
            _gameInputOutput.Setup(g => g.AskForInput(It.IsAny<string>())).Returns(() =>
            {
                if (i == 0)
                {
                    i++;
                    return "A1";
                }
                else
                {
                    return "A2";
                }
            });

            //Act
            _playerEngine.CreatePlayer();
            if (_playerEngine.Player != null)
            {
                _playerEngine.Player.AddChoice(new Field(0, 0));
            }

            _playerEngine.AskForCoordinates();

            //Assert
            Assert.IsNotNull(_playerEngine.Player);
            Assert.That(_playerEngine.Player.Choises.ToArray()[1], Is.EqualTo(new Field(0, 1)));
        }

        [Test]
        public void PlayerEngine_AskForCoordinates_Try_Again_If_Player_Type_Wrong_Input()
        {
            //Arrange
            int i = 0;
            _gameInputOutput.Setup(g => g.AskForInput(It.IsAny<string>())).Returns(() =>
            {
                if (i == 0)
                {
                    i++;
                    return "A5";
                }
                else
                {
                    return "A1";
                }
            });

            //Act
            _playerEngine.CreatePlayer();

            _playerEngine.AskForCoordinates();

            //Assert
            Assert.IsNotNull(_playerEngine.Player);
            Assert.That(_playerEngine.Player.Choises.First(), Is.EqualTo(new Field(0, 0)));
        }
    }
}