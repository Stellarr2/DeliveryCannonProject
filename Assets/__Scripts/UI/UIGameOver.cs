using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] Button playAgainButton;
    [SerializeField] Button exitButton;

    void Awake()
    {
        playAgainButton.onClick.AddListener(() =>
        {
            ScoreManager.Instance.ResetScore();
            Loader.LoadScene(UIMainMenu.Scene.LoadingScene);
        });

        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    void Update()
    {
        if(GameManager.Instance.state == GameManager.GameState.GameOver)
        {
            container.gameObject.SetActive(true);
            scoreText.text = "SCORE: " + ScoreManager.Instance.GetScore().ToString();
            highScoreText.text = "HIGH-SCORE: " + ScoreManager.Instance.GetHighScore().ToString();
        }
        else{container.gameObject.SetActive(false);}
    }
}
