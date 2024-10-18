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
            Console.WriteLine("Choose option: preset or load file");
            string? option = Console.ReadLine();

            if (option != null && (option == "preset" || option == "Preset"))
            {
                Console.WriteLine("Choose preset: easy, advanced or expert");
                string? presetChoice = Console.ReadLine();

                switch (presetChoice)
                {
                    case "easy":
                        List<ICommand> commandsEasy = easyPreset.Commands;
                        player.ExecuteCommands(commandsEasy);
                        WriteCommands(commandsEasy, commandsEasy.Count());
                        break;
                    case "advanced":
                        List<ICommand> commandsAdvanced = advancedPreset.Commands;
						player.ExecuteCommands(commandsAdvanced);
						WriteCommands(commandsAdvanced, commandsAdvanced.Count());
						break;
                    case "expert":
                        List<ICommand> commandsExpert = expertPreset.Commands;
						player.ExecuteCommands(commandsExpert);
						WriteCommands(commandsExpert, commandsExpert.Count());
						break;
                    default: Console.WriteLine("invalid preset");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Enter the strategy you want to use and the filepath");
                Console.WriteLine("Strategies: TXTStrategy");
                string[] valuables = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);


                FileParser parser = new FileParser(valuables[0], valuables[1]);
                List<ICommand> commands = parser.ReadFile();
                WriteCommands(commands, commands.Count());
            }

            Console.WriteLine("End state " + player.position.ToString() + " facing " + player.direction);
        }

        public static void WriteCommands(List<ICommand> commands, int lastElement, int repeatIterator = 0)
        {
            int elementRepeat = repeatIterator;
            for (int i = 0; i < commands.Count(); i++)
            {
                if (commands[i] is MoveCommand)
                {
                    if(i < lastElement && elementRepeat < lastElement + 1)
                    {
                        Console.Write($"Move {((MoveCommand)commands[i]).Steps}, ");
                        elementRepeat++;
                    }
                    else
                    {
						Console.WriteLine($"Move {((MoveCommand)commands[i]).Steps}.");
					}
                }
                else if (commands[i] is TurnCommand)
                {
                    if (i < lastElement && elementRepeat < lastElement + 1)
                    {
                        Console.Write($"Turn {((TurnCommand)commands[i]).TurningDirection}, ");
                        elementRepeat++;
                    } else
                    {
						Console.WriteLine($"Turn {((TurnCommand)commands[i]).TurningDirection}.");
					}
                }
                else if (commands[i] is RepeatCommand)
                {
                    int repeatAmount = ((RepeatCommand)commands[i]).RepeatAmount;
                    int repeatCommandsCount = ((RepeatCommand)commands[i]).Commands.Count();

					for (int j = 0; j < repeatAmount;  j++)
                    {
                        WriteCommands(((RepeatCommand)commands[i]).Commands, repeatAmount * repeatCommandsCount, repeatCommandsCount * (j + 1));
                    }
                }
			}
        }

    }
}
