using System.Collections;
using UnityEngine;
using HedgehogTeam.EasyTouch;

namespace FoodStoryTAS
{
	public abstract class RotatorAnimation : MonoBehaviour
	{
		[Header("Rotation settings")]
		public float RotateSpeed = 5f;

		public bool IsReverse;
		public bool IsRotate;

		[Header("Timer settings")]
		public float TimeDelay = 5f;

		private IEnumerator _timerCoroutine;
		private Vector3 _rotationVector;

		public abstract void OnEnable();
		public abstract void OnDisable();

		public virtual void Update()
		{
			if (IsRotate)
			{
				transform.Rotate(IsReverse ? Vector3.down : Vector3.up, RotateSpeed * Time.deltaTime);
			}
		}

		public virtual void BeginRotation(Gesture gesture)
		{
			IsRotate = false;

			if (_timerCoroutine != null)
			{
				StopCoroutine(_timerCoroutine);
			}

			_timerCoroutine = RotationDelayTimer();
			StartCoroutine(_timerCoroutine);
		}

		private IEnumerator RotationDelayTimer()
		{
			yield return new WaitForSeconds(TimeDelay);
			IsRotate = true;
		}
	}
}