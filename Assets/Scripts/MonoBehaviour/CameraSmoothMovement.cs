using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraSmoothMovement : MonoBehaviour
{
    [SerializeField]
    private float _transitionTime;

    internal void Start()
    {
        Checkpoint.CheckpointHit += OnCheckpointHit;
    }

    internal void OnDestroy()
    {
        Checkpoint.CheckpointHit -= OnCheckpointHit;
    }

    public void Move(Vector3 to, Quaternion rotaion)
    {
        LeanTween.move(gameObject, to, _transitionTime);
        LeanTween.rotate(gameObject, rotaion.eulerAngles, _transitionTime);
    }

    private void OnCheckpointHit(Checkpoint checkpoint, Puck puck)
    {
        if (puck is RealPuck realPuck)
        {
            Move(checkpoint.CameraLocation, checkpoint.CameraRotaion);
        }
    }
}
