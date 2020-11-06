using FoodStoryTAS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPanelController : MonoBehaviour
{
	[SerializeField] private List<string> _labels;
	[SerializeField] private MenuScriptable _menuAsset;
	[SerializeField] private GameObject _exampleLabels;
	[SerializeField] private GameObject _exampleButton;
	[SerializeField] private GameObject _viewContent;
	[SerializeField] private Transform _startContentPosition;
	[SerializeField] private Text _TextInfo;

	private float y;
	private float indent = 80f;
	private float indentAfterCategoryFood = 90f;
	private float indentAfterEndCategoryFood = 60f;
	private GameObject Label;


	void Start()
	{
		y = _startContentPosition.position.y;
		SetupContentInViewPanel();
		_viewContent.GetComponent<RectTransform>().sizeDelta = new Vector2(_viewContent.GetComponent<RectTransform>().sizeDelta.x,(_startContentPosition.position.y - y - indent));
	}

	private void SetupContentInViewPanel()
	{
		for (int i = 0; i < _labels.Count; i++)
		{
			SetupLabels(_labels[i]);
			SetupContentInLabels(_labels[i]);
			y = y - indentAfterEndCategoryFood;
		}

	}

	private void SetupContentInLabels(string nameLabels)
	{
		for (int i = 0; i < _menuAsset.DishesAsset.Count; i++)
		{
			if (nameLabels == _menuAsset.DishesAsset[i].Dish.Label)
			{
				SetupButton(_menuAsset.DishesAsset[i].Dish);
			}
		}
	}

	private void SetupLabels(string nameLabels)
	{
		Label = Instantiate(_exampleLabels, _viewContent.transform);
		//Label.transform.position = new Vector3(_startContentPosition.position.x, y, _startContentPosition.position.z);
		Label.GetComponent<Text>().text = nameLabels;
		y = y - indentAfterCategoryFood;
	}

	private void SetupButton(Dish dish)
	{
		GameObject ButtonDish = Instantiate(_exampleButton, Label.transform);
		//ButtonDish.transform.position = new Vector3(_startContentPosition.position.x, y, _startContentPosition.position.z);
		ButtonDish.GetComponent<SpawnDish>().Id = dish.Id;
		ButtonDish.GetComponent<SpawnDish>().TextInfo = _TextInfo;
		ButtonDish.GetComponent<SpawnDish>().NameDish.text = dish.Name;
		ButtonDish.GetComponent<SpawnDish>().Price.text = "$"+dish.Price+"++";
		y = y - indent;
	}
}
