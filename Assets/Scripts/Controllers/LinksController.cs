using System;
using UnityEngine;
using UnityEngine.UI;

namespace FoodStoryTAS
{
    public class LinksController : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private string _getMoreRecipesLink;
        [SerializeField] private string _watchTheVideoLink;
        [SerializeField] private string _avocadoShowWebsiteLink;
        [SerializeField] private string _naturesPrideWebsiteLink;

        [Header("Buttons")]
        [SerializeField] private Button _getMoreRecipesButton;
        [SerializeField] private Button _watchTheVideoButton;
        [SerializeField] private Button _visitAvocadoShowWebsiteButton;
        [SerializeField] private Button _visitNaturesPrideWebsiteButton;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        /// <summary>
        /// Subscribing to all events.
        /// </summary>
        private void SubscribeEvents()
        {
            _getMoreRecipesButton.onClick.AddListener(OpenGetMoreRecipesLink);
            _watchTheVideoButton.onClick.AddListener(OpenWatchVideoLink);
            _visitAvocadoShowWebsiteButton.onClick.AddListener(OpenAvocadoShowWebsiteButton);
            _visitNaturesPrideWebsiteButton.onClick.AddListener(OpenNaturesPrideWebsiteButton);
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
            _getMoreRecipesButton.onClick.RemoveListener(OpenGetMoreRecipesLink);
            _watchTheVideoButton.onClick.RemoveListener(OpenWatchVideoLink);
            _visitAvocadoShowWebsiteButton.onClick.RemoveListener(OpenAvocadoShowWebsiteButton);
            _visitNaturesPrideWebsiteButton.onClick.RemoveListener(OpenNaturesPrideWebsiteButton);
        }

        private void OpenGetMoreRecipesLink()
        {
            if (MenuPanelController.Instance.CanChangeDishInMenu)
            {
                OpenLink(_getMoreRecipesLink);
            }
        }

        private void OpenWatchVideoLink()
        {
            OpenLink(_watchTheVideoLink);
        }

        private void OpenAvocadoShowWebsiteButton()
        {
            OpenLink(_avocadoShowWebsiteLink);
        }

        private void OpenNaturesPrideWebsiteButton()
        {
            OpenLink(_naturesPrideWebsiteLink);
        }

        private void OpenLink(string link)
        {
            if (!String.IsNullOrEmpty(link))
            {
                Application.OpenURL(link);
            }
            else
            {
                Debug.LogError("Link is empty");
            }
        }
    }
}
