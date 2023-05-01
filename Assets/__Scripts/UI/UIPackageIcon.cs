using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPackageIcon : MonoBehaviour
{
    [SerializeField] House house;
    [SerializeField] TextMeshProUGUI packageText;
    [SerializeField] Image icon;
    [SerializeField] Image fillImage;

    void Awake()
    {
        fillImage.fillAmount = 0f;
    }

    void Start()
    {
        house.OnPackageRequested += House_OnPackageRequested;
        house.OnDelivered += House_OnDelivered;
        house.OnFail += House_OnFail;
    }

    void Update()
    {
        fillImage.fillAmount = house.GetWaitingTimerNormalized();
    }

    void House_OnDelivered(object sender, House.OnDeliveredEventArgs e)
    {
        packageText.text = "Pending";
        icon.gameObject.SetActive(false);
        fillImage.fillAmount = 0f;
    }

    void House_OnPackageRequested(object sender, House.OnPackageRequestedEventArgs e)
    {
        packageText.text = "Waiting";
        icon.gameObject.SetActive(true);
        icon.sprite = e.e_soPackage.packageIcon;
    }

    void House_OnFail(object sender, EventArgs e)
    {
        fillImage.fillAmount = 0f;
    }
}
