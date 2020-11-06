using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _loadingtMenu;
    [SerializeField] private Animation _startLoadLoadingMenu;

    private void Update()
    {
        if ((_startMenu.activeSelf==true && Input.touchCount>0 && Input.touches[0].phase == TouchPhase.Began) || (_startMenu.activeSelf == true && Input.GetMouseButtonDown(0)))
        {
            _startLoadLoadingMenu.Play();
            StartCoroutine(LoadingMenu());
        }
    }

    private IEnumerator LoadingMenu()
    {
        yield return new WaitForSeconds(1f);
        _startMenu.SetActive(false);
        _loadingtMenu.SetActive(true);
    }
}
