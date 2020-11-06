using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBackgroundController : MonoBehaviour
{
	public Image TextBackgroud;

	private float textHeight;
	private Text descriptionText;
	private RectTransform rectSizeImage;
	private RectTransform rectSizeText;

	private void Start()
	{
		descriptionText = gameObject.GetComponent<Text>();
		rectSizeImage = TextBackgroud.gameObject.GetComponent<RectTransform>();
		rectSizeText = descriptionText.gameObject.GetComponent<RectTransform>();
		SetupNewSizeDescription();
	}

	public void SetupNewSizeDescription()
	{
		float stringsCount = (descriptionText.text.Length / (rectSizeImage.sizeDelta.x / descriptionText.fontSize * 1.75f));
		Debug.Log(stringsCount);
		textHeight = stringsCount * (descriptionText.fontSize* 1.3f) + 50f;
		rectSizeImage.sizeDelta = new Vector2(rectSizeImage.sizeDelta.x, textHeight);
	}
}
