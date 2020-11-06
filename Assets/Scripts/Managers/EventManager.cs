using System;
using UnityEngine;

namespace FoodStoryTAS
{
    public class EventManager : MonoBehaviour
    {
        public static event Action ResolutionChangeEvent;

        private Vector2 _currentResolution;

        private void Start()
        {
            _currentResolution = new Vector2(Screen.width, Screen.height);
        }

        private void Update()
        {
            if (_currentResolution.x != Screen.width || _currentResolution.y != Screen.height)
            {
                ResolutionChangeEvent?.Invoke();
            }
        }
    }
}
