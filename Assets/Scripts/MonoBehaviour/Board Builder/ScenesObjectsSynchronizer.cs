using System.Collections.Generic;
using UnityEngine;
using System;

public class ScenesObjectsSynchronizer : MonoBehaviour
{
    internal void LateUpdate()
    {
        Synchronize();
    }

    private void Synchronize()
    {
        PuckTarget[][] puckTargets = GetPuckTargets();
        int length = puckTargets[0].Length;

        for (int i = 0; i < length; i++)
        {
            PuckTarget realPuckTarget = puckTargets[0][i];
            PuckTarget ghostPuckTarget = puckTargets[1][i];
            ghostPuckTarget.SetPosition(realPuckTarget.Position);
            ghostPuckTarget.SetRotation(realPuckTarget.Rotation);
            ghostPuckTarget.SetScale(realPuckTarget.Scale);
        }
    }

    /// <summary>
    /// Returns jagged array of the real puck targets as the first argument and the ghost puck targets as the second argument.
    /// </summary>
    /// <returns>Jagged array of the real and ghost puck targets.</returns>
    private PuckTarget[][] GetPuckTargets()
    {
        PuckTarget[][] realAndGhostPuckTargets = null;
        PuckTarget[] realPuckTargets = MainSceneManager.Instance.GetPuckTargets(true);
        PuckTarget[] ghostPuckTarget = GhostSceneManager.Instance.GetGhostPuckTargets(true);
        realAndGhostPuckTargets = new PuckTarget[][] { realPuckTargets, ghostPuckTarget };
        int realPucksCount = realAndGhostPuckTargets[0].Length;
        int ghostPucksCount = realAndGhostPuckTargets[1].Length;
        if (realPucksCount > ghostPucksCount)
        {
            Debug.LogError($"Real boards are more than ghost ones. Ghost boards: {ghostPucksCount}, Real boards: {realPucksCount}");
        }
        else if (ghostPucksCount > realPucksCount)
        {
            Debug.LogError($"Ghost boards are more than real ones. Ghost boards: {ghostPucksCount}, Real boards: {realPucksCount}");
        }
        return realAndGhostPuckTargets;
    }
}
