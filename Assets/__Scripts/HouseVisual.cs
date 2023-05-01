using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseVisual : MonoBehaviour
{
    [Tooltip("The body's sprite")]
    [SerializeField] SpriteRenderer bodySprite;
    [Tooltip("The door's sprite")]
    [SerializeField] SpriteRenderer doorSprite;
    [Tooltip("Colors the body can be")]
    [SerializeField] List<Color> bodyColors;
    [Tooltip("Colors the door can be")]
    [SerializeField] List<Color> doorColors;

    void Awake()
    {
        bodySprite.color = bodyColors[UnityEngine.Random.Range(0, bodyColors.Count)];
        doorSprite.color = doorColors[UnityEngine.Random.Range(0, doorColors.Count)];
    }

}
