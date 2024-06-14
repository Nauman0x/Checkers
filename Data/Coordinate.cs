using System;
using UnityEngine;

namespace Checkers.Runtime
{
	[Serializable]
	public struct Coordinate
	{
		[SerializeField] private int _xCoordinate;
		[SerializeField] private int _yCoordinate;

		public int XCoordinate => _xCoordinate;

		public int YCoordinate => _yCoordinate;

		public Coordinate(int x, int y)
		{
			_xCoordinate = x;
			_yCoordinate = y;
		}

		public void Set(int x, int y)
		{
			_xCoordinate = x;
			_yCoordinate = y;
		}

		public bool Equals(Coordinate coordinate)
		{
			return coordinate.XCoordinate == XCoordinate && coordinate.YCoordinate == YCoordinate;
		}
	}
}
