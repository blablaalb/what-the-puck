using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainSceneManager : Singleton<MainSceneManager>
{
    private Scene? _mainScene;
    private PhysicsScene? _mainPhysicsScene;
    private Board _mainBoard;
    private PuckTarget[] _puckTargets;


    public void ReloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// Sets the main scene at the active one.
    /// </summary>
    public void Switch()
    {
        SceneManager.SetActiveScene(GetMainScene());
    }

    public Scene GetMainScene()
    {
        if (_mainScene == null)
        {
            _mainScene = SceneManager.GetActiveScene();
        }
        return _mainScene.Value;
    }

    public PhysicsScene GetMainPhysicsScene()
    {
        if (_mainPhysicsScene == null)
        {
            _mainPhysicsScene = GetMainScene().GetPhysicsScene();
        }
        return _mainPhysicsScene.Value;
    }

    public Board GetMainBoard()
    {
        // NOTE: I'm not sure that the FindObjectOfType wouldn't search and return
        // a object from another additively loaded scene.
        // In case it does then I will need to:
        // 1.) search for a object and store reference to iit
        // 2.) copy the object afterwards to the anotehr scene.
        if (_mainBoard == null)
        {
            _mainBoard = FindObjectOfType<Board>();
        }
        return _mainBoard;
    }

    public PuckTarget[] GetPuckTargets(bool update = false)
    {
        if (_puckTargets == null || update)
        {
            _puckTargets = GetMainBoard().GetPuckTargets(update);
        }

        return _puckTargets;
    }
}
