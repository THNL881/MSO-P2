using System.Drawing;

namespace MSO_P2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Character player = new Character(new Point(0, 0), Direction.ViewDir.North);
            Preset easyPreset = new Preset([new MoveCommand(10), new TurnCommand("right"),
                                            new MoveCommand(10), new TurnCommand("right"),
                                            new MoveCommand(10), new TurnCommand("right"),
                                            new MoveCommand(10), new TurnCommand("right")]);
            Preset advancedPreset = new Preset([new RepeatCommand([new MoveCommand(10), new TurnCommand("right")], 4)]);
            Preset expertPreset = new Preset([new MoveCommand(5), new TurnCommand("left"), new TurnCommand("left"),
                                            new MoveCommand(3), new TurnCommand("right"), new RepeatCommand([
                                                new MoveCommand(1), new TurnCommand("right"), new RepeatCommand([new MoveCommand(2)], 5)
                                                ], 3)]);
            Console.WriteLine("choose option: preset or load file");
            string? option = Console.ReadLine();

            if (option != null && (option == "preset" || option == "Preset"))
            {
                Console.WriteLine("choose preset: easy, advanced or expert");
                string? presetChoice = Console.ReadLine();

                switch (presetChoice)
                {
                    case "easy": foreach (ICommand command in easyPreset.Commands)
                        {
                            command.Execute(player);
                        }
                        break;
                    case "advanced": foreach (ICommand command in advancedPreset.Commands)
                        {
                            command.Execute(player);
                        }
                        break;
                    case "expert": foreach (ICommand command in easyPreset.Commands)
                        {
                            command.Execute(player);
                        }
                        break;
                    default: Console.WriteLine("invalid preset");
                        break;
                }
            }
            else
            {
                Console.WriteLine("enter the strategy you want to use and the filepath");
                Console.WriteLine("strategies: TXTStrategy");
                string[] valuables = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);


                FileParser parser = new FileParser(valuables[0], valuables[1]);
            }

            Console.WriteLine("End state " + player.position.ToString() + " facing " + player.direction);
        }

    }
}
