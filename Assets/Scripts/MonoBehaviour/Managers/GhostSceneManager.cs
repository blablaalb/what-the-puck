using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GhostSceneManager : Singleton<GhostSceneManager>
{
    private Scene? _ghostScene;
    private PhysicsScene? _ghostPhysicsScene;
    private Board _ghostBoard;
    private PuckTarget[] _ghostPuckTargets;

    /// <summary>
    /// Sets the ghost scene as the current active one.
    /// </summary>
    public void Switch()
    {
        SceneManager.SetActiveScene(GetGhostScene());
    }

    public Scene GetGhostScene()
    {
        if (_ghostScene == null)
        {
            CreateSceneParameters createSceneParams = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
            _ghostScene = SceneManager.CreateScene("Ghost_Scene", createSceneParams);
        }
        return _ghostScene.Value;
    }

    public PhysicsScene GetGhostPhysicsScene()
    {
        if (_ghostPhysicsScene == null)
        {
            _ghostPhysicsScene = GetGhostScene().GetPhysicsScene();
        }
        return _ghostPhysicsScene.Value;
    }

    public Board GetBoardGhost()
    {
        if (_ghostBoard == null)
        {
            Board board = MainSceneManager.Instance.GetMainBoard();
            Board boardGhost = Instantiate<Board>(board);
            SceneManager.MoveGameObjectToScene(boardGhost.gameObject, GetGhostScene());
            boardGhost.MakeInvisible();
            _ghostBoard = boardGhost;
        }

        return _ghostBoard;
    }

    public PuckTarget[] GetGhostPuckTargets(bool update = false)
    {
        if (_ghostPuckTargets == null || update)
        {
            Board boardGhost = GetBoardGhost();
            _ghostPuckTargets = boardGhost.GetPuckTargets(true);
        }

        return _ghostPuckTargets;
    }

    public PuckTarget MakePuckTargetGhost(PuckTarget realPuckTarget)
    {
        PuckTarget ghostPuckTarget = null;
        Board ghostBoard = GetBoardGhost();
        PuckTargetParent ghostParent = ghostBoard.GetPuckTargetParent();

        realPuckTarget.gameObject.SetActive(false);
        ghostPuckTarget = Instantiate<PuckTarget>(realPuckTarget);
        ghostPuckTarget.gameObject.SetActive(false);
        ghostPuckTarget.SetParent(ghostParent);
        ghostPuckTarget.SetPosition(realPuckTarget.Position);
        ghostPuckTarget.SetRotation(realPuckTarget.Rotation);
        ghostPuckTarget.SetScale(realPuckTarget.Scale);
        
        realPuckTarget.gameObject.SetActive(true);
        ghostPuckTarget.gameObject.SetActive(true);
        return ghostPuckTarget;
    }

}
