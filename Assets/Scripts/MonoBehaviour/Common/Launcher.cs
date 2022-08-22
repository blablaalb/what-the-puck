using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Launcher : Singleton<Launcher>
{
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField]
    private Image _swipeSize;
    private float _distanceTreshold = 1f;
    private bool _simulate;
    private Vector2 _startPosition;
    private Vector3 _puckPosition;
    private Vector2 _currentPosition;
    private Vector2 _leftPosition;
    [SerializeField, Range(0f, 100f)]
    private float _forceSpeed = 10f;
    [SerializeField]
    private ForceMode _forceMode;
    private Vector3 _force;

    public float ForceSpeed => _forceSpeed;
    public ForceMode ForceMode => _forceMode;

    protected override void Awake()
    {
        base.Awake();
        _distanceTreshold = _swipeSize.rectTransform.rect.y;
        _distanceTreshold = Mathf.Abs(_distanceTreshold);
    }

    internal void Start()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.Touched += OnTouched;
            InputManager.Instance.Moved += OnMoved;
            InputManager.Instance.Left += OnLeft;
        }
    }

    override protected void OnDestroy()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.Touched -= OnTouched;
            InputManager.Instance.Moved -= OnMoved;
            InputManager.Instance.Left -= OnLeft;
        }
        base.OnDestroy();
    }

    private void OnTouched(Vector2 screenPosition)
    {
        RaycastHit hit;
        if (TruRaycastFromScreenPosition(screenPosition, out hit))
        {
            if (hit.collider.gameObject.GetComponent<Puck>() is Puck puck)
            {
                if (!_simulate)
                {
                    _simulate = true;
                    puck.Stop();
                    _puckPosition = puck.Position;
                    _startPosition = screenPosition;
                    _currentPosition = screenPosition;
                    // Debug.Log("<color=green>Touched</color>");
                }
            }
        }
    }

    private void OnMoved(Vector2 screenPosition)
    {
        if (_simulate)
        {
            _currentPosition = screenPosition;
            Vector2 normalizedDirection = (_startPosition - _currentPosition).normalized;
            Vector3 force = new Vector3(normalizedDirection.x, 0f, normalizedDirection.y) * (_forceSpeed * GetDistancePercentage());
            _force = force;
            Simulate();
        }
    }

    private void OnLeft(Vector2 screenPosition)
    {
        RealPuck realPuck = RealPuckManager.Instance.GetRealPuck();
        if (_simulate)
        {
            _currentPosition = screenPosition;
            _simulate = false;
            Simulate();
            realPuck.AddForce(_force, _forceMode);
            _leftPosition = screenPosition;
            // Debug.Log($"<color=red>Left</color>");
        }
    }

    /// <summary>
    /// Returns percentage of user's input distance relative to the distance treshold value.
    /// </summary>
    /// <returns>Percentage of the maximum speed.</returns>
    private float GetDistancePercentage()
    {
        float percentage = 0f;
        float distance = Vector2.Distance(_currentPosition, _startPosition);
        distance = Mathf.Clamp(distance, 0f, _distanceTreshold);
        percentage = distance / _distanceTreshold;
        return percentage;
    }

    private void Simulate()
    {
        // PhysicsSimulator.Instance.Simulate(_puckPosition,_force);
        PhysicsSimulator.Instance.Simulate(_force,_forceMode);
    }

    /// <summary>
    /// Returns true and raycasthit variable if the ray hit something from the provided screen position. False otherwsie.
    /// </summary>
    /// <param name="screenPosition">Screen position from where the ray should be thrown,</param>
    /// <param name="raycastHit">Results of casting the ray.</param>
    /// <returns></returns>
    private bool TruRaycastFromScreenPosition(Vector2 screenPosition, out RaycastHit raycastHit)
    {
        bool hit = false;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(screenPosition.x, screenPosition.y, 0f));
        if (Physics.Raycast(ray, out raycastHit, 1000f, _layerMask))
        {
            hit = true;
        }
        return hit;
    }
}
