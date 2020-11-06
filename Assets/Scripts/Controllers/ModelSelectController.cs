using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FoodStoryTAS
{
	public class ModelSelectController : Singleton<ModelSelectController>
	{
		[SerializeField] private List<ModelObjectController> _models;

		private ModelObjectController _selectedModel;

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

		/// <summary>
		/// Show model based on dishId.
		/// </summary>
		/// <param name="dishId">Dish id which should be equals modelId related to specific dish</param>
		public void ShowModelById(int dishId)
		{
			ModelObjectController modelObj = _models.First(model => model.GetModelId() == dishId);

			if (_selectedModel != null && _selectedModel != modelObj)
			{
				_selectedModel.Deactivate();
			}

			_selectedModel = modelObj;
			_selectedModel.Activate();
		}

        public void HideSelectedModel()
        {
            if (_selectedModel != null)
            {
                _selectedModel.Deactivate();
                _selectedModel = null;
            }
        }

		public ModelObjectController ModelById(int dishId)
		{
			ModelObjectController _selectedModel2 = _models[dishId];
			return _selectedModel2;
		}
    }
}