using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace FoodStoryTAS
{
	public class MainMenuManager : MonoBehaviour
	{
        [Header("SerializeFields")]
        [SerializeField] private int _sceneToLoadIndex;

        [SerializeField] private RectTransform _loadingProgressImage;
        [SerializeField] private CanvasGroup _loadingBarGroup;
        [SerializeField] private CanvasGroup _textWhileLoadingGroup;
        [SerializeField] private CanvasGroup _avocadosImageOnCompleteLoadingGroup;

        [Header("Button canvas groups")]
        [SerializeField] private CanvasGroup _startLoadingButtonGroup;
        [SerializeField] private CanvasGroup _allowSceneActivationButtonGroup;

        [Header("Buttons")]
        [SerializeField] private Button _startFakeLoadingButton;
        [SerializeField] private Button _allowSceneActivationButton;

        private AsyncOperation _loadingSceneAsync;
        private Sequence _loadingSequence;

        private float _animationsTime = 0.2f;

        private bool _mainSceneFakeIsLoading = false;

        private float LoadingProgressImageWidth
        {
            get
            {
                return _loadingProgressImage.sizeDelta.x;
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
            _startFakeLoadingButton.onClick.AddListener(StartFakeLoading);
            _allowSceneActivationButton.onClick.AddListener(AllowSceneActivation);
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
            _startFakeLoadingButton.onClick.RemoveListener(StartFakeLoading);
            _allowSceneActivationButton.onClick.RemoveListener(AllowSceneActivation);
        }

        private void Start()
        {
            Init();
            StartToLoadNextSceneAsync();
        }

        private void Init()
        {
            _startLoadingButtonGroup.alpha = 1;
            _loadingBarGroup.interactable = true;
            _loadingBarGroup.blocksRaycasts = true;

            _loadingBarGroup.alpha = 0;
            _loadingBarGroup.interactable = false;
            _loadingBarGroup.blocksRaycasts = false;
            
            _allowSceneActivationButtonGroup.alpha = 0;
            _allowSceneActivationButtonGroup.interactable = false;
            _allowSceneActivationButtonGroup.blocksRaycasts = false;

            _textWhileLoadingGroup.alpha = 0;
            _avocadosImageOnCompleteLoadingGroup.alpha = 0;

            //_loadingProgressImage.gameObject.SetActive(false);
        }

        /// <summary>
        /// Start loading next scene and forbit scene activation when it is loaded.
        /// </summary>
        private void StartToLoadNextSceneAsync()
        {
            _loadingSceneAsync = SceneManager.LoadSceneAsync(_sceneToLoadIndex); // Load next scene in background.
            _loadingSceneAsync.allowSceneActivation = false; // Don't activate the scene automatically on the end of the loading.
        }

        /// <summary>
        /// Animate the fake loading of the next scene.
        /// </summary>
        private void StartFakeLoading()
        {
            if (!_mainSceneFakeIsLoading)
            {
                _mainSceneFakeIsLoading = true;
                //_loadingProgressImage.gameObject.SetActive(true);

                KillSequence();
                _loadingSequence = DOTween.Sequence();

                _loadingSequence.Insert(0, _loadingBarGroup.DOFade(1, _animationsTime).SetEase(Ease.Linear));
                _loadingSequence.Insert(0, _textWhileLoadingGroup.DOFade(1, _animationsTime).SetEase(Ease.Linear));
                _loadingSequence.Insert(0, _startLoadingButtonGroup.DOFade(0, _animationsTime).SetEase(Ease.Linear));

                _startLoadingButtonGroup.interactable = false;
                _startLoadingButtonGroup.blocksRaycasts = false;

                StartCoroutine(FakeLoading());
            }
        }

        private void AllowSceneActivation()
        {
            _loadingSceneAsync.allowSceneActivation = true;
        }

        /// <summary>
        /// Move the loading_progress_image to the right size for loading animation.
        /// </summary>
        /// <returns></returns>
        private IEnumerator FakeLoading()
        {
            /*
             * This part of code move _loadingProgressImage to the right depending on its width.
             * Each time calculate a random distance on which _loadingProgressImage should move to the right,
             * then wait for a random time and repeat this 4 times more.
             */

            float randomDistanceToMove;
            float minTimeToWait = 0.2f;
            float maxTimeToWait = 0.4f;

            yield return new WaitForSeconds(Random.Range(minTimeToWait, maxTimeToWait));

            randomDistanceToMove = Random.Range(LoadingProgressImageWidth / 5, LoadingProgressImageWidth / 6);
            _loadingProgressImage.anchoredPosition = new Vector2(randomDistanceToMove, _loadingProgressImage.anchoredPosition.y); // Set fake level loading.
            yield return new WaitForSeconds(Random.Range(minTimeToWait, maxTimeToWait));

            randomDistanceToMove = Random.Range(LoadingProgressImageWidth / 4, LoadingProgressImageWidth / 5);
            _loadingProgressImage.anchoredPosition = new Vector2(randomDistanceToMove, _loadingProgressImage.anchoredPosition.y); // Set fake level loading.
            yield return new WaitForSeconds(Random.Range(minTimeToWait, maxTimeToWait));

            randomDistanceToMove = Random.Range(LoadingProgressImageWidth / 3, LoadingProgressImageWidth / 4);
            _loadingProgressImage.anchoredPosition = new Vector2(randomDistanceToMove, _loadingProgressImage.anchoredPosition.y); // Set fake level loading.
            yield return new WaitForSeconds(Random.Range(minTimeToWait, maxTimeToWait));

            randomDistanceToMove = Random.Range(LoadingProgressImageWidth / 2, LoadingProgressImageWidth / 3);
            _loadingProgressImage.anchoredPosition = new Vector2(randomDistanceToMove, _loadingProgressImage.anchoredPosition.y); // Set fake level loading.
            yield return new WaitForSeconds(Random.Range(minTimeToWait, maxTimeToWait));

            randomDistanceToMove = Random.Range(LoadingProgressImageWidth / 1, LoadingProgressImageWidth / 2);
            _loadingProgressImage.anchoredPosition = new Vector2(randomDistanceToMove, _loadingProgressImage.anchoredPosition.y); // Set fake level loading.
            yield return new WaitForSeconds(Random.Range(minTimeToWait, maxTimeToWait));

            // In the end there is an animation of objects fading.
            KillSequence();
            _loadingSequence = DOTween.Sequence();

            _loadingSequence.Insert(0, _allowSceneActivationButtonGroup.DOFade(1, _animationsTime).SetEase(Ease.Linear));
            _loadingSequence.Insert(0, _avocadosImageOnCompleteLoadingGroup.DOFade(1, _animationsTime).SetEase(Ease.Linear));
            _loadingSequence.Insert(0, _loadingBarGroup.DOFade(0, _animationsTime).SetEase(Ease.Linear));
            _loadingSequence.Insert(0, _textWhileLoadingGroup.DOFade(0, _animationsTime).SetEase(Ease.Linear));

            _allowSceneActivationButtonGroup.interactable = true;
            _allowSceneActivationButtonGroup.blocksRaycasts = true;

            yield break;
        }

        private void KillSequence()
        {
            if (_loadingSequence != null)
            {
                _loadingSequence.Kill();
            }
        }
    }
}
