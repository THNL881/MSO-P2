using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MSO_P2
{
	public class Grid
	{
		private Character _character;
		private int _size;
		private List<Point> _blockedCells;
		private Point? _endPoint;

		public Character Character
		{
			get { return _character; }
			set 
			{ 
				if (value.position.X >= _size || value.position.Y >= _size)
				{
					throw new ArgumentOutOfRangeException("Character is outside of the grid");
				}
				_character = value;
			}
		}
		public int Size
		{
			get { return _size; }
		}
		public List<Point> BlockedCells
		{
			get { return _blockedCells; }
		}
		public Point? EndPoint
		{
			get { return _endPoint; }
		}

		public Grid(Character character, int size, List<Point> blockedCells, Point? endPoint = null)
		{
			Character = character;
			this._size = size;
			this._blockedCells = blockedCells;
			this._endPoint = endPoint;
		}

		public void setGrid(string input)
		{
			string[] gridString = input.Split("\n");
			_size = gridString.Length;
			for (int i = 0; i < _size; i++)
			{
				for (int j = 0; j < _size; j++)
				{
					switch (gridString[i][j])
					{
						case '+':
							_blockedCells.Add(new Point(j, i));
							break;
						case 'x':
							_endPoint = new Point(j, i);
							break;
					}
				}
			}
			Character = new Character(new Point(0, 0), Direction.ViewDir.East);
		}

		public void loadGrid(string filePath)
		{
			StreamReader reader = new StreamReader(filePath);
			string gridString = reader.ReadToEnd();
			setGrid(gridString);
		}
	}
}
