using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FoodStoryTAS
{
    /// <summary>
    /// Attach this script to the panel where you need to handle swiping.
    /// </summary>
    public class MenuPanelSwiping : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Vector2 StartTouch { get; private set; }
        public Vector2 SwipeDelta { get; private set; }

        public bool SwipeLeft { get; private set; }
        public bool SwipeRight { get; private set; }
        public bool SwipeUp { get; private set; }
        public bool SwipeDown { get; private set; }

        private bool _isSwiped = false;

        private const float SWIPE_DEADZONE_RADIUS_IN_PIXELS = 125;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isSwiped = false;

#if UNITY_EDITOR
            MouseInputs();
#else
            FingersInputs();
#endif
        }

        public void OnDrag(PointerEventData eventData)
        {
            CalculateDistance();
            CalculateSwipeDirection();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ResetInputs();
            ResetSwipes();
        }

        private void MouseInputs()
        {
            StartTouch = Input.mousePosition;
        }

        private void FingersInputs()
        {
            StartTouch = Input.touches[0].position;
        }

        private void CalculateDistance()
        {
            SwipeDelta = Vector3.zero;

            if (Input.touches.Length > 0)
            {
                SwipeDelta = Input.touches[0].position - StartTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                SwipeDelta = (Vector2)Input.mousePosition - StartTouch;
            }
        }

        private void CalculateSwipeDirection()
        {
            if (SwipeDelta.magnitude > SWIPE_DEADZONE_RADIUS_IN_PIXELS && !_isSwiped)
            {
                float x = SwipeDelta.x;
                float y = SwipeDelta.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    SwipeLeft = x < 0 ? true : false;
                    SwipeRight = x > 0 ? true : false;
                }
                else
                {
                    SwipeUp = y < 0 ? true : false;
                    SwipeDown = y > 0 ? true : false;
                }

                _isSwiped = true;
                ResetInputs();
            }
            else
            {
                ResetSwipes();
            }
        }

        private void ResetInputs()
        {
            StartTouch = SwipeDelta = Vector2.zero;
        }

        private void ResetSwipes()
        {
            SwipeLeft = false;
            SwipeRight = false;
            SwipeUp = false;
            SwipeDown = false;
        }
    }
}
