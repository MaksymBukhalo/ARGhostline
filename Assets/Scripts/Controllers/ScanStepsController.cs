using HedgehogTeam.EasyTouch;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace FoodStoryTAS
{
    public class ScanStepsController : Singleton<ScanStepsController>
    {
        [SerializeField] private ARPlaneManager _planeManager;
        [SerializeField] private ScanStepsUIController _uiController;
        //[SerializeField] private Button _returnToPreviousPhaseButton;
        [SerializeField] private Button _closeSharePhaseButton;

        private IEnumerator _scanningCoroutine = null;
        private IEnumerator _tapToPlaceDishCoroutine = null;
        public ScanSteps CurrentScanStep;

        private bool _isScanStepComplete = false;
        private bool _isTapToPlaceDishStepComplete = false;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        /// <summary>
        /// Subscribing to all events.
        /// </summary>
        private void SubscribeEvents()
        {
            _planeManager.planesChanged += OnPlaneAdded;
            _planeManager.planesChanged += OnPlaneUpdated;
            _planeManager.planesChanged += OnPlaneRemoved;
            EasyTouch.On_TouchDown += OnTap;
            //_returnToPreviousPhaseButton.onClick.AddListener(ReturnToPreviousPhase);
            _closeSharePhaseButton.onClick.AddListener(ReturnToPreviousPhase);
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
            _planeManager.planesChanged -= OnPlaneAdded;
            _planeManager.planesChanged -= OnPlaneUpdated;
            _planeManager.planesChanged -= OnPlaneRemoved;
            EasyTouch.On_TouchDown -= OnTap;
            //_returnToPreviousPhaseButton.onClick.RemoveListener(ReturnToPreviousPhase);
            _closeSharePhaseButton.onClick.RemoveListener(ReturnToPreviousPhase);
        }

        private void Start()
        {
            StartScanning();
        }

        public void ReturnToPreviousPhase()
        {
            int previousScanStepIndex = (int)CurrentScanStep - 1;

            if (previousScanStepIndex < 0)
            {
                return;
            }

            DoScanning((ScanSteps)previousScanStepIndex);
        }

        public void DoScanning(ScanSteps scanStep)
        {
            CurrentScanStep = scanStep;

            switch (CurrentScanStep)
            {
                case ScanSteps.ScanInactive:
                case ScanSteps.Scan:
                    DoScanPhase();
                    break;
                case ScanSteps.TapToPlaceDish:
                    DoTapToPlaceDishPhase();
                    break;
                case ScanSteps.MakeSnapshotOrReviewDishes:
                    DoMakeSnapshotOrReviewDishesPhase();
                    break;
                case ScanSteps.Share:
                    DoSharePhase();
                    break;
            }
        }

        private void DoScanPhase()
        {
            CurrentScanStep = ScanSteps.Scan;
            if (_scanningCoroutine != null)
            {
                StopCoroutine(_scanningCoroutine);
                _scanningCoroutine = null;
            }
            if (_tapToPlaceDishCoroutine != null)
            {
                StopCoroutine(_tapToPlaceDishCoroutine);
                _tapToPlaceDishCoroutine = null;
            }

            _isScanStepComplete = false;
            _isTapToPlaceDishStepComplete = false;

            StartScanning();
            MenuSelectionController.Instance.DeselectCurrentMenuItem();
            PlacePointVisualizer.Instance.SetActiveVisualPoint(true);
            ObjectsContainerPlacer.Instance.IsObjectAlreadyPlaced = false;
        }

        private void DoTapToPlaceDishPhase()
        {
            CurrentScanStep = ScanSteps.TapToPlaceDish;
            _isTapToPlaceDishStepComplete = false;

            ShowTapToPlaceDish();
            MenuSelectionController.Instance.DeselectCurrentMenuItem();
            PlacePointVisualizer.Instance.SetActiveVisualPoint(true);
            ObjectsContainerPlacer.Instance.IsObjectAlreadyPlaced = false;
        }

        private void DoMakeSnapshotOrReviewDishesPhase()
        {
            CurrentScanStep = ScanSteps.MakeSnapshotOrReviewDishes;
            _uiController.ShowMakeSnapshotOrReviewDishesPhaseGroup();
        }

        private void DoSharePhase()
        {
            CurrentScanStep = ScanSteps.Share;
            _uiController.ShowSharePhaseGroup();
        }

        /// <summary>
        /// Called when place has been detected by pointer.
        /// </summary>
        /// <param name="obj"></param>
        private void OnPlaneAdded(ARPlanesChangedEventArgs obj)
        {
            if (_isScanStepComplete)
            {
                return;
            }

            _isScanStepComplete = true;
        }

        /// <summary>
        /// Called when place has been updated by pointer.
        /// </summary>
        /// <param name="obj"></param>
        private void OnPlaneUpdated(ARPlanesChangedEventArgs obj)
        {
            if (_isScanStepComplete)
            {
                return;
            }

            _isScanStepComplete = true;
        }

        /// <summary>
        /// Called when place has been removed by pointer.
        /// </summary>
        /// <param name="obj"></param>
        private void OnPlaneRemoved(ARPlanesChangedEventArgs obj)
        {
            if (!_isScanStepComplete)
            {
                return;
            }

            _isScanStepComplete = false;
        }

        /// <summary>
        /// Called when user tap on screen.
        /// </summary>
        private void OnTap(Gesture gesture)
        {
            if (!_isScanStepComplete || _isTapToPlaceDishStepComplete)
            {
                return;
            }

            _isTapToPlaceDishStepComplete = true;
        }

        /// <summary>
        /// Show scanning for user.
        /// </summary>
        private void StartScanning()
        {
            if (_scanningCoroutine != null)
            {
                StopCoroutine(_scanningCoroutine);
                _scanningCoroutine = null;
                CurrentScanStep = ScanSteps.ScanInactive;
            }

            _scanningCoroutine = ShowScanAction();
            StartCoroutine(_scanningCoroutine);
        }

        /// <summary>
        /// Show tap to place dish for user.
        /// </summary>
        private void ShowTapToPlaceDish()
        {
            if (_tapToPlaceDishCoroutine != null)
            {
                StopCoroutine(_tapToPlaceDishCoroutine);
                _tapToPlaceDishCoroutine = null;
                CurrentScanStep = ScanSteps.Scan;
            }

            _tapToPlaceDishCoroutine = ShowTapToPlaceDishAction();
            StartCoroutine(_tapToPlaceDishCoroutine);
        }

        /// <summary>
        /// Actions in scanning phases.
        /// </summary>
        private IEnumerator ShowScanAction()
        {
            CurrentScanStep = ScanSteps.Scan;
            MenuSelectionController.Instance.TurnOffSelectionMode();

            // Show scanning
            _uiController.Show();
            // Waiting for place detecting.
            yield return new WaitUntil(() => _isScanStepComplete);

            //yield return new WaitForSeconds(2f);

            DoTapToPlaceDishPhase();

            yield break;
        }

        private IEnumerator ShowTapToPlaceDishAction()
        {
            CurrentScanStep = ScanSteps.TapToPlaceDish;
            _uiController.ShowTapToPlaceDishPhaseGroup();
            // Waiting for user tap on screen.
            yield return new WaitUntil(() => _isTapToPlaceDishStepComplete);
            MenuSelectionController.Instance.TurnOnSelectionMode();
            MenuSelectionController.Instance.SelectDefaultDish();

            // Hide tutorial.
            //_uiController.Hide();

            //yield return new WaitForSeconds(2f);
            CurrentScanStep = ScanSteps.MakeSnapshotOrReviewDishes;

            DoMakeSnapshotOrReviewDishesPhase();

            yield break;

            //if (_scanningCoroutine != null)
            //{
            //    StopCoroutine(_scanningCoroutine);
            //}
        }
    }
}
