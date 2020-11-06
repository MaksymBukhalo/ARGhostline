using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FoodStoryTAS
{
	[CreateAssetMenu(fileName = "Menu", menuName = "Menu")]
	public class MenuScriptable : ScriptableObject
	{
        //public List<DishCategoryScriptable> CategoriesAsset;

        //[HideInInspector]
        //public List<DishCategory> Categories;

        public List<DishScriptable> DishesAsset;

        [HideInInspector] public List<Dish> Dishes;

        private void OnValidate()
		{
            //Categories = new List<DishCategory>();
            Dishes = new List<Dish>();

            //Sorting by dish id.
            DishesAsset = DishesAsset.OrderBy(asset => asset.Dish.Id).ToList();

            foreach (DishScriptable dish in DishesAsset)
            {
                Dishes.Add(dish.Dish);
            }

			//foreach (var category in CategoriesAsset)
			//{
			//	DishCategory categ = category.Category;
			//	categ.Dishes = new List<Dish>();

			//	foreach (var dish in category.DishesAssets)
			//	{
			//		categ.Dishes.Add(dish.Dish);
			//	}

			//	Categories.Add(categ);
			//}
        }
	}
}