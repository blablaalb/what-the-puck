using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{

    public static event Action OnLevelStarted;
    public static event Action OnLevelResume;
    public static event Action OnLevelWin;
    public static event Action OnLevelLose;

    private bool _isPlaying = false;
    private bool _isStarted  = false;

    override protected void Awake()
    {
        base.Awake();
        Checkpoint.CheckpointHit += onCheckpointHit;
        OnLevelStarted += levelStarted;
        OnLevelResume += levelStarted;
        OnLevelLose += levelLose;
        OnLevelWin += levelWin;
        TouchInput.Left += onLeft;
        KeyboardInput.Left += onLeft;
        RealPuck.timeOut += timeOut;
    }


    override protected void OnDestroy()
    {
        base.OnDestroy();
        Checkpoint.CheckpointHit -= onCheckpointHit;
        OnLevelStarted -= levelStarted;
        OnLevelResume -= levelStarted;
        OnLevelLose -= levelLose;
        OnLevelWin -= levelWin;
        TouchInput.Left -= onLeft;
        KeyboardInput.Left -= onLeft;
        RealPuck.timeOut -= timeOut;

    }

    
    private void timeOut(RealPuck obj)
    {
        OnLevelLose();
    }

    private void onLeft(Vector2 vec){
        if(!_isPlaying){
            OnLevelStarted?.Invoke();
            OnLevelResume?.Invoke();
        }
    }


    private void levelStarted()
    {
        _isStarted = true;
        _isPlaying = true;
    }
    private void levelLose()
    {
        _isStarted = false;
        _isPlaying = false;
    }
    private void levelWin()
    {
        _isStarted = false;
        _isPlaying = false;
    }

    // Update is called once per frame
    void onCheckpointHit(Checkpoint checkpoint, Puck puck)
    {
        _isPlaying = false;
        if (checkpoint.isFinishPoint)
        {
            if (puck is RealPuck)
            {
                OnLevelWin?.Invoke();
                // MainSceneManager.Instance.ReloadScene();
            }
        }
    }
}
