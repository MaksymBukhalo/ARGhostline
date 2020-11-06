using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;

namespace FoodStoryTAS
{
	public class ObjectsContainerPinchToZoom : MonoBehaviour
	{
		public float ScaleMultiplier = 2f;
		public float MaxZoomPercents = 20f;

		[Header("Serialized Fields")]
		[SerializeField]
		private float _maxSize;

		private float _initSize;

		private void OnEnable()
		{
			SubscribeToEvents();
		}

		private void OnDisable()
		{
			UnsubscribeFromEvents();
		}

		private void OnDestroy()
		{
			UnsubscribeFromEvents();
		}

		private void Awake()
		{
			// Calculate 10 percents of model
			_initSize = transform.localScale.x;
			_maxSize = PercentsSizeValue(_initSize, MaxZoomPercents);
		}

		private void SubscribeToEvents()
		{
			EasyTouch.On_Cancel2Fingers += On_Cancel2Fingers;
			EasyTouch.On_PinchIn += On_PinchIn;
			EasyTouch.On_PinchOut += On_PinchOut;
			EasyTouch.On_PinchEnd += On_PinchEnd;
		}

		private void UnsubscribeFromEvents()
		{
			EasyTouch.On_Cancel2Fingers -= On_Cancel2Fingers;
			EasyTouch.On_PinchIn -= On_PinchIn;
			EasyTouch.On_PinchOut -= On_PinchOut;
			EasyTouch.On_PinchEnd -= On_PinchEnd;
		}

		// At the pinch in
		private void On_PinchIn(Gesture gesture)
		{
			float zoom = Time.deltaTime * (gesture.deltaPinch * ScaleMultiplier);

			Vector3 scale = transform.localScale;
			Vector3 newSize = new Vector3(scale.x - zoom, scale.y - zoom, scale.z - zoom);

			// Apply new size if it pass the limits
			if (newSize.x > (_initSize - _maxSize))
			{
				transform.localScale = newSize;
			}

			// Check if scale is negative
			if (transform.localScale.x <= 0)
				ResetScale();

			// Verification that the action on the object
			if (gesture.pickedObject == gameObject)
			{
			}
		}

		// At the pinch out
		private void On_PinchOut(Gesture gesture)
		{
			float zoom = (Time.deltaTime * gesture.deltaPinch) * ScaleMultiplier;

			Vector3 scale = transform.localScale;
			Vector3 newSize = new Vector3(scale.x + zoom, scale.y + zoom, scale.z + zoom);

			// Apply new size if it pass the limits
			if (newSize.x < (_initSize + _maxSize))
			{
				transform.localScale = newSize;
			}

			// Verification that the action on the object
			if (gesture.pickedObject == gameObject)
			{
			}
		}

		// At the pinch end
		private void On_PinchEnd(Gesture gesture)
		{
			if (gesture.pickedObject == gameObject)
			{
			}
		}

		private void On_Cancel2Fingers(Gesture gesture)
		{
			EasyTouch.SetEnablePinch(true);
		}

		private void ResetScale()
		{
			transform.localScale = Vector3.one;
		}

		private float PercentsSizeValue(float size, float percents)
		{
			return Mathf.Abs((size / 100f) * percents);
		}
	}
}