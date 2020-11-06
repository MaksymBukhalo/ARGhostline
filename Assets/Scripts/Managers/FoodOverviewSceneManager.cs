using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FoodStoryTAS
{
    public class FoodOverviewSceneManager : Singleton<FoodOverviewSceneManager>
    {
        [Header("Buttons")]
        [SerializeField] private Button _reloadSceneButton;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        /// <summary>
        /// Subscribing to all events.
        /// </summary>
        private void SubscribeEvents()
        {
            _reloadSceneButton.onClick.AddListener(ReloadScene);
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
            _reloadSceneButton.onClick.RemoveListener(ReloadScene);
        }

        private void ReloadScene()
        {
            ScenesManager.LoadFoodOverviewScene();
        }
    }
}
