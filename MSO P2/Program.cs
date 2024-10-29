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
            Console.WriteLine("Choose option: preset, metrics or load file");
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
                        WriteCommands(commandsEasy);
                        break;
                    case "advanced":
                        List<ICommand> commandsAdvanced = advancedPreset.Commands;
						player.ExecuteCommands(commandsAdvanced);
						WriteCommands(commandsAdvanced);
						break;
                    case "expert":
                        List<ICommand> commandsExpert = expertPreset.Commands;
						player.ExecuteCommands(commandsExpert);
						WriteCommands(commandsExpert);
						break;
                    default: Console.WriteLine("invalid preset");
                        break;
                }
            }
            else if (option != null && (option == "load file" || option == "Load file" || option == "Load File"))
            {
                Console.WriteLine("Enter the strategy you want to use and the filepath");
                Console.WriteLine("Strategies: TXTStrategy");
                string[] valuables = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);


                FileParser parser = new FileParser(valuables[0], valuables[1]);
                List<ICommand> commands = parser.ReadFile();
                WriteCommands(commands);
            } else if (option != null && (option == "metrics" || option == "Metrics"))
            {
                Console.WriteLine("Choose option: preset or load file");
                string? metricsOption = Console.ReadLine();
                if (option != null && (metricsOption == "preset" || metricsOption == "Preset"))
                {
                    Console.WriteLine("Choose preset: easy, advanced, or expert");
                    string? metricsPreset = Console.ReadLine();
                    switch (metricsPreset)
                    {
                        case "easy":
                            Console.WriteLine($"Number of commands: {Metric.CalculateNumberOfCommands(easyPreset.Commands)}");
                            Console.WriteLine($"Maximum nesting level: {Metric.CalculateNestingLevel(easyPreset.Commands)}");
                            Console.WriteLine($"Number of repeat commands: {Metric.CalculateNumberOfRepeats(easyPreset.Commands)}");
                            break;
                        case "advanced":
							Console.WriteLine($"Number of commands: {Metric.CalculateNumberOfCommands(advancedPreset.Commands)}");
							Console.WriteLine($"Maximum nesting level: {Metric.CalculateNestingLevel(advancedPreset.Commands)}");
							Console.WriteLine($"Number of repeat commands: {Metric.CalculateNumberOfRepeats(advancedPreset.Commands)}");
							break;
                        case "expert":
							Console.WriteLine($"Number of commands: {Metric.CalculateNumberOfCommands(expertPreset.Commands)}");
							Console.WriteLine($"Maximum nesting level: {Metric.CalculateNestingLevel(expertPreset.Commands)}");
							Console.WriteLine($"Number of repeat commands: {Metric.CalculateNumberOfRepeats(expertPreset.Commands)}");
							break;
                        default:
                            Console.WriteLine("Invalid preset");
                            return;
                    }

                } else if (option != null && (metricsOption == "load file" || metricsOption == "Load file" || metricsOption == "Load File"))
                {
					Console.WriteLine("Enter the strategy you want to use and the filepath");
					Console.WriteLine("Strategies: TXTStrategy");
					string[] metricValuables = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

					FileParser metricParser = new FileParser(metricValuables[0], metricValuables[1]);
                    List<ICommand> metricCommands = metricParser.ReadFile();

                    Console.WriteLine($"Number of commands: {Metric.CalculateNumberOfCommands(metricCommands)}");
                    Console.WriteLine($"Maximum nesting level: {Metric.CalculateNestingLevel(metricCommands)}");
                    Console.WriteLine($"Number of repeat commands: {Metric.CalculateNumberOfRepeats(metricCommands)}");
				}
                else
                {
                    Console.WriteLine("Invalid option, please restart the application");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Invalid option, please restart the application.");
                return;
            }

            Console.WriteLine("End state " + player.position.ToString() + " facing " + player.direction);
        }

        //TO BE REWRITTEN
        public static void WriteCommands(List<ICommand> commands, int nestingLevel = 0)
        {
            string[] printArray = [];
            foreach (ICommand command in commands)
            {
                if (command is MoveCommand)
                {
                    printArray.Append($"Move {((MoveCommand)command).Steps}");
                    //Console.WriteLine(String.Join(',', $"Move {((MoveCommand)command).Steps}"));
                    //if (command == commands[^1] && nestingLevel == 0)
                    //{
                    //    Console.WriteLine($"Move {((MoveCommand)command).Steps}.");
                    //}
                    //else
                    //{
                    //    Console.Write($"Move {((MoveCommand)command).Steps}, ");
                    //}
                } else if (command is TurnCommand)
				{
					printArray.Append($"Turn {((TurnCommand)command).TurningDirection}");
					//Console.WriteLine(String.Join(',', $"Turn {((TurnCommand)command).TurningDirection}"));
					//if (command == commands[^1] && nestingLevel == 0)
					//{
					//	Console.WriteLine($"Turn {((TurnCommand)command).TurningDirection}.");
					//}
					//else
					//{
					//	Console.Write($"Turn {((TurnCommand)command).TurningDirection}, ");
					//}
				} else if (command is RepeatCommand)
                {
                    RepeatCommand repeatCommand = (RepeatCommand)command;
                    bool isInsideRepeat = true;
                    for(int i = 0; i < repeatCommand.RepeatAmount; i++)
                    {
                        if(i == repeatCommand.RepeatAmount - 1)
                        {
                            isInsideRepeat = false;
                        }
                        WriteCommands(repeatCommand.Commands, nestingLevel + 1);
                    }
                }
			}
            Console.WriteLine(String.Join(", ", printArray));
        }

    }
}
