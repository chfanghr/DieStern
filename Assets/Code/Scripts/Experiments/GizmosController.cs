using UnityEngine;

namespace Code.Scripts.Experiments
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class GizmosController : MonoBehaviour
    {
        private Vector3 _screenPoint;
        private Vector3 _offset;

        private static Camera Camera => Camera.main;
        
        private void OnMouseDown()
        {
            var position = gameObject.transform.position;
            _screenPoint = Camera.WorldToScreenPoint(position);
            _offset = position - Camera.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
        }

        private void OnMouseDrag()
        {
            var curScreenPoint = 
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
 
            var curPosition = Camera.ScreenToWorldPoint(curScreenPoint) + _offset;
            transform.position = curPosition;
 
        }
    }
}