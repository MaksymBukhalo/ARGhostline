using FoodStoryTAS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnDish : MonoBehaviour
{
    public int Id;
    public Text TextInfo;
    public Text NameDish;
    public Text Price;
    public Color ColorForEnabledButton;

    private ModelObjectController modelObjectController;
    private bool dishIsSpawn = true;


    private void Start()
	{
        modelObjectController = ModelSelectController.Instance.ModelById(Id - 1);
    }

	private void Update()
	{
        ChangeButtonCollorIfNeedIt();
    }

	public void GetDish()
    {
        MenuSelectionController.Instance.SelectMenuItem(MenuBuildController.DishItemControllers[Id - 1]);
        Dish dish = MenuDataHolder.Instance.FindDishById(Id);
        TextInfo.text = dish.Description;
        TextInfo.gameObject.GetComponent<TextBackgroundController>().SetupNewSizeDescription();
        Color color = new Color(5f, 63f, 85f);
        dishIsSpawn = true;
    }

    private void ChangeColorToEnabledButton(Color colorForButton, Color colorForText)
	{
        gameObject.GetComponent<Image>().color = colorForButton;
        NameDish.color = colorForText;
        Price.color = colorForText;
    }

    private void ChangeButtonCollorIfNeedIt()
	{
        if (!modelObjectController.gameObject.activeSelf)
        {
            ChangeColorToEnabledButton(Color.white, Color.black);
        }
		else if(dishIsSpawn)
        {
            ChangeColorToEnabledButton(ColorForEnabledButton, Color.white);
            dishIsSpawn = false;
        }
    }
}
