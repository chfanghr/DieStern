using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Scripts.UI.Inventory
{
    [ExecuteAlways]
    public class Item : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        public Sprite icon;
        public int count;
        public Sprite defaultIcon;
        public bool maskOn = false;
        
        private GameObject _count;
        private Text _countNumber;
        private Image _icon;
        private GameObject _shortcut;
        private Text _shortcutText;
        private GameObject _mask;
        private RectTransform _rectTransform;
        
        private void Start() => Init();

        private void OnEnable() => Init();

        private void Init()
        {
            _count = transform.Find("Count").gameObject;
            _countNumber = _count.transform.Find("Number").GetComponent<Text>();
            _icon = transform.Find("Icon").GetComponent<Image>();
            _icon.sprite = icon == null? defaultIcon:icon;
            _shortcut = transform.Find("Shortcut").gameObject;
            _shortcutText = _shortcut.GetComponent<Text>();
            _mask = transform.Find("Mask").gameObject;
            _rectTransform = GetComponent<RectTransform>();
            
            if (Regex.IsMatch(name, @"^Item\d$"))
            {
                _shortcut.SetActive(true);
                _shortcutText.text = name[4].ToString();
            }
            else
            {
                _shortcut.SetActive(false);
            }
        }

        private void Update()
        {
            _icon.sprite = icon == null? defaultIcon:icon;
            _countNumber.text = count.ToString();
            _mask.SetActive(maskOn);
            _shortcut.SetActive(true);
            
            switch (count)
            {
                case 0:
                    _mask.SetActive(false);
                    _count.SetActive(false);
                    _shortcut.SetActive(false);
                    _icon.sprite = defaultIcon;
                    return;
                case 1:
                    _count.SetActive(false);
                    return;
            }
            
            _count.SetActive(true);
        }

        private Vector3 _beginPosition;
        private Vector3 _lastPosition;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _beginPosition = _lastPosition = _rectTransform.position;
        }

        private bool _shouldPlaceBack = false;
        private void FixedUpdate()
        {
            if(_shouldPlaceBack){
                _rectTransform.position = _beginPosition;
            }

            _shouldPlaceBack = false;
        }

        private Vector2 _lastMousePosition;
        
        public void OnDrag(PointerEventData eventData)
        {
            var currentMousePosition = eventData.position;
            var diff = currentMousePosition - _lastMousePosition;
            var position = _rectTransform.position;
            var newPosition = position + new Vector3(diff.x, diff.y, transform.position.z);
            var oldPos = position;
            position = newPosition;
            _rectTransform.position = position;
            if (!IsRectTransformInsideSreen(_rectTransform))
                _rectTransform.position = oldPos;

            _lastMousePosition = currentMousePosition;
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

        public void OnDrop(PointerEventData eventData)
        {
            var draggingObject = eventData.pointerDrag;
            if(draggingObject!=null){
                var item = draggingObject.GetComponent<Item>();
                if (item != null)
                {
                    Common.Swap(ref item.icon, ref icon);
                    Common.Swap(ref item.count, ref count);

                    item.Update();
                    Update();
                }
            }
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            _shouldPlaceBack = true;
        }
    }
}