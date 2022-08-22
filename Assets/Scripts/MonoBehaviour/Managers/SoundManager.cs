using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public AudioClip popSound;
    public AudioSource effectsSource;


    override protected void Awake(){
        base.Awake();
        ObstacleBehaviour.onHit += onObstacleHit;
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
        ObstacleBehaviour.onHit -= onObstacleHit;
    }

    private void onObstacleHit(ObstacleBehaviour obj)
    {
        effectsSource.PlayOneShot(popSound);
    }
}
