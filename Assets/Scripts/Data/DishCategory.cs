using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FoodStoryTAS
{
	[System.Serializable]
	public class DishCategory
	{
		/// <summary>
		/// Name related to specific category.
		/// </summary>
		public string Name;
		
		/// <summary>
		/// Id of specific dish category.
		/// </summary>
		public int Id;

		/// <summary>
		/// All 
		/// </summary>
		public List<Dish> Dishes = new List<Dish>();

		public Dish GetDishById(int dishId)
		{
			if (Dishes == null)
			{
				Debug.LogError("Dish collection equals NULL!");
				return null;
			}

			return Dishes.First(dish => dish.Id == dishId);
		}

		public bool IsContainDishWithId(int dishId)
		{
			if (Dishes == null)
			{
				Debug.LogError("Dish collection equals NULL!");
				return false;
			}

			Dish dish = Dishes.Find(d => d.Id == dishId);

			return dish != null;
		}
	}
}