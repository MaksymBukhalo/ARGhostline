using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FoodStoryTAS
{
    public class DishPanel : MonoBehaviour
    {
        public RectTransform RectTransform;

        public Image DishImage;
        public TextMeshProUGUI DishName;
        public Text DishLabel;
        public Text DishDescription;

        public void FillDishInfo(Dish dish)
        {
            DishImage.sprite = dish.Image;
            DishName.text = dish.Name;
            DishLabel.text = dish.Label;
            DishDescription.text = dish.Description;
        }
    }
}
