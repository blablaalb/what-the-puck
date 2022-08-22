using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchInput : Singleton<TouchInput>
{
    private Vector2 _startPosition;
    private Vector2 _currentPosition;
    private Vector2 _deltaPosition;
    // Max X is the half of the child image
    private float _maxX;
    // Min x is the negative of the max x
    private float _minX;
    private float _startTime;
    private float _endTime;
    [SerializeField, Range(0f, 5f)]
    private float _swipeTimeTreshold = 1f;
    public float minOffset;


    /// <summary>
    /// Called when player touches the screen. Provides position of the screen.
    /// </summary>
    public static event Action<Vector2> Touched;
    public static event Action<Vector2> Moved;
    public static event Action<Vector2> Left;
    /// <summary>
    /// Called when player swiped on the screen (touch + swipe + pull finger up). Provides swipe speed.
    /// </summary>
    public event Action<SwipeDirection> Swiped;

    protected override void Awake()
    {
        base.Awake();
        Image img = transform.GetChild(0).GetComponent<Image>();
        float x = img.rectTransform.rect.width;
        // considering we start to scroll from the center of the image we should divide width by two
        _maxX = x / 2f;
        // subtracting a value from zero gives us negative of the value
        _minX = 0 - _maxX;
        // Debug.Log($"Min X: {_minX} Max X: {_maxX}");
    }

    public float GetXAxis()
    {
        float x = 0f;
        x = _deltaPosition.x;
        x = Mathf.Clamp(x, _minX, _maxX);
        x /= _maxX;
        return x;
    }

    internal void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch firstTouch = Input.GetTouch(0);
            _currentPosition = firstTouch.position;
            _deltaPosition = _currentPosition - _startPosition;


            switch (firstTouch.phase)
            {
                case TouchPhase.Began:
                    _startTime = Time.time;
                    _startPosition = firstTouch.position;
                    Touched?.Invoke(_startPosition);
                    break;
                case TouchPhase.Moved:
                    _currentPosition = firstTouch.position;
                    if (_deltaPosition.magnitude > minOffset)
                        Moved?.Invoke(_currentPosition);
                    break;
                case TouchPhase.Stationary:
                    // _startPosition = _currentPosition;
                    break;
                case TouchPhase.Ended:
                    _endTime = Time.time;
                    if (_deltaPosition.magnitude > minOffset)
                        Left?.Invoke(firstTouch.position);
                    OnTouchEnded();
                    ResetValues();
                    break;
                case TouchPhase.Canceled:
                    ResetValues();
                    break;
            }

        }
    }

    private void ResetValues()
    {
        _startPosition = Vector2.zero;
        _currentPosition = Vector2.zero;
        _deltaPosition = Vector2.zero;
    }

    private void OnTouchEnded()
    {
        float swipeTime = _endTime - _startTime;
        if (Mathf.Abs(_deltaPosition.x) >= _maxX)
        {
            if (swipeTime < _swipeTimeTreshold)
            {
                SwipeDirection direction = _deltaPosition.x > 0.00f ? SwipeDirection.Right : SwipeDirection.Left;
                Swiped?.Invoke(direction);
            }
        }
    }
}

public enum SwipeDirection
{
    Left,
    Right
}
