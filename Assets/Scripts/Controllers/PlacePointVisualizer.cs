using HedgehogTeam.EasyTouch;
using System;
using UnityEngine;

namespace FoodStoryTAS
{
	public class PlacePointVisualizer : Singleton<PlacePointVisualizer>
	{
		public static event Action PlaceHasBeenDetected;

		[SerializeField] private Transform _cameraObserver;

		[SerializeField] private Transform _pointMocObj;
		[SerializeField] private MeshRenderer _pointVisual;

		[Header("Sprites data:")]
        [SerializeField] private Texture _placeUndetectedSprite;
        [SerializeField] private Texture _placeDetectedSprite;

        [Header("Pointer parameters: ")]
		[SerializeField] private Quaternion _defaultRotationOffset;

		private bool _isPlaceHasBeenDetected = false;

		private void OnPlaceDetected(Pose pointerPosition)
		{
			if (!_isPlaceHasBeenDetected)
			{
				_isPlaceHasBeenDetected = true;
				PlaceHasBeenDetected?.Invoke();
			}
			_pointVisual.material.mainTexture = _placeDetectedSprite;
			_pointMocObj.transform.position = pointerPosition.position;
			_pointVisual.transform.rotation = Quaternion.Euler(pointerPosition.rotation.eulerAngles);

			_pointVisual.material.color = Constants.YellowColor;
		}

		private void OnPlaceUndetected()
		{
            _isPlaceHasBeenDetected = false;

            _pointVisual.material.mainTexture = _placeUndetectedSprite;
			_pointMocObj.transform.localPosition = _cameraObserver.position;
			_pointVisual.transform.localRotation = Quaternion.identity;

			_pointVisual.material.color = Color.white;

            //if (ScanStepsController.Instance.CurrentScanStep == ScanSteps.TapToPlaceDish)
            //{
            //    ScanStepsController.Instance.DoScanning(ScanSteps.Scan);
            //}
		}

		private void Update()
		{
			Pose? position = RaycastManager.Instance.ConstructArRaycastPose();

			if (!RaycastManager.Instance.ConstructArRaycastPose().HasValue)
			{
				OnPlaceUndetected();
			}
			else
			{
				OnPlaceDetected(position.Value);
			}
		}

        private void OnFirstTap(Gesture gesture)
        {
            if (RaycastManager.Instance.ConstructArRaycastPose().HasValue)
            {
                SetActiveVisualPoint(false);
            }
        }

        public void SetActiveVisualPoint(bool value)
        {
            _pointVisual.gameObject.SetActive(value);
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
			EasyTouch.On_TouchDown += OnFirstTap;
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
			EasyTouch.On_TouchDown -= OnFirstTap;
		}
	}
}