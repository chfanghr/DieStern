using UnityEngine;
using UnityEngine.Events;
using Debug = System.Diagnostics.Debug;

namespace Code.Scripts
{
    public class ScreenSizeManager : MonoBehaviour
    {
        public static float GetScreenToWorldHeight
        {
            get
            {
                Vector2 topRightCorner = new Vector2(1, 1);
                Debug.Assert(Camera.main != null, "Camera.main != null");
                Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
                var height = edgeVector.y * 2;
                return height;
            }
        }

        public static float GetScreenToWorldWidth
        {
            get
            {
                Vector2 topRightCorner = new Vector2(1, 1);
                Debug.Assert(Camera.main != null, "Camera.main != null");
                Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
                var width = edgeVector.x * 2;
                return width;
            }
        }

        public UnityEvent onScreenSizeChanged;

        private int _lastScreenWidth, _lastScreenHeight;

        private void Update()
        {
            if (_lastScreenWidth == Screen.width && _lastScreenHeight == Screen.height) return;
            _lastScreenWidth = Screen.width;
            _lastScreenHeight = Screen.height;
            onScreenSizeChanged.Invoke();
        }
    }
}