using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2
{
    public interface ICommand
    {
        public void Execute(Character c);
    }

    public class TurnCommand : ICommand
    {
        private string _turningDirection {get; set;}
        
        public TurnCommand(string turningDirection)
        {
            this._turningDirection = turningDirection;
        }

        public void Execute(Character c){
            //switch c.ViewDirection
        }
    }
}
