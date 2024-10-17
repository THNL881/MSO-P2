using System.Drawing;

namespace MSO_P2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Character player = new Character(new Point(0, 0), Direction.ViewDir.East);
            Preset easyPreset = new Preset([new MoveCommand(10), new TurnCommand("right"),
                                            new MoveCommand(10), new TurnCommand("right"),
                                            new MoveCommand(10), new TurnCommand("right"),
                                            new MoveCommand(10), new TurnCommand("right")]);
            Preset advancedPreset = new Preset([new RepeatCommand([new MoveCommand(10), new TurnCommand("right")], 4)]);
            Preset expertPreset = new Preset([new MoveCommand(5), new TurnCommand("left"), new TurnCommand("left"),
                                            new MoveCommand(3), new TurnCommand("right"), new RepeatCommand([
                                                new MoveCommand(1), new TurnCommand("right"), new RepeatCommand([new MoveCommand(2)], 5)
                                                ], 3)]);

        }
    }
}
