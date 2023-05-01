using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public event EventHandler OnPickupPackage;
    public event EventHandler OnFailPickup;

    CapsuleCollider2D playerCollider;
    List<SOPackage> pickedUpSOPackages;
    List<House> requestedHouses;
    PackageCannon packageCannon;
    Transform _transform;
    int maxSOPackages = 9;

    void Awake()
    {
        _transform = transform;
        playerCollider = GetComponent<CapsuleCollider2D>();
        packageCannon = GetComponent<PackageCannon>();
        pickedUpSOPackages = new List<SOPackage>();
        requestedHouses = new List<House>();
    }

    void Start()
    {
        packageCannon.OnCannonShoot += PackageCannon_OnCannonShoot;
    }

    void PackageCannon_OnCannonShoot(object sender, PackageCannon.OnCannonShootEventArgs e)
    {
        pickedUpSOPackages.RemoveAt(0); //Doing this so when you shoot a package you lose it from your ammo.
        requestedHouses.RemoveAt(0); //Doing this to clear the requested house from the List
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Package package))
        {
            if(pickedUpSOPackages.Count >= maxSOPackages)
            {
                Debug.Log("Nope");
                OnFailPickup?.Invoke(this, EventArgs.Empty);
                return;
            }

            if(package.GetFired())
            {
                return;
            }

            pickedUpSOPackages.Add(package.GetSOPackage());
            requestedHouses.Add(package.GetRequestedHouse());
            Destroy(package.gameObject);
            OnPickupPackage?.Invoke(this, EventArgs.Empty);
        }
    }

    public List<SOPackage> GetSOPackages()
    {
        return pickedUpSOPackages;
    }

    public List<House> GetRequestedHouses()
    {
        return requestedHouses;
    }
}
