using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class PuckTargetValuesData : IEnumerable<PuckTargetMemento>
{
    [SerializeField]
    public List<PuckTargetMemento> _puckTargetMementoObjects;

    /// <summary>
    /// Amount of saved puck targets.
    /// </summary>
    public int Count => _puckTargetMementoObjects.Count;

    public PuckTargetValuesData(){}

    public PuckTargetValuesData(ICollection<PuckTargetMemento> puckTargetMementos)
    {
        if (puckTargetMementos == null)
        {
            throw new NullReferenceException("Puck Target Memento is null");
        }
        _puckTargetMementoObjects = new List<PuckTargetMemento>(puckTargetMementos);
    }

    public ReadOnlyCollection<PuckTargetMemento> GetPuckTargetMementos()
    {
        return new ReadOnlyCollection<PuckTargetMemento>(_puckTargetMementoObjects);
    }

    public void AddPuckTargetMemento(PuckTargetMemento memento)
    {
        if (_puckTargetMementoObjects == null)
        {
            _puckTargetMementoObjects = new List<PuckTargetMemento>();
        }

        if (!_puckTargetMementoObjects.Contains(memento))
        {
            if (_puckTargetMementoObjects.Find(x => x.InstanceID == memento.InstanceID) is PuckTargetMemento existingMemento)
            {
                _puckTargetMementoObjects.Remove(existingMemento);
            }
            _puckTargetMementoObjects.Add(memento);
        }
    }

    public void Clear()
    {
        _puckTargetMementoObjects.Clear();
    }

    public IEnumerator<PuckTargetMemento> GetEnumerator()
    {
        return _puckTargetMementoObjects.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _puckTargetMementoObjects.GetEnumerator();
    }
}
