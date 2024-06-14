using UnityEngine;

namespace Checkers.Runtime
{
	public class GridNamesSetter : MonoBehaviour
	{
		[ContextMenu("UpdateNames")]
		public void UpdateNames()
		{
			var objects = this.transform.GetComponentsInChildren<Square>();

			int runningIndex = 0;
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					var obj = objects[runningIndex++];
					obj.transform.name = $"Square({i},{j})";
				}
			}
		}
	}
}
