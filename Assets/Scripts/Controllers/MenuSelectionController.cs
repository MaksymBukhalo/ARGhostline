using System.Collections.Generic;
using UnityEngine;

namespace FoodStoryTAS
{
	public class MenuSelectionController : Singleton<MenuSelectionController>
	{
		public bool IsCanSelect = false;

		[SerializeField] private DishItemController _selectedDish;
		[SerializeField] private DishItemController _defaultDish;

        public DishItemController SelectedDish
        {
            get
            {
                return _selectedDish;
            }
        }

        public void SetDefaultDish(DishItemController dishController)
		{
			if (_defaultDish)
				return;

			_defaultDish = dishController;
		}

		public void SelectDefaultDish()
		{
			if (!_defaultDish)
				return;

			SelectMenuItem(_defaultDish);
		}

		/// <summary>
		/// Select dish in menu.
		/// </summary>
		/// <param name="dishController">Selected menu item.</param>
		public void SelectMenuItem(DishItemController dishController)
		{
			if (!IsCanSelect || dishController == _selectedDish)
			{
				return;
			}

			if (_selectedDish)
			{
				_selectedDish.GetComponent<ISelectable>()?.Deselect();
			}

            _selectedDish = dishController;

			_selectedDish.GetComponent<ISelectable>()?.Select();
			Dish dish = MenuDataHolder.Instance.FindDishById(_selectedDish.GetDishId());
			//DescriptionViewController.Instance.FillDescriptionPanel(dish.Description);
			//DescriptionViewController.Instance.Show();
			ModelSelectController.Instance.ShowModelById(dish.Id);
		}

        /// <summary>
        /// Fill dish info in the menu panel.
        /// </summary>
        public void FillDishInfo(int dishId)
        {
            Dish dish = MenuDataHolder.Instance.FindDishById(dishId);
            MenuPanelController.Instance.FillDescriptionPanel(dish);
        }

        public void DeselectCurrentMenuItem()
        {
            ModelSelectController.Instance.HideSelectedModel();
            _selectedDish = null;
            TurnOffSelectionMode();
        }

        public void TurnOnSelectionMode()
		{
			IsCanSelect = true;
		}

		public void TurnOffSelectionMode()
		{
			IsCanSelect = false;
		}
	}
}