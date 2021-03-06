﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using FoodStoryTAS;

public class GhostlineMenuPanelController : MonoBehaviour
{
	[Header("SerializeFields")]
	[SerializeField] private CanvasGroup _scanStepsPanelGroup;
	private Button _closeMenuPanelButton;

	public bool CanChangeDishInMenu { get; private set; } = true;

	private Sequence _flipDishesSequence;

	private int _nextDishPanelGroupIndex = 1; // Тext panel index is 1 at the start of the application.
	private int _currentVisibleDishId = 1;
	private float _menuPanelsAppearingTime = 0.25f;
	private float _menuPanelsFlippingTime = 0.5f;

	private void Start()
	{
		//ChangeSelectDishAndClosePanelButtons();
	}


	private int CurrentDishPanelGroupIndex
	{
		get
		{
			return _nextDishPanelGroupIndex == 0 ? 1 : 0;
		}
	}

	private int CurrentDishId
	{
		get
		{
			if (MenuSelectionController.Instance.SelectedDish)
			{
				return MenuSelectionController.Instance.SelectedDish.GetDishId();
			}
			return 1;
		}
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
		//_closeMenuPanelButton.onClick.AddListener(CloseMenuPanel);

		//EventManager.ResolutionChangeEvent += AlignNextDishPanel;
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
		//_closeMenuPanelButton.onClick.RemoveListener(CloseMenuPanel);

		//EventManager.ResolutionChangeEvent -= AlignNextDishPanel;
	}

	private void Update()
	{

	}


	/// <summary>
	/// Fill panel.
	/// </summary>
	/// <param name="description">Text which should be show on panel.</param>


	private void OpenMenuPanel()
	{
		MenuSelectionController.Instance.FillDishInfo(CurrentDishId);
		_currentVisibleDishId = CurrentDishId;
		//Switch_SelectDish_And_CloseMenu_Buttons();

		KillSequence();
		_flipDishesSequence = DOTween.Sequence();
		_flipDishesSequence.Insert(0, _scanStepsPanelGroup.DOFade(0, _menuPanelsAppearingTime).SetEase(Ease.Linear)).OnComplete(() =>
		{
			_scanStepsPanelGroup.gameObject.SetActive(false);
		});

		// Make screenshot(for menu panel background) without dish on the background.
		// REMOVED from 23.10.2019 form feedback (https://drive.google.com/file/d/1HFumbp4fkXj5Ujh97cjYZOiSk-gAOAFo/view?usp=sharing)
		//StartCoroutine(SetScreenshotForBackground());
	}

	private void CloseMenuPanel()
	{
		KillSequence();
		_flipDishesSequence = DOTween.Sequence();

		_scanStepsPanelGroup.gameObject.SetActive(true);
	}

	private void ShowNextDish()
	{
		if (CanChangeDishInMenu)
		{
			AnimateDishPanelsFlipping(-1);
			InverseNextDishPanelGroupIndex();

			_currentVisibleDishId++;
			if (_currentVisibleDishId > MenuBuildController.DishItemControllers.Count)
			{
				_currentVisibleDishId = 1;
			}

			MenuSelectionController.Instance.FillDishInfo(_currentVisibleDishId);
			SelectCurrentDish();
		}
	}

	private void ShowPreviousDish()
	{
		if (CanChangeDishInMenu)
		{
			AnimateDishPanelsFlipping(1);
			InverseNextDishPanelGroupIndex();

			_currentVisibleDishId--;
			if (_currentVisibleDishId < 1)
			{
				_currentVisibleDishId = MenuBuildController.DishItemControllers.Count;
			}

			MenuSelectionController.Instance.FillDishInfo(_currentVisibleDishId);
			SelectCurrentDish();
		}
	}

	public void SelectCurrentDish()
	{
		MenuSelectionController.Instance.SelectMenuItem(MenuBuildController.DishItemControllers[_currentVisibleDishId - 1]);
		//Switch_SelectDish_And_CloseMenu_Buttons();
	}

	private void AnimateDishPanelsFlipping(int direction)
	{
		CanChangeDishInMenu = false;


		KillSequence();
		_flipDishesSequence = DOTween.Sequence();
	}

	private void InverseNextDishPanelGroupIndex()
	{
		_nextDishPanelGroupIndex = _nextDishPanelGroupIndex == 0 ? 1 : 0; // Inverse 1 to 0 and vice verse.
	}

	private float DishPanelWidth(DishPanel dishPanel)
	{
		return dishPanel.RectTransform.rect.x * 2;
	}

	private IEnumerator SetScreenshotForBackground()
	{
		ModelSelectController.Instance.HideSelectedModel();
		SnapshotController.Instance.TakeSnapshotForBackground();

		yield return new WaitUntil(() => SnapshotController.Instance.CurrentSnapshotForBackground);

		Rect rect = new Rect(0, 0, Screen.width, Screen.height);
		Sprite image = Sprite.Create(SnapshotController.Instance.CurrentSnapshotForBackground, rect, Vector2.zero, 144);
		SnapshotController.Instance.CurrentSnapshotForBackground = null;

		ModelSelectController.Instance.ShowModelById(CurrentDishId);

		yield break;
	}

	//private void Switch_SelectDish_And_CloseMenu_Buttons()
	//{
	//    _selectCurrentDishButton.gameObject.SetActive(CurrentDishId != _currentVisibleDishId);
	//    _closeMenuPanelButton.gameObject.SetActive(CurrentDishId == _currentVisibleDishId);
	//}

	private void KillSequence()
	{
		if (_flipDishesSequence != null)
		{
			_flipDishesSequence.Kill();
		}
	}
}

