using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance {get; private set;}

    [Tooltip("Current score")]
    [SerializeField] int score = 0;
    [Tooltip("Highest score ever achieved")]
    [SerializeField] int highScore = 0;

    int scoreDeduction = 10;

    void Awake()
    {Instance = this;
        
    }

    void Start()
    {
        House.OnAnyDelivered += House_OnAnyDelivered;
    }

    void House_OnAnyDelivered(object sender, House.OnDeliveredEventArgs e)
    {
        if(e.e_successStatus == true)
        {
            score += e.e_packageScore;
        }
        else if(e.e_successStatus == false)
        {
            score -= scoreDeduction;
        }

        if(score > highScore)
        {
            highScore = score;
        }
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighScore()
    {
        return highScore;
    }
}
