using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FoodStoryTAS
{
    public class AdditionalCameraSnapshot : Singleton<AdditionalCameraSnapshot>
    {
        [SerializeField] private RawImage _rawImage;

        private AudioSource _snapshotSound;

        public Texture2D CurrentSnapshotForSharing { get; private set; } = null;

        private bool _canTakeScreenshot = false;

        protected override void Awake()
        {
            base.Awake();

            _snapshotSound = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _rawImage.texture = null;
        }

        private void OnPostRender()
        {
            if (_canTakeScreenshot)
            {
                ScreenshotForSharing();
            }
        }

        public void TakeSnapshotForSharing(Texture2D screenshotForBackground)
        {
            CurrentSnapshotForSharing = null;
            _rawImage.texture = screenshotForBackground;
            _canTakeScreenshot = true;
        }

        private void ScreenshotForSharing()
        {
            _snapshotSound.Play();

            _canTakeScreenshot = false;

            CurrentSnapshotForSharing = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, Screen.width, Screen.height);
            CurrentSnapshotForSharing.ReadPixels(rect, 0, 0, false);
            CurrentSnapshotForSharing.Apply();

            //byte[] byteArray = CurrentSnapshotForSharing.EncodeToPNG();
            //System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot1.png", byteArray);
            //Debug.Log("Saved CameraScreenshot1.png");

            ScanStepsController.Instance.DoScanning(ScanSteps.Share);
            ScanStepsUIController.Instance.MakeSnapshotOrReviewDishesPhaseGroup.interactable = false;
            ScanStepsUIController.Instance.MakeSnapshotOrReviewDishesPhaseGroup.blocksRaycasts = false;

            _rawImage.texture = null;
        }
    }
}
