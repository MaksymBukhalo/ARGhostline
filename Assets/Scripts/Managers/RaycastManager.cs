using Bidon.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace FoodStoryTAS
{
	public class RaycastManager : BaseInstance<RaycastManager>
	{
		#region Fields

		public TrackableType DetectionType;
		[SerializeField] protected Camera _rayOriginCamera;

#pragma warning disable 0649
		[SerializeField]
		private ARRaycastManager _ARSessionOrigin;
#pragma warning restore 0649

		private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

#pragma warning disable 0649
		[Header("Debug options:")]
		[SerializeField]
		private bool _visualizeRaycast;
#pragma warning restore 0649

		private const float RAYCAST_DISTANCE = 500f;

		#endregion // Fields

		#region Events

		public event Action<Vector3> PlacingObjectEvent;

		#endregion // Events

		#region Monobehavior Callbacks

		private void Update()
		{
			//			ConstructArRaycastByMouseButton();
#if UNITY_EDITOR
			if (_visualizeRaycast)
			{
				DrawDebugLine();
			}
#endif
		}

		#endregion // MonoBehaviour Callback

		#region Methods

		/// <summary>
		/// Draw the debug line in editor.
		/// </summary>
		private void DrawDebugLine()
		{
			var newWorldPos = Input.mousePosition;
			newWorldPos.z = RAYCAST_DISTANCE;
			newWorldPos = _rayOriginCamera.ScreenToWorldPoint(newWorldPos);
			Debug.DrawLine(_rayOriginCamera.transform.position, newWorldPos, Color.green);
		}

		#endregion // Methods

		/// <summary>
		/// Constructs raycast and return hit point position on scanned surface
		/// </summary>
		/// <returns></returns>
		public Vector3? ConstructArRaycastByMouseButton()
		{
			if (Input.GetMouseButtonDown(0))
			{
				return ConstructArRaycast().Value.position;
			}

			return null;
		}

		/// <summary>
		/// AR raycast reacts only on scanned surfaces.
		/// Based on input mousePosition.
		/// </summary>
		/// <returns></returns>
		public Pose? ConstructArRaycast()
		{
			if (IsPointerOnUi())
				return null;

			if (_ARSessionOrigin != null && _ARSessionOrigin.Raycast(Input.mousePosition, _hits, DetectionType))
			{
				PlacingObjectEvent?.Invoke(_hits[0].pose.position);
				return _hits[0].pose;
			}

			return null;
		}

		/// <summary>
		/// AR raycast reacts only on scanned surfaces.
		/// Based on screen center.
		/// </summary>
		/// <returns></returns>
		public Pose? ConstructArRaycastPose()
		{
			if (IsPointerOnUi())
				return null;

			Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
			if (_ARSessionOrigin != null && _ARSessionOrigin.Raycast(screenCenter, _hits, DetectionType))
			{
				PlacingObjectEvent?.Invoke(_hits[0].pose.position);
				return _hits[0].pose;
			}

			return null;
		}

		/// <summary>
		/// Detects if pointer on UI object.
		/// </summary>
		/// <returns><c>true</c>, if pointer on user interface was ised, <c>false</c> otherwise.</returns>
		private static bool IsPointerOnUi()
		{
			return EventSystem.current.IsPointerOverGameObject();
		}
	}
}