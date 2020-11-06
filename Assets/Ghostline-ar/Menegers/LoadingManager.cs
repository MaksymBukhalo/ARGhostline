using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
	[Header("LoadingSceneID")]
	[SerializeField] private int _sceneID;

	[SerializeField] private GameObject _loadingtMenu;
    [SerializeField] private Image _loadingImage;
    [SerializeField]  private Text _progresText;
	private bool isLoading = true;

	private void Update()
	{
		if (_loadingtMenu.activeSelf && isLoading)
		{
			StartCoroutine(AsyncLoad());
			isLoading = false;
		}
	}

	private IEnumerator AsyncLoad()
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneID);
		while (!operation.isDone)
		{
			int progresNumber = (int)(operation.progress * 100);
			if (operation.progress >= 0.9f)
			{
				_loadingImage.fillAmount = 1;
				progresNumber = 100;
			}
			else
			{
				_loadingImage.fillAmount = operation.progress;
			}
			_progresText.text = string.Format("{0}%", progresNumber);
			yield return null;
		}
	}
}
