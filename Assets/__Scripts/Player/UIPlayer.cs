using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fullText;
    [SerializeField] PlayerPickup playerPickup;
    float maxTime = 1f;
    float timer;

    void Start()
    {
        playerPickup.OnFailPickup += PlayerPickup_OnFailPickup;
    }

    void PlayerPickup_OnFailPickup(object sender, EventArgs e)
    {
        fullText.gameObject.SetActive(true);
    }

    void Update()
    {
        if(fullText.gameObject.activeInHierarchy == true)
        {
            timer += Time.deltaTime;
            if(timer >= maxTime)
            {
                fullText.gameObject.SetActive(false);
                timer = 0f;
            }
        }
    }
}
