using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CtrlShitRReset : MonoBehaviour
{
    internal void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    ReloadScene();
                }
            }
        }
    }

    private static void ReloadScene()
    {
        // Scene activeScene = SceneManager.GetActiveScene();
        Scene activeScene = PhysicsSimulator.Instance.GetMainScene();
        Scene ghostScene = PhysicsSimulator.Instance.GetGhostScene();
        SceneManager.LoadScene(activeScene.name, LoadSceneMode.Single);
    }
}
