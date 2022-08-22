using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This script is responsible for moving the puck around the board with touch input or mouse.
/// Since they want to make the puck lanchable we changed to the Launcher script.
/// </summary>
public class PuckTouchMoveController : MonoBehaviour
{
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    [SerializeField, Range(0f, 10f)]
    private float _maxForce = 4.5f;

    internal void Start()
    {
        InputManager.Instance.Touched += OnPlayerTouched;
        InputManager.Instance.Moved += OnPlayerMoved;
        InputManager.Instance.Left += OnPlayerLeftInput;
    }

    internal void OnDestroy()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.Touched -= OnPlayerTouched;
            InputManager.Instance.Moved -= OnPlayerMoved;
            InputManager.Instance.Left -= OnPlayerLeftInput;
        }
    }

    private void OnPlayerTouched(Vector2 position)
    {
        _startPosition = position;
    }

    private void OnPlayerMoved(Vector2 position)
    {
    }

    private void OnPlayerLeftInput(Vector2 position)
    {
        RealPuck realPuck = RealPuckManager.Instance.GetRealPuck();
        _endPosition = position;
        Vector2 direction = _endPosition - _startPosition;
        Vector3 forceDirection = new Vector3(direction.x, 0f, direction.y);
        float force = Vector2.Distance(_endPosition, _startPosition) * Time.fixedDeltaTime;
        force = Mathf.Clamp(force, 0f, _maxForce);
        realPuck.AddForce(forceDirection.normalized * force, ForceMode.Impulse);
    }
}
