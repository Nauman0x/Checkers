using UnityEngine;

namespace Checkers.Runtime
{
	public class Piece : MonoBehaviour
	{
		[SerializeField] private PieceType _pieceType = default;

		public PieceType Type => _pieceType;
	}
}
