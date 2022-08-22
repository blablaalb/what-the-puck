using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class JsonSaverLoader : Singleton<JsonSaverLoader>
{
    private string _path;
    private const string EXTENSION = ".json";
    private const string FOLDER_NAME = "Built_Scenes";


    protected override void Awake()
    {
        base.Awake();

        _path = Path.Combine(Application.persistentDataPath, FOLDER_NAME);
    }

    internal void Start()
    {
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }
    }

    public void SaveToDisk<T>(T obj) where T : class
    {
        string name = typeof(T).FullName;
        SaveToDisk(obj, name);
    }

    /// <summary>
    /// Saves given object with the given name on the disk. Overrides if file already exists.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void SaveToDisk<T>(T obj, string name) where T : class
    {
        name = AppendExtension(name);
        string json = JsonUtility.ToJson(obj);
        string fullPath = Path.Combine(_path, name);
        File.WriteAllText(fullPath, json);
    }

    public bool TryLoadFromDisk<T>(out T obj, string name) where T : class
    {
        obj = null;
        try
        {
            name = AppendExtension(name);
            string fullPath = Path.Combine(_path, name);
            string json = File.ReadAllText(fullPath);
            obj = JsonUtility.FromJson<T>(json);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            return false;
        }

        return true;
    }

    public bool TryLoadFromDisk<T>(out T obj) where T : class
    {
        string name = typeof(T).FullName;
        return TryLoadFromDisk<T>(out obj, name);
    }

    public bool TryDelete(string name)
    {
        bool success = false;
        name = AppendExtension(name);
        string fullPath = Path.Combine(_path, name);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
        else
        {
            Debug.LogError($"Could ffind {fullPath}");
        }
        return success;
    }

    /// <summary>
    /// Enumerates through saved files and return each saved file with its name (expluding extension).
    /// </summary>
    /// <typeparam name="T">Type of the file to return</typeparam>
    /// <typeparam name="string">Name of the returned file.</typeparam>
    public IEnumerable<Tuple<T, string>> Enumerate<T>() where T : class
    {
        IEnumerable<string> paths = Directory.EnumerateFiles(_path);
        foreach (string path in paths)
        {
            T obj;
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(path);
            if (TryLoadFromDisk<T>(out obj, fileNameWithoutExt))
            {
                yield return new Tuple<T, string>(obj, fileNameWithoutExt);
            }
        }
    }

    private string AppendExtension(string name)
    {
        return name + EXTENSION;
    }

}
