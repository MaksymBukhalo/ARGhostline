using UnityEngine;
using UnityEngine.UI;

namespace FoodStoryTAS
{
	public class CategoryView : MenuViewItem
	{
		/// <summary>
		/// Text component of category name.
		/// </summary>
		[SerializeField] private Text _categoryName;

		/// <summary>
		/// Fill category menu element.
		/// </summary>
		/// <param name="data">Data related to specific category.</param>
		public override void FillInfo(object data)
		{
			DishCategory category = data as DishCategory;

			if (category != null)
			{
				_categoryName.text = category.Name.ToUpper();
			}
			else
			{
				Debug.LogError($"Can not convert to DishCategory type or data equals NULL!");
			}
		}
	}
}