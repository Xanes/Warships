using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warships.Game.Exceptions;
using Warships.Game.Objects;
using Warships.Game.Ships.Descriptions.Interfaces;

namespace WarshipsTests.Objects
{
    public class WarshipTests
    {
        private Warship warship;
        private Mock<IWarshipDescription> _description;

        [SetUp]
        public void Setup()
        {
            _description = new Mock<IWarshipDescription>();
            _description.Setup(d => d.Size).Returns(1);
            warship = new Warship(_description.Object);
        }

        [Test]
        public void Warship_AssignPossition_Throw_Exception_If_input_not_maching_warship_size()
        {
            //Act & Assert
            Assert.Throws<GameErrorException>(() => warship.AssignPosition(new List<ShipPart>()));
        }

        [Test]
        public void Warship_AssignPossition_Throw_Exception_If_position_is_asigned_second_time()
        {
            //Act & Assert
            warship.AssignPosition(new List<ShipPart>() { new ShipPart(new Field(0, 0), warship) });

            Assert.Throws<GameErrorException>(() => warship.AssignPosition(new List<ShipPart>() { new ShipPart(new Field(0, 0), warship) }));
        }
    }
}
