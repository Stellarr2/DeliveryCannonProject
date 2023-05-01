using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public static void LoadScene(UIMainMenu.Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public static void LoadGame()
    {
        SceneManager.LoadScene(UIMainMenu.Scene.GameScene.ToString());
    }
}
