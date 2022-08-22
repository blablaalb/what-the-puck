using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public MainSceneManager sceneManager;
    public GameObject StartPanel;
    public GameObject WinPanel;
    public Button nextLevelButton;
    public GameObject LosePanel;
    public Button restartButton;

    void Awake()
    {
        GameManager.OnLevelWin += onLevelWin;
        GameManager.OnLevelLose += onLevelLose;
        GameManager.OnLevelStarted += onLevelStarted;
        TouchInput.Moved += onTouch;
        KeyboardInput.Moved += onTouch;

        nextLevelButton.onClick.AddListener(() =>
        {
            sceneManager.ReloadScene();
        });

        restartButton.onClick.AddListener(() =>
        {
            sceneManager.ReloadScene();
        });

    }
    void OnDestroy()
    {
        GameManager.OnLevelWin -= onLevelWin;
        GameManager.OnLevelLose -= onLevelLose;
        GameManager.OnLevelStarted -= onLevelStarted;
        TouchInput.Moved -= onTouch;
        KeyboardInput.Moved -= onTouch;
    }

    private void onTouch(Vector2 obj)
    {
        if (StartPanel.activeInHierarchy)
            StartPanel.SetActive(false);
    }

    private void onLevelStarted()
    {

    }

    private void onLevelWin()
    {
        WinPanel.SetActive(true);
    }

    private void onLevelLose()
    {
        LosePanel.SetActive(true);
    }

}
