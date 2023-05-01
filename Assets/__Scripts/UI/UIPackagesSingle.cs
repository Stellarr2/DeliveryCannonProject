using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPackagesSingle : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI text;

    public void SetIcon(SOPackage package, int packageIndex)
    {
        gameObject.SetActive(true);
        image.sprite = package.packageIcon;
        text.text = packageIndex.ToString();
    }
}
