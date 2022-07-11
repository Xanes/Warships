using Moq;
using Warships.Game.Board.Description.Interfaces;
using Warships.Game.Exceptions;
using Warships.Game.Objects;
using Warships.Game.Ships.Descriptions.Interfaces;
using Warships.Game.Ships.Factory;

namespace WarshipsTests.Factory
{
    public class WarshipFactoryTests
    {
        private WarshipFactory _warshipFactory;
        private Mock<IBoardDescription> _boardDescriptionMock;
        private Mock<IWarshipDescription> _warshipDescriptionMock;

        [SetUp]
        public void Setup()
        {
            _boardDescriptionMock = new Mock<IBoardDescription>();
            _warshipDescriptionMock = new Mock<IWarshipDescription>();
            _warshipFactory = new WarshipFactory(new List<IWarshipDescription>() { _warshipDescriptionMock.Object }, _boardDescriptionMock.Object);
        }

        [Test]
        public void WarshipFactory_CreateWarships_Throw_Error_When_No_Description_Registered()
        {
            //Arrgange
            WarshipFactory warshipFactory_local = new WarshipFactory(new List<IWarshipDescription>(), _boardDescriptionMock.Object);

            //Act && Assert
            Assert.Throws<GameErrorException>(() => warshipFactory_local.CreateWarships());
        }

        [Test]
        public void WarshipFactory_CreateWarships_Throw_Error_When_Ship_Is_Bigger_Than_Map()
        {
            //Arrgange
            _warshipDescriptionMock.Setup(w => w.Size).Returns(5);
            _boardDescriptionMock.Setup(b => b.RowLimit).Returns(3);
            _boardDescriptionMock.Setup(b => b.ColumnLimit).Returns(3);

            //Act && Assert
            Assert.Throws<GameErrorException>(() => _warshipFactory.CreateWarships());
        }

        [Test]
        public void WarshipFactory_CreateWarships_WarshipIsCreated()
        {
            //Arrgange
            _warshipDescriptionMock.Setup(w => w.Size).Returns(2);
            _boardDescriptionMock.Setup(b => b.RowLimit).Returns(10);
            _boardDescriptionMock.Setup(b => b.ColumnLimit).Returns(10);

            //Act
            IEnumerable<Warship> warships = _warshipFactory.CreateWarships();

            //Act && Assert
            Assert.That(warships.Count(), Is.EqualTo(1));
            Assert.That(warships.First().Parts.Count, Is.EqualTo(_warshipDescriptionMock.Object.Size));
        }
    }
}