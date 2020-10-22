using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Scripts.UI
{
    public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Vector2 _lastMousePosition;

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Begin Drag");
            _lastMousePosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var currentMousePosition = eventData.position;
            var diff = currentMousePosition - _lastMousePosition;
            var rect = GetComponent<RectTransform>();
            var position = rect.position;
            var newPosition = position + new Vector3(diff.x, diff.y, transform.position.z);
            var oldPos = position;
            position = newPosition;
            rect.position = position;
            if (!IsRectTransformInsideSreen(rect)) rect.position = oldPos;

            _lastMousePosition = currentMousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("End Drag");
        }

        private bool IsRectTransformInsideSreen(RectTransform rectTransform)
        {
            var isInside = false;
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            var rect = new Rect(0, 0, Screen.width, Screen.height);
            var visibleCorners = corners.Count(corner => rect.Contains(corner));
            if (visibleCorners == 4) isInside = true;
            return isInside;
        }
    }
}