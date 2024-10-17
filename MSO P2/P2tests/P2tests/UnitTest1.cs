using MSO_P2;

namespace P2tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestTurnCommand()
        {
            //Arrange

            Character p1 = new Character(new System.Drawing.Point(0, 0), Direction.ViewDir.North);
            TurnCommand turn = new TurnCommand("right");

            //act
            turn.Execute(p1);

            //assert
            Assert.Equal(Direction.ViewDir.West, p1.direction);

        }
    }
}