using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIOptions : MonoBehaviour
{
    [SerializeField] Button backButton;
    [SerializeField] Transform container;
    [SerializeField] Transform pauseContainer;

    void Awake()
    {
        backButton.onClick.AddListener(() =>
        {
            pauseContainer.gameObject.SetActive(true);
            container.gameObject.SetActive(false);
        });
    }
}
