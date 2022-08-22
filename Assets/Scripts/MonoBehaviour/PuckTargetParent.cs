using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PuckTargetParent : MonoBehaviour
{
    private List<PuckTarget> _targets;

    internal void Awake()
    {
        UpdateTargets();
    }

    internal void LateUpdate()
    {
        UpdateTargets();
    }

    private void UpdateTargets()
    {
        _targets = new List<PuckTarget>(GetComponentsInChildren<PuckTarget>());
    }
}
