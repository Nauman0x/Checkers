using UnityEngine;
using UnityEngine.EventSystems;

namespace Checkers.Runtime
{
	public sealed class GameInputProcessor : MonoBehaviour, IPointerClickHandler
	{
		public void OnPointerClick(PointerEventData eventData) => this.gameObject.SetActive(false);
	}
}
