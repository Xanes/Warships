using Moq;
using Warships.Game.Board.Description.Interfaces;
using Warships.Game.Exceptions;
using Warships.Game.Objects;
using Warships.Game.Ships.Descriptions.Interfaces;
using Warships.Infrastructure.BoardRenderer;
using Warships.Infrastructure.Wrappers;

namespace WarshipsTests.Infrastructure
{
    public class BoardRendererTests
    {
        private Mock<IConsole> consoleMock;
        private Mock<IBoardDescription> boardMock;
        private BoardRenderer boardRenderer;

        [SetUp]
        public void Setup()
        {
            consoleMock = new Mock<IConsole>();
            boardMock = new Mock<IBoardDescription>();
            boardRenderer = new BoardRenderer(consoleMock.Object, boardMock.Object);
        }

        [Test]
        public void BoardRenderer_RenderBoard_If_Fleet_is_Empty()
        {
            //Act & Assert
            Assert.Throws<GameErrorException>(() => boardRenderer.RenderBoard(new List<Warship>(), new Player()));
        }

        [Test]
        public void BoardRenderer_RenderBoard_If_Fleet_dont_have_position()
        {
            //Arrange
            Mock<IWarshipDescription> descriptionMock = new Mock<IWarshipDescription>();
            descriptionMock.Setup(d => d.Size).Returns(1);
            List<Warship> fleet = new List<Warship>() { new Warship(descriptionMock.Object) };

            //Act & Assert
            Assert.Throws<GameErrorException>(() => boardRenderer.RenderBoard(new List<Warship>(), new Player()));
        }

        [TestCase(3, 3, "ABC")]
        [TestCase(4, 4, "ABCD")]
        [TestCase(10, 10, "ABCDEFGHIJ")]
        public void BoardRenderer_RenderBoard_BoardIsRenderedCorrectly(int rowLimit, int columnLimit, string supportedLetters)
        {
            //Arrange
            Mock<IWarshipDescription> descriptionMock = new Mock<IWarshipDescription>();

            descriptionMock.Setup(d => d.Size).Returns(1);
            List<Warship> fleet = new List<Warship>() { new Warship(descriptionMock.Object) };
            fleet.First().AssignPosition(new[] { new ShipPart(new Field(0, 0), fleet.First()) });
            Player player = new Player();

            boardMock.Setup(b => b.ColumnLimit).Returns(columnLimit);
            boardMock.Setup(b => b.RowLimit).Returns(rowLimit);
            boardMock.Setup(b => b.SupportedLetters).Returns(supportedLetters);

            //Act
            boardRenderer.RenderBoard(fleet, player);

            //Assert
            consoleMock.Verify(c => c.WriteLine("  " + supportedLetters), Times.Once);
            consoleMock.Verify(c => c.Clear(), Times.Once);
            for (int i = 1; i <= rowLimit; i++)
            {
                if(i < 10)
                {
                    consoleMock.Verify(c => c.WriteLine($" {i}" + new string(' ', columnLimit)), Times.Once);
                }
                else
                {
                    consoleMock.Verify(c => c.WriteLine($"{i}" + new string(' ', columnLimit)), Times.Once);
                }
                
            }
        }

        [Test]
        public void BoardRenderer_RenderBoard_DisplayPlayerMiss()
        {
            //Arrange
            Mock<IWarshipDescription> descriptionMock = new Mock<IWarshipDescription>();

            descriptionMock.Setup(d => d.Size).Returns(1);
            List<Warship> fleet = new List<Warship>() { new Warship(descriptionMock.Object) };
            fleet.First().AssignPosition(new[] { new ShipPart(new Field(0, 1), fleet.First()) });
            Player player = new Player();
            player.AddChoice(new Field(0, 0));

            boardMock.Setup(b => b.ColumnLimit).Returns(2);
            boardMock.Setup(b => b.RowLimit).Returns(2);
            boardMock.Setup(b => b.SupportedLetters).Returns("AB");

            //Act
            boardRenderer.RenderBoard(fleet, player);

            //Assert
            consoleMock.Verify(c => c.WriteLine(" 1M "), Times.Once);
        }

        [Test]
        public void BoardRenderer_RenderBoard_DisplayPlayerHit()
        {
            //Arrange
            Mock<IWarshipDescription> descriptionMock = new Mock<IWarshipDescription>();

            descriptionMock.Setup(d => d.Size).Returns(1);
            List<Warship> fleet = new List<Warship>() { new Warship(descriptionMock.Object) };
            fleet.First().AssignPosition( new[] { new ShipPart(new Field(0, 0), fleet.First()) });
            Player player = new Player();
            player.AddChoice(new Field(0, 0));

            boardMock.Setup(b => b.ColumnLimit).Returns(2);
            boardMock.Setup(b => b.RowLimit).Returns(2);
            boardMock.Setup(b => b.SupportedLetters).Returns("AB");

            //Act
            boardRenderer.RenderBoard(fleet, player);

            //Assert
            consoleMock.Verify(c => c.WriteLine(" 1H "), Times.Once);
        }

        [Test]
        public void BoardRenderer_RenderBoard_DisplayPlayerSink()
        {
            //Arrange
            Mock<IWarshipDescription> descriptionMock = new Mock<IWarshipDescription>();

            descriptionMock.Setup(d => d.Size).Returns(1);
            List<Warship> fleet = new List<Warship>() { new Warship(descriptionMock.Object) };
            fleet.First().AssignPosition(new[] { new ShipPart(new Field(0, 0), fleet.First()) });
            fleet.First().Parts.First().Shooted = true;
            Player player = new Player();
            player.AddChoice(new Field(0, 0));

            boardMock.Setup(b => b.ColumnLimit).Returns(2);
            boardMock.Setup(b => b.RowLimit).Returns(2);
            boardMock.Setup(b => b.SupportedLetters).Returns("AB");

            //Act
            boardRenderer.RenderBoard(fleet, player);

            //Assert
            consoleMock.Verify(c => c.WriteLine(" 1S "), Times.Once);
        }
    }
}