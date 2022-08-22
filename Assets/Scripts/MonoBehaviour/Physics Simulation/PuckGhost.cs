using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Puck which is invisible is used only to simualte the movement of the real puck.
/// </summary>
public class PuckGhost : Puck
{

    
    override public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    override protected void OnCheckpointHit(Checkpoint checkpoint, Puck puck)
    {
        SetScale(checkpoint.puckSize*Vector3.one);
        // if (!checkpoint.Reached)
        // {
        //     if (puck is PuckGhost ghostPuck)
        //     {
        //         if (ghostPuck == this)
        //         {
        //             PlaceInMiddleOfCheckpoint(checkpoint);
        //             Stop();
        //         }
        //     }
        // }
    }

    private void MakeInvisible()
    {
        if (GetComponentInChildren<MeshRenderer>() is MeshRenderer meshRenderer)
        {
            meshRenderer.enabled = false;
        }
    }

    private void MakeVisible()
    {
        if (GetComponentInChildren<MeshRenderer>() is MeshRenderer meshRenderer)
        {
            meshRenderer.enabled = true;
        }
    }
}
