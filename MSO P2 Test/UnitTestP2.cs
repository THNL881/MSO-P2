using MSO_P2;
using NuGet.Frameworks;
using System.Drawing;

namespace MSO_P2_Test
{
	public class UnitTestP2
	{
		#region UnitTestMetric
		#region CalculateNumberOfCommands() Tests
		[Fact]
		public void TestNumberCommands_Empty()
		{
			int commandNumber = Metric.CalculateNumberOfCommands([]);

			Assert.Equal(0, commandNumber);
		}

		[Fact]
		public void TestNumberCommands_NotNested()
		{
			List<ICommand> commands = [new TurnCommand("left"), new MoveCommand(3), new TurnCommand("left"), new MoveCommand(2),
									   new MoveCommand(3), new TurnCommand("right"), new MoveCommand(1), new TurnCommand("left")];

			int commandNumber = Metric.CalculateNumberOfCommands(commands);

			Assert.Equal(8, commandNumber);
		}

		[Fact]
		public void TestNumberCommands_SingleNested()
		{
			List<ICommand> commands = [new RepeatCommand([new MoveCommand(2), new TurnCommand("left")], 3), new MoveCommand(4)];

			int commandNumber = Metric.CalculateNumberOfCommands(commands);

			Assert.Equal(4, commandNumber);
		}

		[Fact]
		public void TestNumberCommands_DoubleNested()
		{
			List<ICommand> commands = [new MoveCommand(2), new RepeatCommand([new RepeatCommand([new TurnCommand("right")], 2), new MoveCommand(2)], 3)];

			int commandNumber = Metric.CalculateNumberOfCommands(commands);

			Assert.Equal(5, commandNumber);
		}
		#endregion

		#region CalculateNumberOfRepeats() Tests
		[Fact]
		public void TestNumberRepeats_Empty()
		{
			int repeatNumber = Metric.CalculateNumberOfRepeats([]);

			Assert.Equal(0, repeatNumber);
		}

		[Fact]
		public void TestNumberRepeats_NoRepeat()
		{
			List<ICommand> commands = [new MoveCommand(2), new TurnCommand("right"), new TurnCommand("right"), new MoveCommand(5)];

			int repeatNumber = Metric.CalculateNumberOfRepeats(commands);
			
			Assert.Equal(0, repeatNumber);
		}

		[Fact]
		public void TestNumberRepeats_SingleRepeat()
		{
			List<ICommand> commands = [new MoveCommand(2), new RepeatCommand([new TurnCommand("right")], 2), new MoveCommand(5)];

			int repeatNumber = Metric.CalculateNumberOfRepeats(commands);

			Assert.Equal(1, repeatNumber);
		}

		[Fact]
		public void TestNumberRepeats_MultipleRepeat()
		{
			List<ICommand> commands = [new RepeatCommand([new MoveCommand(2), new TurnCommand("left")], 3),
									   new RepeatCommand([new TurnCommand("right"), new MoveCommand(2)], 3)];

			int repeatNumber = Metric.CalculateNumberOfRepeats(commands);

			Assert.Equal(2, repeatNumber);
		}

		[Fact]
		public void TestNumberRepeats_NestedRepeat()
		{
			List<ICommand> commands = [new RepeatCommand([new RepeatCommand([new MoveCommand(2), new RepeatCommand([new TurnCommand("left")], 2)], 3), 
									   new MoveCommand(2)], 4)];

			int repeatNumber = Metric.CalculateNumberOfRepeats(commands);

			Assert.Equal(3, repeatNumber);
		}
		#endregion

		#region CalculateNestingLevel() Tests
		[Fact]
		public void TestNestingLevel_LevelOf0()
		{
			List<ICommand> commands = [new TurnCommand("right"), new MoveCommand(2), new TurnCommand("left")];

			int nestingLevel = Metric.CalculateNestingLevel(commands);

			Assert.Equal(0, nestingLevel);
		}

		[Fact]
		public void TestNestingLevel_LevelOf1()
		{
			List<ICommand> commands = [new RepeatCommand([new TurnCommand("right"), new MoveCommand(2), new TurnCommand("left")], 2)];

			int nestingLevel = Metric.CalculateNestingLevel(commands);

			Assert.Equal(1, nestingLevel);
		}

