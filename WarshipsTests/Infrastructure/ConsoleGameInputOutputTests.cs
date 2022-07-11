using Moq;
using Warships.Game.Exceptions;
using Warships.Infrastructure.InputOutput;
using Warships.Infrastructure.Wrappers;

namespace WarshipsTests.Infrastructure
{
    public class ConsoleGameInputOutputTests
    {
        private Mock<IConsole> _consoleMock;
        private ConsoleGameInputOutput _consoleGameInputOutput;

        [SetUp]
        public void Setup()
        {
            _consoleMock = new Mock<IConsole>();
            _consoleGameInputOutput = new ConsoleGameInputOutput(_consoleMock.Object);
        }

        [TestCase("TestMessage1")]
        [TestCase("TestMessage2")]
        [TestCase("Hello Test")]
        public void ConsoleGameInputOutput_RenderMessage_Invoke_Console_ReadLine_With_Proper_Args(string message)
        {
            //Act
            _consoleGameInputOutput.RenderMessage(message);

            //Assert
            _consoleMock.Verify(c => c.WriteLine(message), Times.Once);
        }

        [Test]
        public void ConsoleGameInputOutput_AskForInput_ThrowExitError_If_input_is_Equal_To_Exit()
        {
            //Arrange
            _consoleMock.Setup(c => c.ReadLine()).Returns("close");


            //Act & Assert
            Assert.Throws<ExitException>(() => _consoleGameInputOutput.AskForInput());
        }

        [Test]
        public void ConsoleGameInputOutput_AskForInput_ReturnsExpectedResult()
        {
            //Arrange
            string? expectedResult = "testResult";
            _consoleMock.Setup(c => c.ReadLine()).Returns(expectedResult);

            //Act
            string? result = _consoleGameInputOutput.AskForInput();

            //Assert
            _consoleMock.Verify(c => c.WriteLine(It.IsAny<string>()), Times.Never);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ConsoleGameInputOutput_AskForInput_ReturnsExpectedResult_And_Display_Message()
        {
            //Arrange
            string? expectedResult = "testResult";
            string? message = "testMessage";
            _consoleMock.Setup(c => c.ReadLine()).Returns(expectedResult);

            //Act
            string? result = _consoleGameInputOutput.AskForInput(message);

            //Assert
            _consoleMock.Verify(c => c.WriteLine(message), Times.Once);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}