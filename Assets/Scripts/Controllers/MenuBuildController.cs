using System.Collections.Generic;
using UnityEngine;

namespace FoodStoryTAS
{
	public class MenuBuildController : MonoBehaviour
	{
		[Header("Menu build elements:")]
		//[SerializeField] private GameObject _categoryItem;

		[SerializeField] private GameObject _dishItem;
		//[SerializeField] private GameObject _spaceItem;

		[Space, SerializeField] private Transform _menuContainer;

        [HideInInspector] public static List<DishItemController> DishItemControllers = new List<DishItemController>();

        private int _lastCategoryId = 0;

		private void Start()
		{
			BuildMenu();
		}

		public void BuildMenu(Menu menu = null)
		{
			Menu menuData = menu ?? MenuDataHolder.Instance.GetMenuData();

            //BuildCategoriesMenu(menuData.Categories);
            BuildDishesMenu(menuData.Dishes);
        }

		//private void BuildCategoriesMenu(List<DishCategory> categories)
		//{
		//	foreach (var category in categories)
		//	{
		//		//Create empty space object for visual separation.
		//		if (_lastCategoryId != category.Id)
		//		{
		//			_lastCategoryId = category.Id;

		//			Instantiate(_spaceItem, _menuContainer);
		//		}

		//		CategoryView categoryObj = Instantiate(_categoryItem, _menuContainer).GetComponent<CategoryView>();

		//		if (categoryObj)
		//		{
		//			categoryObj.FillInfo(category);
		//			BuildDishesMenu(category.Dishes);
		//		}
		//		else
		//		{
		//			Debug.LogError("CategoryView is empty or equals NULL!");
		//		}
		//	}
		//}

		private void BuildDishesMenu(List<Dish> dishes)
		{
            DishItemControllers = new List<DishItemController>();

            foreach (var dish in dishes)
			{
                DishItemController handler = Instantiate(_dishItem, _menuContainer).GetComponent<DishItemController>();

                if (handler)
                {
                    if (handler)
                    {
                        DishItemControllers.Add(handler);
                        handler.SetDishId(dish.Id);
                        MenuSelectionController.Instance.SetDefaultDish(handler);
                    }
                    else
                    {
                        Debug.LogError("DishButtonHandler is empty or equals NULL!");
                    }
                }
                else
                {
                    Debug.LogError("DishView is empty or equals NULL!");
                }

                //DishView dishObj = Instantiate(_dishItem, _menuContainer).GetComponent<DishView>();

                //if (dishObj)
                //{
                //    dishObj.FillInfo(dish);

                //    DishItemController handler = dishObj.GetComponent<DishItemController>();

                //    if (handler)
                //    {
                //        handler.SetDishId(dish.Id);
                //        MenuSelectionController.Instance.SetDefaultDish(handler);
                //    }
                //    else
                //    {
                //        Debug.LogError("DishButtonHandler is empty or equals NULL!");
                //    }
                //}
                //else
                //{
                //    Debug.LogError("DishView is empty or equals NULL!");
                //}
            }
		}

		private void OnEnable()
		{
			SubscribeEvents();
		}

		/// <summary>
		/// Subscribing to all events.
		/// </summary>
		private void SubscribeEvents()
		{
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
		}
	}
}