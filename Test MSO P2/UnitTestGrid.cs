using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MSO_P2;

namespace Test_MSO_P2
{
	public class UnitTestGrid
	{
		[Fact]
		public void Constructor_InvalidCharacter()
		{
			Character c = new Character(new Point(2, 8), Direction.ViewDir.East);
			int size = 4;
			List<Point> blockedCells = new List<Point>();
			Point? endPoint = null;

			Action a = () => new Grid(c, size, blockedCells, endPoint);

			Assert.Throws<ArgumentOutOfRangeException>(a);
		}
	}
}
