using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BuilderSaveLoadHandler : Singleton<BuilderSaveLoadHandler>
{
    override protected void Awake()
    {
        base.Awake();
    }

    public IEnumerable<Tuple<PuckTargetValuesData, string>> GetSavedPuckTargetValuesDatas()
    {
        foreach (Tuple<PuckTargetValuesData, string> ptvd in JsonSaverLoader.Instance.Enumerate<PuckTargetValuesData>())
        {
            yield return ptvd;
        }
    }

    public void Save(string name)
    {
        PuckTarget[] puckTargets = MainSceneManager.Instance.GetPuckTargets();
        PuckTargetValuesData sptvd = new PuckTargetValuesData();
        foreach (var puckTarget in puckTargets)
        {
            sptvd.AddPuckTargetMemento(puckTarget.SaveToMemento());
        }
        JsonSaverLoader.Instance.SaveToDisk<PuckTargetValuesData>(sptvd, name);
    }

    public void Load(string name)
    {
        PuckTargetValuesData savedPuckTarvetMementos;
        if (JsonSaverLoader.Instance.TryLoadFromDisk<PuckTargetValuesData>(out savedPuckTarvetMementos, name))
        {
            PuckTarget[] puckTargets = MainSceneManager.Instance.GetPuckTargets();

            foreach (var ptm in savedPuckTarvetMementos)
            {
                // PuckTarget puckTarget = GetPuckTargetByID(ptm.InstanceID);
                PuckTarget puckTarget  = PuckTargetInstantiator.Instance.Spawn(ptm.Type, ptm.Position, ptm.Rotation, ptm.Scale);
                PuckTargetInstantiator.Instance.MakeGhostPuckTarget(puckTarget);
                puckTarget.RestoreFromMemento(ptm);
            }
        }
    }

    private PuckTarget GetPuckTargetByID(int id)
    {
        PuckTarget target = null;
        PuckTarget[] puckTargets = MainSceneManager.Instance.GetPuckTargets();
        try
        {
            target = puckTargets.Where(x => x.InstanceID == id).First();
        }
        catch
        {
          throw new UnityException($"Was unable to find puck target with ID: {id}");
        }

        return target;
    }

    public void Clear()
    {
        foreach (var v in JsonSaverLoader.Instance.Enumerate<PuckTargetValuesData>())
        {
            string fileName = v.Item2;
            Delete(fileName);
        }
    }

    public void Delete(string name)
    {
        JsonSaverLoader.Instance.TryDelete(name);
    }

    public int GetSavedCount()
    {
        int count = 0;
        foreach (var v in JsonSaverLoader.Instance.Enumerate<PuckTargetValuesData>())
        {
            count++;
        }
        return count;
    }
}
