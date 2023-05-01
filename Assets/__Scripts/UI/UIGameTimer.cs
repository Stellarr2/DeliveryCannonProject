using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    float gameTime;

    void Update()
    {
        TrackTime();
    }
    
    void TrackTime()
    {
        gameTime = GameManager.Instance.GetGameplayTimer();
        int minutes = Mathf.FloorToInt(gameTime / 60f);
        int seconds = Mathf.FloorToInt(gameTime % 60f);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
