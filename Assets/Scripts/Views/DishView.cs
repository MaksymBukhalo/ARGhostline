using UnityEngine;
using UnityEngine.UI;

namespace FoodStoryTAS
{
	public class DishView : MenuViewItem, ISelectable
	{
		/// <summary>
		/// Text component of category name.
		/// </summary>
		[SerializeField] private Image _dishBackground;

		/// <summary>
		/// Text component of category name.
		/// </summary>
		[SerializeField] private Text _dishName;

		/// <summary>
		/// Text component of dish price.
		/// </summary>
		[SerializeField] private Text _dishPrice;

		/// <summary>
		/// Fill dish menu element.
		/// </summary>
		/// <param name="data">Data related to specific dish.</param>
		public override void FillInfo(object data)
		{
			Dish info = data as Dish;

			if (info != null)
			{
				_dishName.text = info.Name;

				//We will convert Currency enym value to string for convinience and fast changing dish currency.
				_dishPrice.text = Extensions.ConvertCurrency(info.Currency) + info.Price;
			}
			else
			{
				Debug.LogError($"Can not convert to Dish type or data equals NULL!");
			}
		}

		/// <summary>
		/// Select action of interface
		/// </summary>
		public void Select()
		{
			_dishBackground.color = Constants.VioletColor;
			_dishName.color = Color.white;
			_dishPrice.color = Color.white;
		}

		/// <summary>
		/// Deselect action of interface
		/// </summary>
		public void Deselect()
		{
			_dishBackground.color = Color.white;
			_dishName.color = Color.black;
			_dishPrice.color = Color.black;
		}
	}
}