using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public abstract class Puck : MonoBehaviour
{
    protected Rigidbody _rigidBody;

    public Vector3 Position => _rigidBody.position;
    public Quaternion Rotation => _rigidBody.rotation;
    public Vector3 Scale => transform.localScale;

    public void SetPosition(Vector3 position)
    {
        _rigidBody.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        _rigidBody.rotation = rotation;
    }
    public virtual void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    virtual protected void Awake()
    {
        Checkpoint.CheckpointHit += OnCheckpointHit;
        _rigidBody = GetComponent<Rigidbody>();
    }

    virtual protected void OnDestroy()
    {
        Checkpoint.CheckpointHit -= OnCheckpointHit;
    }

    public void Stop()
    {
        _rigidBody.velocity = Vector3.zero;
    }

    public void AddForce(Vector3 force, ForceMode forceMode)
    {
        _rigidBody.AddForce(force, forceMode);
        // Debug.Log($"<color=red>Puck: <b>{force}</b> force added.</color>", this);
    }

    protected void PlaceInMiddleOfCheckpoint(Checkpoint checkpoint)
    {
        Vector3 middle = checkpoint.Position;
        if (checkpoint.TransactionLocation != null)
        {
            middle = checkpoint.TransactionLocation;
        }
        SetPosition(middle);
        SetScale(checkpoint.puckSize*Vector3.one);
    }

    protected abstract void OnCheckpointHit(Checkpoint checkpoint, Puck puck);
}

