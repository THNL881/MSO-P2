using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2
{
    public abstract class ParseStrategy
    {
        public abstract List<ICommand> ReadFile();
    }

    public class TXTStrategy : ParseStrategy
    {
        StreamReader? reader;
        String? fileName;
        public TXTStrategy(String filePath)
        {
            if (filePath != null)
            {
                reader = new StreamReader(filePath);   
            } else
            {
                reader = null;
            }
                
        }

        public override List<ICommand> ReadFile()
        {
            List<ICommand> commands = new List<ICommand>();

            Console.WriteLine("top of readfile");
            if (reader == null)
            {
                Console.WriteLine("reader is null");
                return commands;
            }

            String line = reader.ReadLine();

            while (line != null)
            {
                Console.WriteLine("inside while loop, line: " + line);

                String[] stringArray = line.Split(" ");

                String command = stringArray[0];
                String addOn = stringArray[1];

                switch (command)
                {
                    case "Move": commands.Add(new MoveCommand(Convert.ToInt32(addOn)));
                        break;
                    case "Turn": commands.Add(new TurnCommand(addOn));
                        break;
                    case "Repeat": commands.Add(new RepeatCommand(GetRepeatCommands(reader), Convert.ToInt32(addOn)));
                        break;
                    default:  
                        break;
                };
                
                line = reader.ReadLine();
            }

            Console.WriteLine("now at end of readfile, count command: " + commands.Count);
            foreach (ICommand command in commands)
            {
                Console.WriteLine("yeet");
            }
            return commands;
        }

        public List<ICommand> GetRepeatCommands(StreamReader reader)
        {
            Console.WriteLine("inside RepeatLoop");
            List<ICommand> commands = new List<ICommand>();
            String line = reader.ReadLine();

            Console.WriteLine("line: " + line);

            String[] stringArray = line.Split(" ");

            while (line != null)
            {
                string[] commandString = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string command = commandString[0];
                string addOn = commandString[1];

                switch (command)
                {
                    case "Move":
                        commands.Add(new MoveCommand(Convert.ToInt32(addOn)));
                        break;
                    case "Turn":
                        commands.Add(new TurnCommand(addOn));
                        break;
                    case "Repeat":
                        commands.Add(new RepeatCommand(GetRepeatCommands(reader), Convert.ToInt32(addOn)));
                        break;
                    default: break;
                };

                long currentReaderPosition = reader.BaseStream.Position;
                if (reader.Peek() == -1)
                {
                    Console.WriteLine("nextline is empty, now at command: " +  line);
                    break;
                } else
                {
                    Console.WriteLine("next line not empty, peek gives: " + reader.Peek());
                }
                //check of volgende line indentation heeft, zo ja dan break en list returnen
                if (reader.ReadLine().Split(" ")[0] != " ")
                {
                    reader.BaseStream.Seek(currentReaderPosition, SeekOrigin.Begin);
                    reader.DiscardBufferedData();
                    break;
                }
                line = reader.ReadLine();

            }


            Console.WriteLine("exiting repeat loop, count: " + commands.Count);
            return commands;
        }
    }
}
