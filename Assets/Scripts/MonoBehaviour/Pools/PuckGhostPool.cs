using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PuckGhostPool : GenericPool<PuckGhost>
{
    override public PuckGhost Get()
    {
        RealPuck realPuck = RealPuckManager.Instance.GetRealPuck();
        PuckGhost puckGhost = base.Get();
        puckGhost.gameObject.SetActive(false);
        Scene ghostScene = PhysicsSimulator.Instance.GetGhostScene();
        SceneManager.MoveGameObjectToScene(puckGhost.gameObject, ghostScene);
        puckGhost.SetPosition(realPuck.Position);
        puckGhost.SetRotation(realPuck.Rotation);
        puckGhost.SetScale(realPuck.Scale);
        puckGhost.gameObject.SetActive(true);
        return puckGhost;
    }
}
