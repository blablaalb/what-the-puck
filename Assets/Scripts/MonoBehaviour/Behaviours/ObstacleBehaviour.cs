using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [Range(0f,1f)]
    public float animationTime;
    [Range(0f,1f)]
    public float scaleFactor;

    public GameObject root;
    
    private LTDescr anim;
    private Vector3 startScale;

    public static event Action<ObstacleBehaviour> onHit;


    void Awake(){
        if(root == null){
            root = this.gameObject;
        }
        startScale = root.transform.localScale;
    }

    internal void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Puck>() is RealPuck puck)
        {
            if (anim != null){
                LeanTween.cancel(gameObject);
                root.transform.localScale = startScale;
            }
            anim = root.transform.LeanScale(startScale + new Vector3(1f, 1f, 1f) * scaleFactor,animationTime).setLoopPingPong(1);
            onHit(this);
        }
    }
}
