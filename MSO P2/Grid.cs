using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MSO_P2
{
	internal class Grid
	{
		private Character character_;
		private int size_;
		private List<Point> blockedCells_;
		private Point endPoint_;

		public Character Character
		{
			get { return character_; }
		}
		public int Size
		{
			get { return size_; }
		}
		public List<Point> BlockedCells
		{
			get { return blockedCells_; }
		}
		public Point EndPoint
		{
			get { return endPoint_; }
		}

		public Grid(Character character, int size, List<Point> blockedCells, Point endPoint)
		{
			this.character_ = character;
			this.size_ = size;
			this.blockedCells_ = blockedCells;
			this.endPoint_ = endPoint;
		}

		public void setGrid(string input)
		{
			string[] gridString = input.Split("\n");
			size_ = gridString.Length;
			for (int i = 0; i < size_; i++)
			{
				for (int j = 0; j < size_; j++)
				{
					switch (gridString[i][j])
					{
						case '+':
							blockedCells_.Add(new Point(j, i));
							break;
						case 'x':
							endPoint_ = new Point(j, i);
							break;
					}
				}
			}
			character_ = new Character(new Point(0, 0), Direction.ViewDir.East);
		}

		public void loadGrid(string filePath)
		{
			StreamReader reader = new StreamReader(filePath);
			string gridString = reader.ReadToEnd();
			setGrid(gridString);
		}
	}
}
