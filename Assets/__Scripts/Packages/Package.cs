using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PolygonCollider2D))]
public class Package : MonoBehaviour
{
    [Tooltip("A package's Scriptable Object. Each package must have a Scriptable Object of the same type.")]
    [SerializeField] SOPackage soPackage;

    Transform _transform;
    Rigidbody2D packageBody;
    PolygonCollider2D packageCollider;
    House requestingHouse;

    float timer;
    bool fired = false;

    void Awake()
    {
        _transform = transform;
        packageBody = GetComponent<Rigidbody2D>();
        packageCollider = GetComponent<PolygonCollider2D>();
    }

    void Start()
    {
        StartCoroutine(DestroyAfterHouseIsUp());
    }


     void OnTriggerEnter2D(Collider2D collision)
     {
        collision.TryGetComponent(out House house);
        collision.TryGetComponent(out PlayerPickup playerPickup);
        if(house)
        {
            house.TakePackage(this);
        }
    }

    IEnumerator EndFired()
    {
        yield return new WaitForSeconds(1f);
        fired = false;
    }

    IEnumerator DestroyAfterHouseIsUp()
    {
        yield return new WaitForSeconds(requestingHouse.GetWaitingTimerMax());
        Destroy(gameObject);
    }

    public void MovePackage(Vector2 direction)
    {
        packageBody.AddForce(direction*soPackage.packageSpeed, ForceMode2D.Impulse);
        fired = true;
        StartCoroutine(EndFired());
    }

    public bool GetFired()
    {
        return fired;
    }

    public SOPackage GetSOPackage()
    {
        return soPackage;
    }

    public void SetRequestingHouse(House _requestingHouse)
    {
        requestingHouse = _requestingHouse;
    }

    public House GetRequestedHouse()
    {
        return requestingHouse;
    }
}
