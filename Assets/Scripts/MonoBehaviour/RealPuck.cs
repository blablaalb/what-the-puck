using System;
using System.Collections;
using UnityEngine;

public class RealPuck : Puck
{
    public static Action<Checkpoint, RealPuck> CheckpointReached;
    public static event Action<RealPuck> timeOut;

    override protected void Awake()
    {
        base.Awake();
        GameManager.OnLevelResume += levelStarted;
        GameManager.OnLevelWin += levelFinished;
    }

    override protected void OnDestroy()
    {
        GameManager.OnLevelResume -= levelStarted;
        GameManager.OnLevelWin -= levelFinished;

        StopCoroutine("Timer");

        CheckpointReached = null;
        base.OnDestroy();
    }

    private void levelFinished()
    {
        StopCoroutine("Timer");
    }

    private void levelStarted()
    {
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        // TODO: move to update
        var startTime = Time.timeSinceLevelLoad;

        while (startTime + 3 > Time.timeSinceLevelLoad)
        {
            yield return new WaitForSeconds(0.5f);
            if (_rigidBody.velocity.magnitude < 0.01f)
            {
                break;
            }
            Debug.Log(Time.timeSinceLevelLoad);
        }
        timeOut?.Invoke(this);
    }

    override protected void OnCheckpointHit(Checkpoint checkpoint, Puck puck)
    {
        if (!checkpoint.Reached)
        {
            if (puck is RealPuck realPuck)
            {
                if (realPuck == this)
                {
                    StopCoroutine("Timer");
                    PlaceInMiddleOfCheckpoint(checkpoint);
                    Stop();
                    CheckpointReached?.Invoke(checkpoint, this);
                }
            }
        }
    }


}