using System.Linq;
using UnityEngine;

namespace FoodStoryTAS
{
	public class MenuDataHolder : Singleton<MenuDataHolder>
	{
		[Header("Menu data:")]
		[SerializeField] private MenuScriptable _menuAsset;

		/// <summary>
		/// Find dish in menu by id.
		/// </summary>
		/// <param name="id">Dish id.</param>
		public Dish FindDishById(int id)
		{
			//return _menuAsset.CategoriesAsset.First(x => x.Category.IsContainDishWithId(id)).Category.GetDishById(id);
			return _menuAsset.DishesAsset.First(x => x.Dish.Id == id).Dish;
		}

		/// <summary>
		/// Get collection of categories.
		/// </summary>
		public Menu GetMenuData()
		{
			//return new Menu() {Categories = _menuAsset.Categories};
			return new Menu() { Dishes = _menuAsset.Dishes};
		}
	}
}