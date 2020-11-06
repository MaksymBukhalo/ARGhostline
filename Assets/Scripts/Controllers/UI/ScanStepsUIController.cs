using UnityEngine;
using DG.Tweening;

namespace FoodStoryTAS
{
    public class ScanStepsUIController : Singleton<ScanStepsUIController>, IAppearable
    {
        [SerializeField] private RectTransform _scanIcon;
        [SerializeField] private float _tutorialIconDistanceChanger = 50;

        [Header("Canvas groups")]
        [SerializeField] private CanvasGroup _scanIconGroup;
        [SerializeField] private CanvasGroup _scanPhaseGroup;
        [SerializeField] private CanvasGroup _tapToPlaceDishPhaseGroup;
        [SerializeField] private CanvasGroup _makeSnapshotOrReviewDishesPhaseGroup;
        [SerializeField] private CanvasGroup _sharePhaseGroup;
        //[SerializeField] private CanvasGroup _returnToPreviousPhaseButtonGroup;
        [SerializeField] private CanvasGroup _reloadSceneButtonGroup;
        
        private Sequence _currentScanSequence;
        private Sequence _scanSequence;

        private float _scanIconStartPosX;
        private float _fadingTime = 0.5f;

        public CanvasGroup MakeSnapshotOrReviewDishesPhaseGroup
        {
            get
            {
                return _makeSnapshotOrReviewDishesPhaseGroup;
            }
        }

        private void Start()
        {
            InitializeObjects();
        }

        private void InitializeObjects()
        {
            _scanIconGroup.alpha = 1;
            _scanPhaseGroup.alpha = 1;
            _tapToPlaceDishPhaseGroup.alpha = 0;
            _makeSnapshotOrReviewDishesPhaseGroup.alpha = 0;
            _sharePhaseGroup.alpha = 0;
            //_returnToPreviousPhaseButtonGroup.alpha = 0;
            _reloadSceneButtonGroup.alpha = 0;

            _scanIcon.gameObject.SetActive(true);
            //_returnToPreviousPhaseButtonGroup.gameObject.SetActive(false);
            _reloadSceneButtonGroup.gameObject.SetActive(false);

            _scanIconStartPosX = _scanIcon.transform.localPosition.x;
        }

        /// <summary>
        /// Show scan animation.
        /// </summary>
        public void Show()
        {
            KillSequence(ref _currentScanSequence);
            ShowScanPhaseGroup();

            _currentScanSequence = DOTween.Sequence();
            _currentScanSequence.Append(_scanIcon.DOLocalMoveX(_scanIconStartPosX - _tutorialIconDistanceChanger, 1));
            _currentScanSequence.Append(_scanIcon.DOLocalMoveX(_scanIconStartPosX, 1));
            _currentScanSequence.Append(_scanIcon.DOLocalMoveX(_scanIconStartPosX + _tutorialIconDistanceChanger, 1));
            _currentScanSequence.Append(_scanIcon.DOLocalMoveX(_scanIconStartPosX, 1));
            _currentScanSequence.SetLoops(-1);
        }

        #region Show scan phases

