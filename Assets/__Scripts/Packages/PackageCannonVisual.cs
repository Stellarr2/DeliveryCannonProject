using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageCannonVisual : MonoBehaviour
{

    [SerializeField] PackageCannon cannon;
    [SerializeField] SpriteRenderer cannonVisual;
    [SerializeField] Animator cannonAnimator;

    readonly string SHOOT_TRIGGER = "ShootTrigger";

    float minZ = 90f; //90f is when the gun points up
    float maxZ = 270f; //270f is when the gun points down

    void Awake()
    {
        cannonAnimator = cannonVisual.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        HandleVisual();
    }

    void Start()
    {
        cannon.OnCannonShoot += Cannon_OnShoot;
        cannon.OnRocketShoot += Cannon_OnShoot;
    }

    void Cannon_OnShoot(object sender, PackageCannon.OnCannonShootEventArgs e)
    {
        cannonAnimator.SetTrigger(SHOOT_TRIGGER);
    }

    void HandleVisual()
    {
        if(transform.eulerAngles.z > minZ)
        {
            cannonVisual.flipY = true;
            if(transform.eulerAngles.z > maxZ)
            {
                cannonVisual.flipY = false;
            }
        }

        else
        {
            cannonVisual.flipY = false;
        }
    }

    public bool GetFlipY()
    {
        return cannonVisual.flipY;
    }
}
