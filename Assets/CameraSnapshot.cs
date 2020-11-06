using UnityEngine;
using UnityEngine.UI;

namespace FoodStoryTAS
{
    /// <summary>
    /// Makes and stores screenshots. Requires a camera on this GameObject.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class CameraSnapshot : Singleton<CameraSnapshot>
    {
        [SerializeField] private Image _snapshotPreviewImage;

        [HideInInspector] public Texture2D CurrentSnapshotForBackground;

        private bool _takeScreenshotOnTheNextFrame = false;
        private bool _takeScreenshotForSharing = false;

        /// <summary>
        /// MonoBehaviour method. It needs camera on this game object.
        /// </summary>
        private void OnPostRender()
        {
            if (_takeScreenshotOnTheNextFrame)
            {
                ScreenshotForBackground();
                return;
            }
        }

        /// <summary>
        /// This method called when open menu panel.
        /// </summary>
        public void TakeSnapshotForBackground(bool takeScreenshotForSharing)
        {
            _takeScreenshotOnTheNextFrame = true;
            _takeScreenshotForSharing = takeScreenshotForSharing;
        }

        /// <summary>
        /// This method called by "Take snapshot" button.
        /// </summary>
        //public void TakeSnapshotForSharing(bool takeScreenshotForSharing)
        //{
        //    _takeScreenshotForSharing = takeScreenshotForSharing;
        //    _takeScreenshotOnTheNextFrame = true;
        //}

        private void ScreenshotForBackground()
        {
            _takeScreenshotOnTheNextFrame = false;

            CurrentSnapshotForBackground = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, Screen.width, Screen.height);
            CurrentSnapshotForBackground.ReadPixels(rect, 0, 0, false);
            CurrentSnapshotForBackground.Apply();

            _snapshotPreviewImage.color = Color.white;
            Sprite sprite = Sprite.Create(CurrentSnapshotForBackground, new Rect(0, 0, Screen.width, Screen.height), Vector2.zero, 144);
            _snapshotPreviewImage.sprite = sprite;

            //byte[] byteArray = CurrentSnapshotForBackground.EncodeToPNG();
            //System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot2.png", byteArray);
            //Debug.Log("Saved CameraScreenshot2.png");

            if (_takeScreenshotForSharing)
            {
                _takeScreenshotForSharing = false;
                AdditionalCameraSnapshot.Instance.TakeSnapshotForSharing(CurrentSnapshotForBackground);
            }
        }
    }
}
