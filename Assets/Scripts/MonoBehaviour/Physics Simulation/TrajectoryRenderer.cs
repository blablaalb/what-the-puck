using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryRenderer : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    internal void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.loop = false;
    }

    public void RenderPoints(Vector3[] points)
    {
        _lineRenderer.positionCount = points.Length;
        _lineRenderer.SetPositions(points);
    }
}
