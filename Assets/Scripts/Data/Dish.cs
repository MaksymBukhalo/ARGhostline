using UnityEngine;

namespace FoodStoryTAS
{
	[System.Serializable]
	public class Dish
	{
		public string Name;
		public int Id;
        public string Label;
		public string Description;
		public string Price;
		public Currencies Currency;
		public Sprite Image;
	}
}