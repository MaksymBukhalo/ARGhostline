using UnityEngine;
using UnityEngine.UI;

namespace FoodStoryTAS
{
    public class SnapshotController : Singleton<SnapshotController>
    {
        [Header("SerializeFields")]
        [SerializeField] private CameraSnapshot _cameraSnapshot;
        [SerializeField] private Animator _snapshotFlash;
        [SerializeField] private string _screenshotFlashAnimationName = "TakeSnapshot";

        [Header("Buttons")]
        [SerializeField] private Button _makeSnapshotForSharingButton;

        /// <summary>
        /// If you take a screenshot and want to use it right away, wait until this property becomes non-null.
        /// After you used the screenshot, set it to null.
        /// </summary>
        public Texture2D CurrentSnapshotForSharing => AdditionalCameraSnapshot.Instance.CurrentSnapshotForSharing;

        /// <summary>
        /// If you take a screenshot and want to use it right away, wait until this property becomes non-null.
        /// After you used the screenshot, set it to null.
        /// </summary>
        public Texture2D CurrentSnapshotForBackground
        {
            get => _cameraSnapshot.CurrentSnapshotForBackground;
            set => _cameraSnapshot.CurrentSnapshotForBackground = value;
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
            _makeSnapshotForSharingButton.onClick.AddListener(TakeSnapshotForSharing);
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
            _makeSnapshotForSharingButton.onClick.RemoveListener(TakeSnapshotForSharing);
        }

        /// <summary>
        /// Make snapshot for sharing(with AR-dish and text) on the next rendered frame and store it in the _cameraSnapshot script.
        /// </summary>
        public void TakeSnapshotForSharing()
        {
            _snapshotFlash.SetTrigger(_screenshotFlashAnimationName);
            _cameraSnapshot.TakeSnapshotForBackground(true);
            //_cameraSnapshot.TakeSnapshotForSharing();
        }

        /// <summary>
        /// Make snapshot for background(without AR-dish and UI) on the next rendered frame and store it in the _cameraSnapshot script.
        /// </summary>
        public void TakeSnapshotForBackground()
        {
            _cameraSnapshot.TakeSnapshotForBackground(false);
        }
    }
}
