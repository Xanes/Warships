using Moq;
using Warships.Game.Board.Description.Interfaces;
using Warships.Game.Engines.Warships;
using Warships.Game.Exceptions;
using Warships.Game.Objects;
using Warships.Game.Ships.Descriptions.Interfaces;
using Warships.Game.Ships.Factory.Interfaces;

namespace WarshipsTests.Engines
{
    public class WarshipEngineTests
    {
        private Mock<IWarshipFactory> _warshipFactory;
        private Mock<IBoardDescription> _boardDescription;
        private WarshipsEngine _warshipsEngine;
        private Mock<IWarshipDescription> _shipDescription;

        [SetUp]
        public void Setup()
        {
            _warshipFactory = new Mock<IWarshipFactory>();
            _boardDescription = new Mock<IBoardDescription>();
            _warshipsEngine = new WarshipsEngine(_warshipFactory.Object, _boardDescription.Object);
            _shipDescription = new Mock<IWarshipDescription>();
        }

        [Test]
        public void WarshipsEngine_Initialize_Ships_Are_Created_And_Position_Are_Assigned()
        {
            //Arrange
            _shipDescription.Setup(s => s.Size).Returns(1);
            Warship warship = new Warship(_shipDescription.Object);
            _warshipFactory.Setup(w => w.CreateWarships()).Returns(new List<Warship>() { warship });
            _boardDescription.Setup(w => w.RowLimit).Returns(1);
            _boardDescription.Setup(w => w.ColumnLimit).Returns(1);

            //Act
            _warshipsEngine.Initialize();

            //Assert
            Assert.IsTrue(warship.Parts.First().Field == new Field(0, 0));
        }

        [Test]
        public void WarshipsEngine_Throws_Exception_When_Numbers_Of_Tries_Are_Reached()
        {
            //Arrange
            _shipDescription.Setup(s => s.Size).Returns(1);
            Warship warship = new Warship(_shipDescription.Object);
            Warship warship2 = new Warship(_shipDescription.Object);
            _warshipFactory.Setup(w => w.CreateWarships()).Returns(new List<Warship>() { warship, warship2 });
            _boardDescription.Setup(w => w.RowLimit).Returns(1);
            _boardDescription.Setup(w => w.ColumnLimit).Returns(1);

            //Act && Assert
            Assert.Throws<GameErrorException>(() => _warshipsEngine.Initialize());
        }

        [Test]
        public void WarshipsEngine_CheckShoot_Ship_Is_Shooted()
        {
            //Arrange
            _shipDescription.Setup(s => s.Size).Returns(1);
            Warship warship = new Warship(_shipDescription.Object);
            _warshipFactory.Setup(w => w.CreateWarships()).Returns(new List<Warship>() { warship });
            _boardDescription.Setup(w => w.RowLimit).Returns(1);
            _boardDescription.Setup(w => w.ColumnLimit).Returns(1);

            //Act
            _warshipsEngine.Initialize();
            _warshipsEngine.CheckShoot(new Field(0, 0));

            //Assert
            Assert.IsTrue(warship.Parts.First().Shooted && _warshipsEngine.AllShipsSink);
        }

        [Test]
        public void WarshipsEngine_CheckShoot_Throw_Exception_if_Engine_Is_Not_Initialized()
        {
            //Arrange
            _shipDescription.Setup(s => s.Size).Returns(1);
            Warship warship = new Warship(_shipDescription.Object);
            _warshipFactory.Setup(w => w.CreateWarships()).Returns(new List<Warship>() { warship });
            _boardDescription.Setup(w => w.RowLimit).Returns(1);
            _boardDescription.Setup(w => w.ColumnLimit).Returns(1);

            //Act && Assert
            Assert.Throws<GameErrorException>(() => _warshipsEngine.CheckShoot(new Field(0, 0)));
        }
    }
}