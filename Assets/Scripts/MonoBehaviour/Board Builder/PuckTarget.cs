using System.Collections.Generic;
using UnityEngine;
using System;

public class PuckTarget : MonoBehaviour
{
    [SerializeField]
    private PuckTargetType _type;

    public Vector3 Position => transform.position;
    public Vector3 Scale => transform.localScale;
    public Quaternion Rotation => transform.rotation;
    public int InstanceID { get; private set; }
    public PuckTargetType Type => _type;

    internal void Awake()
    {
        InstanceID = gameObject.GetInstanceID();
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    public void SetParent(PuckTargetParent parent)
    {
        transform.SetParent(parent.transform);
    }

    public PuckTargetMemento SaveToMemento()
    {
        return new PuckTargetMemento(Position, Scale, Rotation, InstanceID, _type);
    }

    public void RestoreFromMemento(PuckTargetMemento memento)
    {
        SetPosition(memento.Position);
        SetRotation(memento.Rotation);
        SetScale(memento.Scale);
    }
}