		[Fact]
		public void TestNestingLevel_LevelOf2()
		{
			List<ICommand> commands = [new RepeatCommand([new TurnCommand("right"),
									   new RepeatCommand([new MoveCommand(2), new TurnCommand("left")], 3)], 2)];

			int nestingLevel = Metric.CalculateNestingLevel(commands);

			Assert.Equal(2, nestingLevel);
		}

		[Fact]
		public void TestNestingLevel_SameLevel1Repeats()
		{
			List<ICommand> commands = [new RepeatCommand([new TurnCommand("right"), new MoveCommand(2)], 2),
									   new RepeatCommand([new TurnCommand("left")], 3)];

			int nestingLevel = Metric.CalculateNestingLevel(commands);

			Assert.Equal(1, nestingLevel);
		}

		[Fact]
		public void TestNestingLevel_SameLevel2Repeats()
		{
			List<ICommand> commands = [new RepeatCommand([new RepeatCommand([new TurnCommand("right")], 1)], 2),  
									   new RepeatCommand([new RepeatCommand([new TurnCommand("left")], 1)], 3)];

			int nestingLevel = Metric.CalculateNestingLevel(commands);

			Assert.Equal(2, nestingLevel);
		}
		#endregion
		#endregion

		#region UnitTestCommands
		#region TurnCommand Tests
		[Fact]
		public void TestTurnCommand()
		{
			Character p1 = new Character(new Point(0, 0), Direction.ViewDir.North);
			TurnCommand test = new TurnCommand("right");

			test.Execute(p1);

			Assert.Equal(Direction.ViewDir.East, p1.direction);
		}

		[Fact]
		public void TestTurnCommand_CyclicEnum_NorthToWest()
		{
			Character p1 = new Character(new Point(0, 0), Direction.ViewDir.North);
			TurnCommand test = new TurnCommand("left");

			test.Execute(p1);

			Assert.Equal(Direction.ViewDir.West, p1.direction);
		}

		[Fact]
		public void TestTurnCommand_CyclicEnum_WestToNorth()
		{
			Character p1 = new Character(new Point(0, 0), Direction.ViewDir.West);
			TurnCommand test = new TurnCommand("right");

			test.Execute(p1);

			Assert.Equal(Direction.ViewDir.North, p1.direction);
		}
		#endregion

		#region MoveCommand Tests
		[Fact]
		public void TestMoveCommand_Upwards()
		{
			Character p1 = new Character(new Point(0, 0), Direction.ViewDir.North);
			MoveCommand move = new MoveCommand(10);

			move.Execute(p1);

			Assert.Equal(-10, p1.position.Y);

		}

		[Fact]
		public void TestMoveCommand_Downwards()
		{
			Character p1 = new Character(new Point(0, 0), Direction.ViewDir.South);
			MoveCommand move = new MoveCommand(10);

			move.Execute(p1);

			Assert.Equal(10, p1.position.Y);

		}

		[Fact]
		public void TestMoveCommand_Right()
		{
			Character p1 = new Character(new Point(0, 0), Direction.ViewDir.East);
			MoveCommand move = new MoveCommand(10);

			move.Execute(p1);

			Assert.Equal(10, p1.position.X);

		}

		[Fact]
		public void TestMoveCommand_Left()
		{
			Character p1 = new Character(new Point(0, 0), Direction.ViewDir.West);
			MoveCommand move = new MoveCommand(10);

			move.Execute(p1);

			Assert.Equal(-10, p1.position.X);
		}
		#endregion

		#region RepeatCommand Tests
		[Fact]
		public void TestRepeatCommand_SimpleTurn()
		{
			Character p1 = new Character(new Point(0, 0), Direction.ViewDir.North);
			List<ICommand> commands = [new MoveCommand(10), new TurnCommand("left")];

			RepeatCommand repeat = new RepeatCommand(commands, 2);
			repeat.Execute(p1);

			Assert.Equal(new Point(-10, -10), p1.position);

		}

		[Fact]
		public void TestRepeatCommand_NestedRepeat()
		{
			Character p1 = new Character(new Point(0, 0), Direction.ViewDir.North);
			List<ICommand> commands = [new MoveCommand(10), new TurnCommand("left"), new RepeatCommand([new MoveCommand(2)], 3)];

			RepeatCommand repeat = new RepeatCommand(commands, 2);
			repeat.Execute(p1);

			Assert.Equal(new Point(-16, -4), p1.position);
			Assert.Equal(Direction.ViewDir.South, p1.direction);
		}
		#endregion
		#endregion
	}
}