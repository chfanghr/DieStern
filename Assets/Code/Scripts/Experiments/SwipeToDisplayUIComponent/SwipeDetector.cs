using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Code.Scripts.Experiments.SwipeToDisplayUIComponent
{
  public class SwipeDetector : MonoBehaviour {
    public float swipeThreshold = 50f;
    public float timeThreshold = 0.3f;

    public UnityEvent onSwipeLeft;
    public UnityEvent onSwipeRight;
    public UnityEvent onSwipeUp;
    public UnityEvent onSwipeDown;

    private Vector2 _fingerDown;
    private DateTime _fingerDownTime;
    private Vector2 _fingerUp;
    private DateTime _fingerUpTime;

    private void Update () {
      if (Input.GetMouseButtonDown(0)) {
        _fingerDown = Input.mousePosition;
        _fingerUp = Input.mousePosition;
        _fingerDownTime = DateTime.Now;
      }
      if (Input.GetMouseButtonUp(0)) {
        _fingerDown = Input.mousePosition;
        _fingerUpTime = DateTime.Now;
        CheckSwipe();
      }

      var fingerOnGameObject = false;
      var touches = Input.touches;
      
      foreach (var touch in touches)
      {
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
          fingerOnGameObject = true;
        }
      }

      if (!fingerOnGameObject)
      {
        return;
      }
      
      foreach (var touch in touches)
      {
        switch (touch.phase)
        {
          case TouchPhase.Began:
            _fingerDown = touch.position;
            _fingerUp = touch.position;
            _fingerDownTime = DateTime.Now;
            break;
          case TouchPhase.Ended:
            _fingerDown = touch.position;
            _fingerUpTime = DateTime.Now;
            CheckSwipe();
            break;
          case TouchPhase.Moved:
            break;
          case TouchPhase.Stationary:
            break;
          case TouchPhase.Canceled:
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    private void CheckSwipe() {
      var duration = (float)_fingerUpTime.Subtract(_fingerDownTime).TotalSeconds;
      if (duration > timeThreshold) return;

      var deltaX = _fingerDown.x - _fingerUp.x;
      if (Mathf.Abs(deltaX) > swipeThreshold) {
        if (deltaX > 0) {
          onSwipeRight.Invoke();
          Debug.Log("right");
        } else if (deltaX < 0) {
          onSwipeLeft.Invoke();
          Debug.Log("left");
        }
      }

      var deltaY = _fingerDown.y - _fingerUp.y;
      if (Mathf.Abs(deltaY) > swipeThreshold) {
        if (deltaY > 0) {
          onSwipeUp.Invoke();
          Debug.Log("up");
        } else if (deltaY < 0) {
          onSwipeDown.Invoke();
          Debug.Log("down");
        }
      }

      _fingerUp = _fingerDown;
    }
  }
}