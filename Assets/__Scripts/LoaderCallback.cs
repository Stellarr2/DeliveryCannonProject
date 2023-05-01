using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    float maxLoadTime = 2f;
    float loadTime = 0f;

    void Awake()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        loadTime += Time.deltaTime;
        if(loadTime > maxLoadTime)
        {
            Loader.LoadGame();
        }
    }
}
