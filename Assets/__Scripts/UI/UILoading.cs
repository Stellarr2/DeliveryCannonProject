using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILoading : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI loadingText;

    float maxDotTime = 0.5f;
    float dotTime;
    int timesDotted = 0;

    void Awake()
    {
        loadingText.text = "LOADING GAME";
    }

    void Update()
    {
        dotTime += Time.deltaTime;
        if(dotTime >= maxDotTime)
        {
            timesDotted++;
            if(timesDotted > 3)
            {
                timesDotted = 0;
            }
            switch(timesDotted)
            {
                case 0: 
                    loadingText.text = "LOADING GAME";
                break;
                case 1: 
                    loadingText.text = "LOADING GAME.";
                break;
                case 2:
                    loadingText.text = "LOADING GAME..";
                break;
                case 3:
                    loadingText.text = "LOADING GAME...";
                break;
            }
            dotTime = 0f;
        }
    }
}
