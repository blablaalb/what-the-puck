using System.Collections.Generic;
using UnityEngine;
using System;

public class EndlessSimulator : Singleton<EndlessSimulator>
{
    private float _forceSpeed;

    internal void LateUpdate()
    {
        Launcher launcher = Launcher.Instance;
        ForceMode forceMode = launcher.ForceMode;
        PhysicsSimulator simulator = PhysicsSimulator.Instance;
        RealPuck realPuck = RealPuckManager.Instance.GetRealPuck();
        Vector3 force = realPuck.transform.forward * _forceSpeed;
        simulator.Simulate(force, forceMode);
    }

    public void SetForceSpeed(float speed)
    {
        _forceSpeed = speed;
    }
}
