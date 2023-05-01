using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    public static UIMainMenu Instance {get; private set;}
    public enum Scene
    {
        GameScene,
        LoadingScene,
        MenuScene
    } //Ordered properly

    [SerializeField] Button playButton;
    [SerializeField] Button aboutButton;

    [Space(15)]
    [SerializeField] Transform aboutContainer;

    void Awake()
    {Instance = this;

        playButton.onClick.AddListener(() =>
        {
            Loader.LoadScene(Scene.LoadingScene);
        });

        aboutButton.onClick.AddListener(() =>
        {
            aboutContainer.gameObject.SetActive(true);
        });
    }
}
