using MSO_P2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MSO_P2_Test
{
    public class UnitTestCommands
    {
        #region TestMoveCommands
        [Fact]
        public void TestTurnCommand()
        {
            Character p1 = new Character(new Point(0, 0), Direction.ViewDir.North);

            TurnCommand test = new TurnCommand("right");

            test.Execute(p1);

            Assert.Equal(Direction.ViewDir.East, p1.direction);
        }

        [Fact]
        public void TestMoveCommand()
        {
            Character p1 = new Character(new Point(0, 0), Direction.ViewDir.North);
            MoveCommand move = new MoveCommand(10);

            move.Execute(p1);

            Assert.Equal(-10, p1.position.Y);

        }

        [Fact]
        public void TestRepeatCommand()
        {
            Character p1 = new Character(new Point(0,0), Direction.ViewDir.North);
            List<ICommand> commands = new List<ICommand>();

            commands.Add(new MoveCommand(10));
            commands.Add(new TurnCommand("left"));

            RepeatCommand repeat = new RepeatCommand(commands, 2);
            repeat.Execute(p1);

            Assert.Equal(new Point(-10, -10), p1.position);

        }
        #endregion

    }
}
