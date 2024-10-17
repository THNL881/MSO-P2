using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace MSO_P2
{
    public static class Metric 
    {
        public static int CalculateNumberOfCommands(List<ICommand> commands){
            return commands.Count();
        }

        public static int CalculateNumberOfRepeats(List<ICommand> commands){
            int returnValue = 0;
            foreach(ICommand command in commands){
                if(command is RepeatCommand){
                    returnValue ++;
                    returnValue += CalculateNumberOfRepeats(((RepeatCommand)command).Commands);
                }
            }
            return returnValue;
        }

        public static int CalculateNestingLevel(List<ICommand> commands, int i = 0){
            HashSet<int> levels = new HashSet<int>();
            foreach (ICommand command in commands){
                if (command is RepeatCommand){
                    CalculateNestingLevel(((RepeatCommand)command).Commands, ++i);
                }
                levels.Add(i);
            }
            return levels.Max();
        }
    }
}