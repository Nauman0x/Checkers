namespace Checkers.Runtime
{
	public static class Statics
	{
		public const int ROWS = 8;
		public const int COLS = 8;

		public static PlayerType CurrentTurn = PlayerType.Player;

		public static void SwitchTurn()
		{
			CurrentTurn = CurrentTurn == PlayerType.Player ? PlayerType.Opponent : PlayerType.Player;
		}

		public static PieceType GetExpectedPieceType()
			=> CurrentTurn == PlayerType.Player ? PieceType.Black : PieceType.White;

		public static MoveType GetMoveDirection() 
			=> CurrentTurn == PlayerType.Player ? MoveType.Forward : MoveType.Backward;
	}
}
