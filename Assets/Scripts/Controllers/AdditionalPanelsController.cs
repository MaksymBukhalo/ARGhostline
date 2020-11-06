using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace FoodStoryTAS
{
    public class AdditionalPanelsController : MonoBehaviour
    {
        [Header("SerializeFields")]
        [SerializeField] private CanvasGroup _additionalPanelsGroup;
        [SerializeField] private CanvasGroup _aboutTheAvocadoShowPanelGroup;
        [SerializeField] private CanvasGroup _aboutNaturesPridePanelGroup;
        [SerializeField] private CanvasGroup _watchTheVideoButton;

        [Header("Text containers")]
        [SerializeField] private Transform _aboutAvocadoShowText;
        [SerializeField] private Transform _aboutNaturesPrideText;

        [Header("Buttons")]
        [SerializeField] private Button _openAboutTheAvocadoShowPanel;
        [SerializeField] private Button _openAboutNaturesPridePanel;
        [SerializeField] private Button _closeAdditionalPanelsButton;

        private Sequence _openAdditionalPanelsSequence;

        private float _additionalPanelsAppearingTime = 0.5f;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        /// <summary>
        /// Subscribing to all events.
        /// </summary>
        private void SubscribeEvents()
        {
            _openAboutTheAvocadoShowPanel.onClick.AddListener(OpenAboutTheAvocadoShowPanel);
            _openAboutNaturesPridePanel.onClick.AddListener(OpenAboutNaturesPridePanel);
            _closeAdditionalPanelsButton.onClick.AddListener(CloseAdditionalPanels);
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
            _openAboutTheAvocadoShowPanel.onClick.RemoveListener(OpenAboutTheAvocadoShowPanel);
            _openAboutNaturesPridePanel.onClick.RemoveListener(OpenAboutNaturesPridePanel);
            _closeAdditionalPanelsButton.onClick.RemoveListener(CloseAdditionalPanels);
        }

        private void Start()
        {
            InitializeObjects();
        }

        private void InitializeObjects()
        {
            _additionalPanelsGroup.alpha = 0;
            _aboutTheAvocadoShowPanelGroup.alpha = 0;
            _aboutNaturesPridePanelGroup.alpha = 0;
            _watchTheVideoButton.alpha = 0;

            _additionalPanelsGroup.gameObject.SetActive(false);
            _aboutTheAvocadoShowPanelGroup.gameObject.SetActive(false);
            _aboutNaturesPridePanelGroup.gameObject.SetActive(false);
            _watchTheVideoButton.gameObject.SetActive(false);
        }

        /// <summary>
        /// Hides about_natures_pride panel. Also show additional panels and about_the_avocado_show panel.
        /// </summary>
        private void OpenAboutTheAvocadoShowPanel()
        {
            if (!MenuPanelController.Instance.CanChangeDishInMenu)
            {
                return;
            }
            
            KillSequence();
            _openAdditionalPanelsSequence = DOTween.Sequence();
            
            _openAdditionalPanelsSequence.Insert(0, _watchTheVideoButton.DOFade(1, _additionalPanelsAppearingTime).SetEase(Ease.Linear));
            _openAdditionalPanelsSequence.Insert(0, _additionalPanelsGroup.DOFade(1, _additionalPanelsAppearingTime).SetEase(Ease.Linear));
            _openAdditionalPanelsSequence.Insert(0, _aboutTheAvocadoShowPanelGroup.DOFade(1, _additionalPanelsAppearingTime).SetEase(Ease.Linear));
            _openAdditionalPanelsSequence.Insert(0, _aboutNaturesPridePanelGroup.DOFade(0, _additionalPanelsAppearingTime).SetEase(Ease.Linear)).OnComplete(() =>
            {
                _aboutNaturesPridePanelGroup.gameObject.SetActive(false);
            });
            _additionalPanelsGroup.gameObject.SetActive(true);
            _watchTheVideoButton.gameObject.SetActive(true);
            _aboutTheAvocadoShowPanelGroup.gameObject.SetActive(true);

            _aboutAvocadoShowText.localPosition = Vector3.zero;
        }

        /// <summary>
        /// Hides about_the_avocado_show panel. Also show additional panels and about_natures_pride panel.
        /// </summary>
        private void OpenAboutNaturesPridePanel()
        {
            if (!MenuPanelController.Instance.CanChangeDishInMenu)
            {
                return;
            }

            KillSequence();
            _openAdditionalPanelsSequence = DOTween.Sequence();

            _openAdditionalPanelsSequence.Insert(0, _watchTheVideoButton.DOFade(1, _additionalPanelsAppearingTime).SetEase(Ease.Linear));
            _openAdditionalPanelsSequence.Insert(0, _additionalPanelsGroup.DOFade(1, _additionalPanelsAppearingTime).SetEase(Ease.Linear));
            _openAdditionalPanelsSequence.Insert(0, _aboutNaturesPridePanelGroup.DOFade(1, _additionalPanelsAppearingTime).SetEase(Ease.Linear));
            _openAdditionalPanelsSequence.Insert(0, _aboutTheAvocadoShowPanelGroup.DOFade(0, _additionalPanelsAppearingTime).SetEase(Ease.Linear)).OnComplete(() =>
            {
                _aboutTheAvocadoShowPanelGroup.gameObject.SetActive(false);
            });
            _watchTheVideoButton.gameObject.SetActive(true);
            _additionalPanelsGroup.gameObject.SetActive(true);
            _aboutNaturesPridePanelGroup.gameObject.SetActive(true);

            _aboutNaturesPrideText.localPosition = Vector3.zero;
        }

        /// <summary>
        /// Hides all additional panels.
        /// </summary>
        private void CloseAdditionalPanels()
        {
            KillSequence();
            _openAdditionalPanelsSequence = DOTween.Sequence();

            _openAdditionalPanelsSequence.Insert(0, _watchTheVideoButton.DOFade(0, _additionalPanelsAppearingTime).SetEase(Ease.Linear));
            _openAdditionalPanelsSequence.Insert(0, _additionalPanelsGroup.DOFade(0, _additionalPanelsAppearingTime).SetEase(Ease.Linear));
            _openAdditionalPanelsSequence.Insert(0, _aboutTheAvocadoShowPanelGroup.DOFade(0, _additionalPanelsAppearingTime).SetEase(Ease.Linear));
            _openAdditionalPanelsSequence.Insert(0, _aboutNaturesPridePanelGroup.DOFade(0, _additionalPanelsAppearingTime).SetEase(Ease.Linear)).OnComplete(() =>
            {
                _watchTheVideoButton.gameObject.SetActive(false);
                _additionalPanelsGroup.gameObject.SetActive(false);
                _aboutTheAvocadoShowPanelGroup.gameObject.SetActive(false);
                _aboutNaturesPridePanelGroup.gameObject.SetActive(false);
            });
        }

        private void KillSequence()
        {
            if (_openAdditionalPanelsSequence != null)
            {
                _openAdditionalPanelsSequence.Kill();
            }
        }
    }
}
