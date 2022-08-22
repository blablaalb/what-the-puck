using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PuckGhostManager : Singleton<PuckGhostManager>
{
    private PuckGhostPool _puckGhostPool;
    private PuckGhost _instantiatedPuckGhost = null;

    override protected void Awake()
    {
        _puckGhostPool = GetComponent<PuckGhostPool>();
    }

    public PuckGhost GetPuckGhost()
    {
        RealPuck realPuck = RealPuckManager.Instance.GetRealPuck();
        if (_instantiatedPuckGhost == null)
        {
            PuckGhost puckGhost = _puckGhostPool.Get();
            _instantiatedPuckGhost = puckGhost;
        }
        // Match position with the original puck's position.
        Vector3 position = realPuck.Position;
        _instantiatedPuckGhost.SetPosition(position);
        
        return _instantiatedPuckGhost;
    }
}
