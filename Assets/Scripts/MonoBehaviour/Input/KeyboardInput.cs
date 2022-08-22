using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyboardInput : Singleton<KeyboardInput>
{
    public static event Action<Vector2> Touched;
    public static event Action<Vector2> Moved;
    public static event Action<Vector2> Left;

    // [Range(10f, 5f)]
    public float minOffset;

    private Vector2 _startPoint = Vector2.zero;
    private Vector2 _endPoint = Vector2.zero;

    internal void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Touched?.Invoke(Input.mousePosition);
            _startPoint = Input.mousePosition;
        }
        else if (_startPoint != Vector2.zero)
        {
            _endPoint = Input.mousePosition;
            var dist = Vector3.Distance(_startPoint, _endPoint);
            // Debug.Log(dist);

            if (Input.GetMouseButton(0))
            {
                if (dist > minOffset)

                    Moved?.Invoke(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (dist > minOffset)
                    Left?.Invoke(Input.mousePosition);

                _startPoint = Vector2.zero;
                _endPoint = Vector2.zero;
            }

        }


    }
}
