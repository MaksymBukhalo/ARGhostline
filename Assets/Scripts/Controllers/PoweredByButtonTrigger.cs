using UnityEngine;
using UnityEngine.UI;

namespace FoodStoryTAS
{
	[RequireComponent(typeof(Button))]
	public class PoweredByButtonTrigger : MonoBehaviour
	{
		[SerializeField] private Button _poweredByBtn;

		[SerializeField] private string _urlLink = "https://foodstory.io";

		private void OnEnable()
		{
			SubscribeEvents();
		}

		/// <summary>
		/// Subscribing to all events.
		/// </summary>
		private void SubscribeEvents()
		{
			_poweredByBtn.onClick.AddListener(OnBtnClick);
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
			_poweredByBtn.onClick.RemoveListener(OnBtnClick);
		}

		/// <summary>
		/// Called when user click on button.
		/// </summary>
		public void OnBtnClick()
		{
			Application.OpenURL(_urlLink);
		}
	}
}