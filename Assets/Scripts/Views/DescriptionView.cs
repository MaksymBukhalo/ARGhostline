using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace FoodStoryTAS
{
	public class DescriptionView : MonoBehaviour, IAppearable
	{
		private Sequence _animationSequence;

		/// <summary>
		/// Text component of description panel.
		/// </summary>
		[SerializeField] private RectTransform _descriptionRect;

		/// <summary>
		/// Text component of description panel.
		/// </summary>
		[SerializeField] private Text _descriptionText;

		/// <summary>
		/// Fill description panel.
		/// </summary>
		/// <param name="data">Description data.</param>
		public void Fill(string description)
		{
			if (string.IsNullOrEmpty(description))
			{
				Debug.LogError("Descriptioin is empty or equals NULL!");

				return;
			}

			_descriptionText.text = description;
		}

		/// <summary>
		/// Show description panel animation.
		/// </summary>
		public void Show()
		{
			KillSequence();
			//_descriptionRect.localScale = Vector2.zero;

			//_animationSequence.Append(_descriptionRect.DOScale(new Vector3(0.1f,1f,0f), 0.2f));
			//_animationSequence.Append(_descriptionRect.DOScale(Vector3.one,0.8f).SetEase(Ease.OutBack));
		}

		/// <summary>
		/// Hide description panel animation.
		/// </summary>
		public void Hide()
		{
			KillSequence();
		}

		private void KillSequence()
		{
			if (_animationSequence != null)
			{
				_animationSequence.Kill();
			}
		}
	}
}