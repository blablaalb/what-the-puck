using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PuckTargetInstantiator : Singleton<PuckTargetInstantiator>
{
    [SerializeField]
    private PuckTarget _puckTargetCube;

    public PuckTarget Spawn(PuckTargetType type, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        PuckTarget target = null;
        switch (type)
        {
            case PuckTargetType.Cube:
                target = Instantiate<PuckTarget>(_puckTargetCube);
                break;

            default:
                throw new Exception($"Unkon type: {type}");
        }

        PuckTargetParent puckTargetParent = MainSceneManager.Instance.GetMainBoard().GetPuckTargetParent();
        target.SetParent(puckTargetParent);
        target.SetPosition(position);
        target.SetRotation(rotation);
        target.SetScale(scale);
        return target;
    }

    public PuckTarget Spawn(PuckTargetType type)
    {
        return Spawn(type, Vector3.zero, Quaternion.identity, Vector3.zero);
    }

    public PuckTarget MakeGhostPuckTarget(PuckTarget target)
    {
        return GhostSceneManager.Instance.MakePuckTargetGhost(target);
    }
}
