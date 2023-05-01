using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonParticles : MonoBehaviour
{
    [SerializeField] ParticleSystem cannonExplosion;
    [SerializeField] PackageCannon cannon;

    void Awake()
    {
        cannon = GetComponent<PackageCannon>();
    }
    void Start()
    {
        cannon.OnCannonShoot += Cannon_OnCannonShoot;
        cannon.OnRocketShoot += Cannon_OnRocketShoot;
    }

    void Cannon_OnCannonShoot(object sender, PackageCannon.OnCannonShootEventArgs e)
    {
        Instantiate(cannonExplosion, e.e_cannonEndPointPosition, Quaternion.identity);
    }

    void Cannon_OnRocketShoot(object sender, PackageCannon.OnCannonShootEventArgs e)
    {
        Instantiate(cannonExplosion, e.e_cannonEndPointPosition, Quaternion.identity);
    }
}
