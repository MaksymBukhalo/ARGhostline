using UnityEngine;
using UnityEngine.UI;

namespace FoodStoryTAS
{
    public class ReviewDishesController : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _selectNextDishButton;
        [SerializeField] private Button _selectPreviousDishButton;

        private int CurrentDishId
        {
            get
            {
                if (MenuSelectionController.Instance.SelectedDish)
                {
                    return MenuSelectionController.Instance.SelectedDish.GetDishId();
                }
                return 1;
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
            _selectNextDishButton.onClick.AddListener(SelectNextDish);
            _selectPreviousDishButton.onClick.AddListener(SelectPreviousDish);
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
            _selectNextDishButton.onClick.RemoveListener(SelectNextDish);
            _selectPreviousDishButton.onClick.RemoveListener(SelectPreviousDish);
        }

        private void SelectNextDish()
        {
            int nextDishId = CurrentDishId + 1;
            if (nextDishId > MenuBuildController.DishItemControllers.Count)
            {
                nextDishId = 1;
            }
            MenuSelectionController.Instance.SelectMenuItem(MenuBuildController.DishItemControllers[nextDishId - 1]);
        }

        private void SelectPreviousDish()
        {
            int previousDishId = CurrentDishId - 1;
            if (previousDishId < 1)
            {
                previousDishId = MenuBuildController.DishItemControllers.Count;
            }
            MenuSelectionController.Instance.SelectMenuItem(MenuBuildController.DishItemControllers[previousDishId - 1]);
        }
    }
}
