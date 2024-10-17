using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MSO_P2
{
    public static class Metric 
    {
        private static int CalculateNumberOfCommands(List<ICommand> commands){
            return commands.Count();
        }

        private int CalculateNestingLevel(List<ICommand> commands){
            foreach (ICommand command in commands){
                //if (command == RepeatCommand){

                //}
            }
        }
    }
}