using System.Collections.Generic;
using UnityEngine;
using System;

public class Board : MonoBehaviour
{
    private PuckTarget[] _puckTargets;
    private MeshRenderer[] _meshRenderers;
    private PuckTargetParent _puckTargetParent;

    internal void Awake()
    {
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        _puckTargets = GetComponentsInChildren<PuckTarget>();
    }

    public PuckTargetParent GetPuckTargetParent()
    {
        if (_puckTargetParent == null)
        {
            _puckTargetParent = GetComponentInChildren<PuckTargetParent>();
        }
        return _puckTargetParent;
    }

    public PuckTarget[] GetPuckTargets(bool update = false)
    {
        if (_puckTargets == null || update)
        {
            _puckTargets = GetComponentsInChildren<PuckTarget>();
        }
        return _puckTargets;
    }

    public void MakeVisible()
    {
        foreach (MeshRenderer mr in _meshRenderers)
        {
            mr.enabled = true;
        }
    }

    public void MakeInvisible()
    {
        foreach (MeshRenderer mr in _meshRenderers)
        {
            mr.enabled = false;
        }
    }
}
