using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScoreKeeper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    void Update()
    {
        scoreText.text = "Score: " + ScoreManager.Instance.GetScore().ToString();
    }
}
