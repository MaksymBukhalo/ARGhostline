using UnityEngine;
using UnityEngine.UI;
using VoxelBusters.NativePlugins;

namespace FoodStoryTAS
{
    public class ShareController : MonoBehaviour
    {
        [SerializeField] private Button _shareButton;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        /// <summary>
        /// Subscribing to all events.
        /// </summary>
        private void SubscribeEvents()
        {
            _shareButton.onClick.AddListener(ShareSnapshot);
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
            _shareButton.onClick.RemoveListener(ShareSnapshot);
        }

        /// <summary>
        /// This method called by "Share" button.
        /// </summary>
        private void ShareSnapshot()
        {
            if (SnapshotController.Instance.CurrentSnapshotForSharing)
            {
                // NatShare crashes on some devices
                //NatShareU.NatShare.Share(SnapshotController.Instance.CurrentSnapshotForSharing);

                ShareSheet socialShareSheet = new ShareSheet();
                socialShareSheet.AttachImage(SnapshotController.Instance.CurrentSnapshotForSharing);
                NPBinding.UI.SetPopoverPointAtLastTouchPosition();
				NPBinding.Sharing.ShowView(socialShareSheet, FinishSharing);
			}
            else
            {
                Debug.LogError("No screenshot was taken. First call the screenshot taking method.");
            }
        }

        private void FinishSharing(eShareResult shareResult)
        {
            Debug.Log(shareResult);
        }
    }
}
