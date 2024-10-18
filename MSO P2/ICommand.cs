using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel.Design;

namespace MSO_P2;

public interface ICommand
{
    public void Execute(Character c);
}

public class TurnCommand : ICommand
{
    private string _turningDirection {get; set;}

    public string TurningDirection
    {
        get { return _turningDirection; }
    }
    
    public TurnCommand(string turningDirection)
    {
        this._turningDirection = turningDirection;
    }

    public void Execute(Character c){
        switch(_turningDirection){
            case "right":
            c.direction = (Direction.ViewDir)((int)(c.direction + 1) % 4);
            break;
            case "left":
            c.direction = (Direction.ViewDir)((int)(c.direction + 3) % 4);
            break;
            default:
            throw new ArgumentException("Invalid turning direction");
        }
    }
}

public class MoveCommand : ICommand{
    private int _steps {get; set;}

    public int Steps
    {
        get { return _steps; }
    }

    public MoveCommand(int steps){
        _steps = steps;
    }

    public void Execute(Character c){
        switch (c.direction){
            case Direction.ViewDir.North:
            c.position = new Point(c.position.X, c.position.Y - _steps);
            break;
            case Direction.ViewDir.East:
            c.position = new Point(c.position.X + _steps, c.position.Y);
            break;
            case Direction.ViewDir.South:
            c.position = new Point(c.position.X, c.position.Y + _steps);
            break;
            case Direction.ViewDir.West:
            c.position = new Point(c.position.X - _steps, c.position.Y);
            break;
        }
    }
}

public class RepeatCommand : ICommand{
    private List<ICommand> _commands {get; set;}
    private int _repeatAmount {get; set;}

    public List<ICommand> Commands{
        get{ return _commands;}
    }
    public int RepeatAmount {
        get {return _repeatAmount;}
    }

    public RepeatCommand(List<ICommand> commands, int repeatAmount){
        _commands = commands;
        _repeatAmount = repeatAmount;
    }

    public void Execute(Character c){
        for (int i = 0; i < _repeatAmount; i++){
            foreach (ICommand command in _commands){
                command.Execute(c);
            }
        }
    }
}

public struct Preset {
    private List<ICommand> _commands;

    public List<ICommand> Commands
    {
        get { return _commands; }
    }

    public Preset(List<ICommand> commands){
        _commands = commands;
    }
}

