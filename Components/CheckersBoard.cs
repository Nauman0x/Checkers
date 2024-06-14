using UnityEngine;

namespace Checkers.Runtime
{
	public sealed class CheckersBoard : MonoBehaviour
	{
		[SerializeField] private Square[] _board = default;

		public Square[,] Board => GetBoard(_board, Statics.ROWS, Statics.COLS);

		private static Square[,] GetBoard(Square[] input, int rows, int cols)
		{
			var output = new Square[rows, cols];

			for (var i = 0; i < rows; i++)
			{
				for (var j = 0; j < cols; j++)
				{
					output[i, j] = input[i*cols + j];
				}
			}

			return output;
		}
	}
}
