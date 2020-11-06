using System.Collections.Generic;
using UnityEngine;

namespace FoodStoryTAS
{
	[CreateAssetMenu(fileName = "DishCategory", menuName = "Category/DishCategory")]
	public class DishCategoryScriptable : ScriptableObject
	{
		public string Name;
		public int Id;
		public List<DishScriptable> DishesAssets;

		[HideInInspector]
		public DishCategory Category;

		private void OnValidate()
		{
			Category.Name = Name;
			Category.Id = Id;
			Category.Dishes = new List<Dish>();

			foreach (var dishAsset in DishesAssets)
			{
				Dish dish = dishAsset.Dish;
				Category.Dishes.Add(dish);
			}
		}
	}
}