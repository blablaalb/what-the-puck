using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Memento object for saving state fo the PuckTarget object.
/// </summary>
[Serializable]
public class PuckTargetMemento : IEquatable<PuckTargetMemento>
{
    public Vector3 Position;
    public Vector3 Scale;
    public Quaternion Rotation;
    public int InstanceID;
    public PuckTargetType Type;

    public PuckTargetMemento(Vector3 position, Vector3 scale, Quaternion rotation, int instanceID, PuckTargetType type)
    {
        Position = position;
        Scale = scale;
        Rotation = rotation;
        InstanceID = instanceID;
        Type = type;
    }

    public override bool Equals(object obj)
    {
        bool equals = false;
        if (obj is PuckTargetMemento memento)
        {
            equals = Equals(memento);
        }
        return equals;
    }

    public bool Equals(PuckTargetMemento otherPuckTarget)
    {
        bool equals = false;
        if (otherPuckTarget != null)
        {
            equals = Position == otherPuckTarget.Position && Scale == otherPuckTarget.Scale && Rotation == otherPuckTarget.Rotation && InstanceID == otherPuckTarget.InstanceID;
        }
        return equals;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}