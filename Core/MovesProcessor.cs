using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Checkers.Runtime
{
	public sealed class MovesProcessor
	{
		// contains all valid moves according to current context
		private readonly List<Coordinate> _validMoves = new();

		private readonly Dictionary<Coordinate, (int, int)> _killCoordinates = new();

		private Square[,] _board = default;
		private Square _sourceSquare = default;

		public void Init(Square[,] board)
		{
			_board = board;
			Square.OnValidSelection += HighlightPossibleMoves;
			Square.OnSelect += OnSelection;
		}

		public void Cleanup()
		{
			Square.OnValidSelection -= HighlightPossibleMoves;
			Square.OnSelect -= OnSelection;
		}

		private void OnSelection(Square square)
		{
			var coordinate = square.Coordinate;

			// valid move is going to be made
			if (_validMoves.Any(x => x.Equals(coordinate)))
			{
				var piece = _sourceSquare.RemovePiece(_sourceSquare.Piece);
				var destinationSquare = _board[coordinate.XCoordinate, coordinate.YCoordinate];
				var pieceTransform = piece.transform;
				pieceTransform.SetParent(destinationSquare.transform);
				pieceTransform.localPosition = Vector3.zero;
				destinationSquare.AddPiece(piece);

				_killCoordinates.TryGetValue(coordinate, out var val);

				if (val != default)
				{
					var kSquare = _board[val.Item1, val.Item2];
					var killedPiece = kSquare.RemovePiece(kSquare.Piece).transform;
					Object.Destroy(killedPiece.gameObject);
					
					HighlightPossibleMoves(destinationSquare);
					if (_killCoordinates.Count != 0) return;
				}

				ClearHighlights();
				Statics.SwitchTurn();
			}
		}

		private void HighlightPossibleMoves(Square square)
		{
			ClearHighlights();
			_sourceSquare = square;

			var direction = Statics.GetMoveDirection();

			switch (direction)
			{
				case MoveType.Forward:
					HighlightForwardMoves(square.Coordinate);
					break;
				case MoveType.Backward:
					HighlightBackwardMoves(square.Coordinate);
					break;
			}
		}

		private void HighlightForwardMoves(Coordinate coordinate)
		{
			var x = coordinate.XCoordinate;
			var y = coordinate.YCoordinate;

			if (IsValid(x - 1, y - 1))
			{
				var square = _board[x - 1, y - 1];
				if (square.HasPiece)
				{
					if (square.Piece.Type == PieceType.White)
					{
						if (IsValid(x - 2, y - 2))
						{
							square = _board[x - 2, y - 2];
							if (square.IsEmpty)
							{
								_killCoordinates.Add(new Coordinate(x - 2, y - 2), (x - 1, y - 1));
								_validMoves.Add(new Coordinate(x - 2, y - 2));
							}
						}
					}
				}
				else
				{
					_validMoves.Add(new Coordinate(x - 1, y - 1));
				}
			}

			if (IsValid(x - 1, y + 1))
			{
				var square = _board[x - 1, y + 1];
				if (square.HasPiece)
				{
					if (square.Piece.Type == PieceType.White)
					{
						if (IsValid(x - 2, y + 2))
						{
							square = _board[x - 2, y + 2];
							if (square.IsEmpty)
							{
								_killCoordinates.Add(new Coordinate(x - 2, y + 2), (x - 1, y + 1));
								_validMoves.Add(new Coordinate(x - 2, y + 2));
							}
						}
					}
				}
				else
				{
					_validMoves.Add(new Coordinate(x - 1, y + 1));
				}
			}

			if (_validMoves.Count > 0) _board[x, y].SetHighlightState(true);

			HighlightMoves();
		}

		private void HighlightBackwardMoves(Coordinate coordinate)
		{
			var x = coordinate.XCoordinate;
			var y = coordinate.YCoordinate;

			if (IsValid(x + 1, y - 1))
			{
				var square = _board[x + 1, y - 1];
				if (square.HasPiece)
				{
					if (square.Piece.Type == PieceType.Black)
					{
						if (IsValid(x + 2, y - 2))
						{
							square = _board[x + 2, y - 2];
							if (square.IsEmpty)
							{
								_killCoordinates.Add(new Coordinate(x + 2, y - 2), (x + 1, y - 1));
								_validMoves.Add(new Coordinate(x + 2, y - 2));
							}
						}
					}
				}
				else _validMoves.Add(new Coordinate(x + 1, y - 1));
			}

			if (IsValid(x + 1, y + 1))
			{
				var square = _board[x + 1, y + 1];
				if (square.HasPiece)
				{
					if (square.Piece.Type == PieceType.Black)
					{
						if (IsValid(x + 2, y + 2))
						{
							square = _board[x + 2, y + 2];
							if (square.IsEmpty)
							{
								_killCoordinates.Add(new Coordinate(x + 2, y + 2), (x + 1, y + 1));

								_validMoves.Add(new Coordinate(x + 2, y + 2));
							}
						}
					}
				}
				else
				{
					_validMoves.Add(new Coordinate(x + 1, y + 1));
				}
			}

			if (_validMoves.Count > 0) _board[x, y].SetHighlightState(true);

			HighlightMoves();
		}

		private void HighlightMoves()
		{
			foreach (var coordinate in _validMoves)
			{
				_board[coordinate.XCoordinate, coordinate.YCoordinate].SetHighlightState(true);
			}
		}

		private bool IsValid(int x, int y)
		{
			return x >= 0 && x < Statics.ROWS && y >= 0 && y < Statics.COLS;
		}

		private void ClearHighlights()
		{
			_killCoordinates.Clear();
			_validMoves.Clear();
			_sourceSquare = default;

			for (var i = 0; i < _board.GetLength(0); i++)
			{
				for (var j = 0; j < _board.GetLength(1); j++)
				{
					_board[i, j].SetHighlightState(false);
				}
			}
		}
	}
}
