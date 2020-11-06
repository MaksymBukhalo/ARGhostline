using UnityEngine;
using UnityEngine.UI;

namespace FoodStoryTAS
{
	[RequireComponent(typeof(Button))]
	public class CloseButtonTrigger : MonoBehaviour
	{
		[SerializeField] private Button _closeBtn;

		private void OnEnable()
		{
			SubscribeEvents();
		}

		/// <summary>
		/// Subscribing to all events.
		/// </summary>
		private void SubscribeEvents()
		{
			_closeBtn.onClick.AddListener(OnBtnClick);
		}

		private void OnDisable()
		{
			UnsubscribeEvents();
		}

		/// <summary>
		/// Unsubscribing from all events.
		/// </summary>
		private void UnsubscribeEvents()
		{
			_closeBtn.onClick.RemoveListener(OnBtnClick);
		}

		/// <summary>
		/// Called when user click on button.
		/// </summary>
		public void OnBtnClick()
		{
			ScenesManager.LoadSplashScene();
		}
	}
}