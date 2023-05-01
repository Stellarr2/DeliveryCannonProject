using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHouseEffect : MonoBehaviour
{
    [SerializeField] House house;
    [SerializeField] TextMeshProUGUI successText;
    [SerializeField] TextMeshProUGUI failText;

    float maxTimer = 1f;
    float timer = 0f;

    void Start()
    {
        house.OnDelivered += House_OnDelivered;
    }


    void House_OnDelivered(object sender, House.OnDeliveredEventArgs e)
    {
        successText.gameObject.SetActive(e.e_successStatus);
        failText.gameObject.SetActive(!e.e_successStatus);
        successText.text = "+"+e.e_packageScore.ToString()+"!";
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= maxTimer)
        {
            successText.gameObject.SetActive(false);
            failText.gameObject.SetActive(false);
            timer = 0f;
        }
    }
}
