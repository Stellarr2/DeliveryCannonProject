using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPause : MonoBehaviour
{
    public event EventHandler OnPaused;
    public event EventHandler OnUnpaused;

    [Tooltip("Button to open the options menu")]
    [SerializeField] Button optionsButton;
    [Tooltip("Button to go to the menu")]
    [SerializeField] Button menuButton;
    [Tooltip("Button to leave the game")]
    [SerializeField] Button exitButton;
    [Tooltip("Transform for the container object")]
    [SerializeField] Transform container;
    [Tooltip("Transform for the options container")]
    [SerializeField] Transform optionsContainer;

    bool paused = false;

    void Awake()
    {
        optionsButton.onClick.AddListener(() =>
        {
            optionsContainer.gameObject.SetActive(true);
            container.gameObject.SetActive(false);
        });

        menuButton.onClick.AddListener(() =>
        {
            OnUnpaused?.Invoke(this, EventArgs.Empty);
            Loader.LoadScene(UIMainMenu.Scene.MenuScene);
        });

        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    void Start()
    {
        InputManager.Instance.OnPauseClick += InputManager_OnPauseClick;
        GameManager.OnGameStart += GameManager_OnGameStart;
    }

    void GameManager_OnGameStart(object sender, EventArgs e)
    {
        paused = false;
        container.gameObject.SetActive(false);
    }

    void InputManager_OnPauseClick(object sender, EventArgs e)
    {
        if(paused == false)
        {
            container.gameObject.SetActive(true);
            paused = true;
            OnPaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            container.gameObject.SetActive(false);
            optionsContainer.gameObject.SetActive(false);
            paused = false;
            OnUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

}
