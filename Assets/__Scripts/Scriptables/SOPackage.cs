using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SOPackage : ScriptableObject
{
    public Transform packagePrefab;
    public Sprite packageIcon;
    public float packageSpeed;
    public int packageScore;
}
