using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class BoardBuilderEditor : EditorWindow
{
    [MenuItem("Window/Board Builder Editor")]
    internal static void Init()
    {
        BoardBuilderEditor boardBuilderEditor = EditorWindow.GetWindow<BoardBuilderEditor>("Board Builder");
    }

    internal void OnGUI()
    {
        try
        {
            PuckEditor.OnGUI();
            EndlessSimulatorEditor.OnGUI();
            BoardEditor.OnGUI();
        }
        catch (Exception exception)
        {
            Debug.LogError(exception);
        }
    }
}


public static class PuckEditor
{
    private static RealPuck _realPuck;
    private static float _yRotation;

    static PuckEditor()
    {
        _realPuck = RealPuckManager.Instance.GetRealPuck();
    }

    public static void OnGUI()
    {
        if (Application.isPlaying)
        {
            EditorGUILayout.LabelField("Puck Rotation");
            _yRotation = EditorGUILayout.Slider(_yRotation, -360f, 360f);
            Vector3 newRotation = new Vector3(0f, _yRotation, 0f);
            _realPuck.SetRotation(Quaternion.Euler(newRotation));
        }
    }
}

public static class EndlessSimulatorEditor
{
    private static float _forceSpeed;
    private static float _maxForceSpeed;

    static EndlessSimulatorEditor()
    {
        _maxForceSpeed = Launcher.Instance.ForceSpeed;
    }

    public static void OnGUI()
    {
        if (Application.isPlaying)
        {
            UpdateForceSpeed();
        }
    }

    private static void UpdateForceSpeed()
    {
        EditorGUILayout.LabelField("Acceleration");
        _forceSpeed = EditorGUILayout.Slider(_forceSpeed, 0f, _maxForceSpeed);
        EndlessSimulator.Instance.SetForceSpeed(_forceSpeed);
    }
}

public static class BoardEditor
{
    private static string _newFilename;

    public static void OnGUI()
    {
        if (Application.isPlaying)
        {
            DrawPuckTargets();
            DrawSaveAsNew();
            DrawEnumaratedSaves();
            DrawClearButton();
        }
    }

    private static void DrawSaveAsNew()
    {
        EditorGUILayout.BeginHorizontal();
        _newFilename = EditorGUILayout.TextField("Name", _newFilename);
        if (GUILayout.Button("Save"))
        {
            BuilderSaveLoadHandler.Instance.Save(_newFilename);
        }
        EditorGUILayout.EndHorizontal();
    }

    private static void DrawEnumaratedSaves()
    {
        var saves = BuilderSaveLoadHandler.Instance.GetSavedPuckTargetValuesDatas();
        foreach (var s in saves)
        {
            string fileName = s.Item2;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(fileName);
            if (GUILayout.Button("Rewrite"))
            {
                BuilderSaveLoadHandler.Instance.Save(fileName);
            }
            if (GUILayout.Button("Load"))
            {
                BuilderSaveLoadHandler.Instance.Load(fileName);
            }
            if (GUILayout.Button("Delete"))
            {
                BuilderSaveLoadHandler.Instance.Delete(fileName);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private static void DrawClearButton()
    {
        int savedCount = BuilderSaveLoadHandler.Instance.GetSavedCount();
        EditorGUI.BeginDisabledGroup(savedCount <= 0);
        if (GUILayout.Button("Clear"))
        {
            BuilderSaveLoadHandler.Instance.Clear();
        }
        EditorGUI.EndDisabledGroup();
    }

    private static void DrawPuckTargets()
    {
        EditorGUILayout.LabelField("Targets");
        PuckTarget[] realPuckTargets = MainSceneManager.Instance.GetPuckTargets(true);
        foreach (var v in realPuckTargets)
        {
            GUILayout.BeginHorizontal();
            DrawObject(v);
            DrawSelectGameObjectButton(v.gameObject);
            GUILayout.EndHorizontal();
        }
    }

    private static void DrawSelectGameObjectButton(GameObject go)
    {
        if (GUILayout.Button("Select"))
        {
            Selection.activeGameObject = go;
            EditorGUIUtility.PingObject(go);
        }
    }

    private static void DrawObject(UnityEngine.Object obj, bool disabled = true)
    {
        EditorGUI.BeginDisabledGroup(disabled);
        string name = ((Component)obj).gameObject.name;
        EditorGUILayout.ObjectField(name, obj, typeof(object), true);
        EditorGUI.EndDisabledGroup();
    }

}
