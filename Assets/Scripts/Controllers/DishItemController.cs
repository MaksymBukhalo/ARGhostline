using UnityEngine;
using UnityEngine.UI;

namespace FoodStoryTAS
{
	public class DishItemController : MonoBehaviour
	{
		/// <summary>
		/// Button component of dish item.
		/// </summary>
		//[SerializeField] private Button _dishButton;

		/// <summary>
		/// Id of dish.
		/// </summary>
		private int _dishId;

		private void OnEnable()
		{
			SubscribeEvents();
		}

		/// <summary>
		/// Subscribing to all events.
		/// </summary>
		private void SubscribeEvents()
		{
			//_dishButton.onClick.AddListener(OnClick);
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
			//_dishButton.onClick.RemoveListener(OnClick);
		}

		/// <summary>
		/// Call when user click on dish element.
		/// </summary>
		private void OnClick()
		{
			MenuSelectionController.Instance.SelectMenuItem(this);
		}

		/// <summary>
		/// Set id for identifying dish.
		/// </summary>
		/// <param name="id">Dish id.</param>
		public void SetDishId(int id)
		{
			_dishId = id;
		}

		/// <summary>
		/// Get id of dish.
		/// </summary>
		public int GetDishId()
		{
			return _dishId;
		}
	}
}
