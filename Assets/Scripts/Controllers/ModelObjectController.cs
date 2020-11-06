using UnityEngine;

namespace FoodStoryTAS
{
	public class ModelObjectController : MonoBehaviour
	{
		/// <summary>
		/// Id of model that should be equals dishId of specific dish.
		/// </summary>
		[SerializeField] private int _modelId;

		/// <summary>
		/// Get id of model.
		/// </summary>
		public int GetModelId()
		{
			return _modelId;
		}

		/// <summary>
		/// Activate model.
		/// </summary>
		public void Activate()
		{
			gameObject.SetActive(true);
		}

		/// <summary>
		/// Deactivate model.
		/// </summary>
		public void Deactivate()
		{
			gameObject.SetActive(false);
		}
	}
}