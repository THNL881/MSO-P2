using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2
{
    public class Character
    {
        public Direction.ViewDir direction { get; set; }
        public Point position { get; set; }

        private void ExecuteOrder(List<ICommand> commands)
        {
            foreach (ICommand command in commands)
            {
                command.Execute();
            }
        }

    }

    public struct Direction
    {
        public enum ViewDir
        {
            Left, Right, Top, Bottom
        }
    }
}
