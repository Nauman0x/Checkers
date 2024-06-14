using System.Linq;
using UnityEngine;

namespace Checkers.Runtime
{
	/// <summary>
	/// Holds Core Logic of the game
	/// </summary>
	public sealed class GamePlayer : MonoBehaviour
	{
		[SerializeField] private CheckersBoard _board = default;
		[SerializeField] private Piece _whitePiece = default;
		[SerializeField] private Piece _blackPiece = default;
		[SerializeField] private GameObject _overScreen = default;

		private readonly MovesProcessor _movesProcessor = new();

		private void Start()
		{
			InitBoard();
		}

		private void OnEnable()
		{
			_movesProcessor.Init(_board.Board);
			Square.OnSelect += OnSquareSelect;
		}

		private void OnDisable()
		{
			_movesProcessor.Cleanup();
			Square.OnSelect -= OnSquareSelect;
		}

		private void InitBoard()
		{
			var board = _board.Board;

			for (var i = 0; i < 3; i++) InitPieces(board, PieceType.White, i);

			var rows = board.GetLength(0);
			for (var i = rows - 1; i > rows - 4; i--) InitPieces(board, PieceType.Black, i);
		}

		private void InitPieces(Square[,] board, PieceType type, int i)
		{
			for (var j = 0; j < board.GetLength(1); j++)
			{
				var square = board[i, j];

				if (square.Type == SquareType.Black)
				{
					var piece = type == PieceType.Black ? _blackPiece : _whitePiece;
					var pieceObj = Instantiate(piece, square.transform);
					square.AddPiece(pieceObj);
				}
			}
		}

		private void OnSquareSelect(Square _) => _overScreen.SetActive(CheckGameOver());

		private bool CheckGameOver()
		{
			var components = this.GetComponentsInChildren<Piece>();
			if (components.Any(x => x.Type == PieceType.Black) == false) return true;
			if (components.Any(x => x.Type == PieceType.White) == false) return true;
			return false;
		}
	}
}
