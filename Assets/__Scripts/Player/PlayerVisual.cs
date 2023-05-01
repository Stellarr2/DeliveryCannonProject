using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PackageCannonVisual cannonVisual;
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] Animator playerAnimator;

    readonly string MOVING = "Moving";

    void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        AnimatePlayer();
    }

    void AnimatePlayer()
    {
        playerSprite.flipX = cannonVisual.GetFlipY();

        if(InputManager.Instance.GetMovementVector().x != 0 ||InputManager.Instance.GetMovementVector().y != 0)
        {
            playerAnimator.SetBool(MOVING, true);
        }
        else
        {
            playerAnimator.SetBool(MOVING, false);
        }
    }
}
