using System.Drawing;

namespace MSO_P2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Character player = new Character(new Point(0, 0), Direction.ViewDir.East);
        }


        //public void TestTurnCommand()
        //{
        //    //Arrange

        //    Character p1 = new Character(new System.Drawing.Point(0, 0), Direction.ViewDir.North);
        //    TurnCommand turn = new TurnCommand("right");

        //    //act
        //    turn.Execute(p1);

        //    //assert
        //    Assert.Equal(p1.direction, Direction.ViewDir.East);

        //}
    }
}
