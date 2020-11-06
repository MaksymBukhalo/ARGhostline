using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;
using UnityEngine.Events;

namespace FoodStoryTAS
{
	public class ObjectsContainerRotateByTwoFingers : QuickBase
	{
		private float _axisActionValue = 0;

		public ObjectsContainerRotateByTwoFingers()
		{
			quickActionName = "QuickSwipe" + System.Guid.NewGuid().ToString().Substring(0, 7);
		}

		public override void OnEnable()
		{
			base.OnEnable();
			SubscribeToEvents();
		}

		public override void OnDisable()
		{
			base.OnDisable();
			UnsubscribeFromEvents();
		}

		private void OnDestroy()
		{
			UnsubscribeFromEvents();
		}

		private void SubscribeToEvents()
		{
			EasyTouch.On_TouchStart2Fingers += On_TouchStart2Fingers;
			EasyTouch.On_Swipe2Fingers += On_Swipe2Fingers;
			EasyTouch.On_SwipeEnd2Fingers += On_SwipeEnd2Fingers;
		}

		private void UnsubscribeFromEvents()
		{
			EasyTouch.On_TouchStart2Fingers -= On_TouchStart2Fingers;
			EasyTouch.On_Swipe2Fingers -= On_Swipe2Fingers;
			EasyTouch.On_SwipeEnd2Fingers -= On_SwipeEnd2Fingers;
		}

		private void On_TouchStart2Fingers(Gesture gesture)
		{
			// Verification that the action on the object
			if (gesture.pickedObject == gameObject)
			{
			}
		}

		private void On_Swipe2Fingers(Gesture gesture)
		{
			if (HorizontalDirections(gesture))
			{
				RotateAction(gesture);
			}
		}

		private void On_SwipeEnd2Fingers(Gesture gesture)
		{
		}

		private bool HorizontalDirections(Gesture gesture)
		{
			float coef = -1;

			if (inverseAxisValue)
			{
				coef = 1;
			}

			_axisActionValue = 0;

			if (gesture.swipe == EasyTouch.SwipeDirection.Left || gesture.swipe == EasyTouch.SwipeDirection.Right)
			{
				_axisActionValue = gesture.deltaPosition.x * coef;
				return true;
			}

			_axisActionValue = 0;
			return false;
		}

		private void RotateAction(Gesture gesture)
		{
			_axisActionValue *= sensibility;

			DoDirectAction(_axisActionValue);
		}
	}
}