using Bidon.Utilities;
using HedgehogTeam.EasyTouch;
using UnityEngine;

namespace FoodStoryTAS
{
	public class ObjectsContainerPlacer : BaseInstance<ObjectsContainerPlacer>
	{
		public bool IsCanBeMoved = true;
		public bool IsObjectAlreadyPlaced = false;

		private BoxCollider _collider;
		private bool _isTwoFingersDetected;
		private int _fingerIndex;

		private void OnEnable()
		{
			EasyTouch.On_TouchDown += OnTouchDown;
			EasyTouch.On_DragStart += OnDragStart;
			EasyTouch.On_Drag += OnDrag;
			EasyTouch.On_TouchDown2Fingers += OnTouchDown2Fingers;
			EasyTouch.On_TouchUp2Fingers += OnTouchUp2Fingers;
		}

		private void OnDisable()
		{
			EasyTouch.On_TouchDown -= OnTouchDown;
			EasyTouch.On_DragStart -= OnDragStart;
			EasyTouch.On_Drag -= OnDrag;
			EasyTouch.On_TouchDown2Fingers -= OnTouchDown2Fingers;
			EasyTouch.On_TouchUp2Fingers -= OnTouchUp2Fingers;
		}

		private void Start()
		{
			Init();
		}

		private void Init()
		{
			// Init components
			_collider = GetComponent<BoxCollider>();
		}

		private void OnTouchDown(Gesture gesture)
		{
			if (_isTwoFingersDetected)
				return;

			if (!IsObjectAlreadyPlaced)
			{
				MoveContainerToScreenCenterPos();
			}
		}

		private void OnDragStart(Gesture gesture)
		{
			// Verification that the action on the object
			if (gesture.pickedObject == gameObject)
			{
				_fingerIndex = gesture.fingerIndex;
			}
		}

		private void OnDrag(Gesture gesture)
		{
			if (_isTwoFingersDetected)
				return;

			// Verification that the action on the object
			if (gesture.pickedObject == gameObject && _fingerIndex == gesture.fingerIndex)
			{
				MoveContainerToMousePos();
			}
		}

		protected void OnTouchDown2Fingers(Gesture gesture)
		{
			_isTwoFingersDetected = true;
		}

		protected void OnTouchUp2Fingers(Gesture gesture)
		{
			_isTwoFingersDetected = false;
		}

		/// <summary>
		/// Move container exactly to specific pos
		/// </summary>
		/// <param name="pos"></param>
		public void MoveContainerToPos(Vector3 pos)
		{
			// Move container with collider offset
			this.transform.position = pos + new Vector3(0, _collider.size.y / 2, 0);
		}

		/// <summary>
		/// Move container to position gets from Raycast manager
		/// </summary>
		public void MoveContainerToScreenCenterPos()
		{
			if (!IsCanBeMoved)
				return;

			Pose? newPos = RaycastManager.Instance.ConstructArRaycastPose();

			if (newPos.HasValue)
			{
				// Rotate container with placement rotation
				transform.localRotation = Quaternion.Euler(newPos.Value.rotation.eulerAngles);
				// Move container with collider offset
				transform.localPosition = newPos.Value.position;

				// Define that object is placed
				if (!IsObjectAlreadyPlaced)
				{
					IsObjectAlreadyPlaced = true;
				}
			}
		}

		/// <summary>
		/// Move container to position gets from Raycast manager
		/// </summary>
		public void MoveContainerToMousePos()
		{
			if (!IsCanBeMoved)
				return;

			Pose? newPos = RaycastManager.Instance.ConstructArRaycast();

			if (newPos.HasValue)
			{
				// Rotate container with placement rotation
				transform.localRotation = Quaternion.Euler(newPos.Value.rotation.eulerAngles);
				// Move container with collider offset
				transform.localPosition = newPos.Value.position;
			}
		}
	}
}