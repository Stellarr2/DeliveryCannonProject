using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticData : MonoBehaviour
{
    void Awake()
    {
        House.ResetStaticData();
        GameManager.ResetStaticData();
    }
}
