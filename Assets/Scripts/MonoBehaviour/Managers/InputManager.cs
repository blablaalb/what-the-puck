using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : Singleton<InputManager>
{
    public event Action<Vector2> Touched;
    public event Action<Vector2> Moved;
    public event Action<Vector2> Left;

    internal void Start()
    {
        TouchInput.Touched += OnTouched;
        TouchInput.Moved += OnMoved;
        TouchInput.Left += OnLeft;

        KeyboardInput.Touched += OnTouched;
        KeyboardInput.Moved += OnMoved;
        KeyboardInput.Left += OnLeft;
    }

    override protected void OnDestroy()
    {
        if (TouchInput.Instance != null)
        {
            TouchInput.Touched -= OnTouched;
            TouchInput.Moved -= OnMoved;
            TouchInput.Left -= OnLeft;
        }
        if (KeyboardInput.Instance != null)
        {
            KeyboardInput.Touched -= OnTouched;
            KeyboardInput.Moved -= OnMoved;
            KeyboardInput.Left -= OnLeft;
        }
        base.OnDestroy();
    }

    private void OnTouched(Vector2 position)
    {
        Touched?.Invoke(position);
    }

    private void OnMoved(Vector2 position)
    {
        Moved?.Invoke(position);
    }

    private void OnLeft(Vector2 position)
    {
        Left?.Invoke(position);
    }
}
