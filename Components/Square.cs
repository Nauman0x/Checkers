using System;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Checkers.Runtime
{
	public sealed class Square : MonoBehaviour, IPointerClickHandler
	{
		public static event Action<Square> OnValidSelection = default;

		public static event Action<Square> OnSelect = default;

		[SerializeField] private SquareType _squareType = default;
		[SerializeField] private Image _selectImage = default;
		[SerializeField] private Coordinate _coordinate = default;
		[SerializeField] private Piece _piece = default;

		public bool IsHighlighted { get; private set; } = false;

		public SquareType Type => _squareType;

		public bool IsEmpty => _piece == default;

		public bool HasPiece => _piece != default;

		public Piece Piece => _piece;

		public Coordinate Coordinate => _coordinate;

		public void AddPiece(Piece piece) => _piece = piece;

		public Piece RemovePiece(Piece piece)
		{
			_piece = default;
			return piece;
		}

		public void SetHighlightState(bool state)
		{
			IsHighlighted = state;
			_selectImage.gameObject.SetActive(state);
		}

		public void OnPointerClick(PointerEventData _)
		{
			OnSelect?.Invoke(this);

			if (HasPiece)
			{
				var expectedType = Statics.GetExpectedPieceType();

				if (Piece.Type == expectedType) OnValidSelection?.Invoke(this);
			}
		}
	}
}