        private void ShowScanPhaseGroup()
        {
            KillSequence(ref _scanSequence);
            _scanSequence = DOTween.Sequence();

            // Hide this
            _scanSequence.Insert(0, _tapToPlaceDishPhaseGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear));
            _scanSequence.Insert(0, _makeSnapshotOrReviewDishesPhaseGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear));
            _scanSequence.Insert(0, _sharePhaseGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear));
            //_scanSequence.Insert(0, _returnToPreviousPhaseButtonGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear)).OnComplete(() =>
            _scanSequence.Insert(0, _reloadSceneButtonGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear)).OnComplete(() =>
            {
                _tapToPlaceDishPhaseGroup.gameObject.SetActive(false);
                _makeSnapshotOrReviewDishesPhaseGroup.gameObject.SetActive(false);
                _sharePhaseGroup.gameObject.SetActive(false);
                //_returnToPreviousPhaseButtonGroup.gameObject.SetActive(false);
                _reloadSceneButtonGroup.gameObject.SetActive(false);
            });

            // Show this
            _scanSequence.Insert(0, _scanIconGroup.DOFade(1, _fadingTime).SetEase(Ease.Linear));
            _scanSequence.Insert(0, _scanPhaseGroup.DOFade(1, _fadingTime).SetEase(Ease.Linear));
            _scanIconGroup.gameObject.SetActive(true);
            _scanPhaseGroup.gameObject.SetActive(true);
        }

        public void ShowTapToPlaceDishPhaseGroup()
        {
            KillSequence(ref _scanSequence);
            _scanSequence = DOTween.Sequence();

            // Hide this
            _scanSequence.Insert(0, _scanPhaseGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear));
            _scanSequence.Insert(0, _makeSnapshotOrReviewDishesPhaseGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear));
            _scanSequence.Insert(0, _sharePhaseGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear)).OnComplete(() =>
            {
                _scanPhaseGroup.gameObject.SetActive(false);
                _makeSnapshotOrReviewDishesPhaseGroup.gameObject.SetActive(false);
                _sharePhaseGroup.gameObject.SetActive(false);
            });

            // Show this
            _scanSequence.Insert(0, _scanIconGroup.DOFade(1, _fadingTime).SetEase(Ease.Linear));
            _scanSequence.Insert(0, _tapToPlaceDishPhaseGroup.DOFade(1, _fadingTime).SetEase(Ease.Linear));
            //_scanSequence.Insert(0, _returnToPreviousPhaseButtonGroup.DOFade(1, _fadingTime).SetEase(Ease.Linear));
            _scanSequence.Insert(0, _reloadSceneButtonGroup.DOFade(1, _fadingTime).SetEase(Ease.Linear));

            _scanIconGroup.gameObject.SetActive(true);
            _tapToPlaceDishPhaseGroup.gameObject.SetActive(true);
            //_returnToPreviousPhaseButtonGroup.gameObject.SetActive(true);
            _reloadSceneButtonGroup.gameObject.SetActive(true);
        }

        public void ShowMakeSnapshotOrReviewDishesPhaseGroup()
        {
            KillSequence(ref _scanSequence);
            _scanSequence = DOTween.Sequence();

            // Hide this
            _scanSequence.Insert(0, _scanIconGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear));
            _scanSequence.Insert(0, _scanPhaseGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear));
            _scanSequence.Insert(0, _tapToPlaceDishPhaseGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear));
            _scanSequence.Insert(0, _sharePhaseGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear)).OnComplete(() =>
            {
                _scanIconGroup.gameObject.SetActive(false);
                _scanPhaseGroup.gameObject.SetActive(false);
                _tapToPlaceDishPhaseGroup.gameObject.SetActive(false);
                _sharePhaseGroup.gameObject.SetActive(false);
            });

            // Show this
            _scanSequence.Insert(0, _makeSnapshotOrReviewDishesPhaseGroup.DOFade(1, _fadingTime).SetEase(Ease.Linear));
            //_scanSequence.Insert(0, _returnToPreviousPhaseButtonGroup.DOFade(1, _fadingTime).SetEase(Ease.Linear));
            _scanSequence.Insert(0, _reloadSceneButtonGroup.DOFade(1, _fadingTime).SetEase(Ease.Linear));

            _makeSnapshotOrReviewDishesPhaseGroup.gameObject.SetActive(true);
            //_returnToPreviousPhaseButtonGroup.gameObject.SetActive(true);
            _reloadSceneButtonGroup.gameObject.SetActive(true);

            _makeSnapshotOrReviewDishesPhaseGroup.interactable = true;
            _makeSnapshotOrReviewDishesPhaseGroup.blocksRaycasts = true;
        }

        public void ShowSharePhaseGroup()
        {
            KillSequence(ref _scanSequence);
            _scanSequence = DOTween.Sequence();

            // Hide this
            _scanSequence.Insert(0, _scanIconGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear));
            _scanSequence.Insert(0, _scanPhaseGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear));
            _scanSequence.Insert(0, _tapToPlaceDishPhaseGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear));
            _scanSequence.Insert(0, _makeSnapshotOrReviewDishesPhaseGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear));
            //_scanSequence.Insert(0, _returnToPreviousPhaseButtonGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear)).OnComplete(() =>
            _scanSequence.Insert(0, _reloadSceneButtonGroup.DOFade(0, _fadingTime).SetEase(Ease.Linear)).OnComplete(() =>
            {
                _scanIconGroup.gameObject.SetActive(false);
                _scanPhaseGroup.gameObject.SetActive(false);
                _tapToPlaceDishPhaseGroup.gameObject.SetActive(false);
                _makeSnapshotOrReviewDishesPhaseGroup.gameObject.SetActive(false);
                //_returnToPreviousPhaseButtonGroup.gameObject.SetActive(false);
                _reloadSceneButtonGroup.gameObject.SetActive(false);
            });

            // Show this
            _scanSequence.Insert(0, _sharePhaseGroup.DOFade(1, 0.5f).SetEase(Ease.Linear));
            _sharePhaseGroup.gameObject.SetActive(true);
        }

        #endregion

        /// <summary>
        /// Hide scan steps animation.
        /// </summary>
        public void Hide()
        {
            KillSequence(ref _scanSequence);
        }

        /// <summary>
        /// Kill animation sequence.
        /// </summary>
        private void KillSequence(ref Sequence sequence)
        {
            if (sequence != null)
            {
                sequence.Kill();
            }
        }
    }
}
