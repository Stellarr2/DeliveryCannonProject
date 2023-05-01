using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    Transform _transform;
    float footstepTimerMax = .5f;
    float timer;

    void Awake()
    {
        _transform = transform;
        timer = footstepTimerMax;
    }

    void Update()
    {
        if(InputManager.Instance.GetMovementVector().x != 0 || InputManager.Instance.GetMovementVector().y != 0)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                SoundManager.Instance.PlayFootstepsSound(_transform.position);
                timer = footstepTimerMax;
            }
        }
    }
}
