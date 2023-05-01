using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPointerArrow : MonoBehaviour
{
    [SerializeField] PlayerPickup playerPickup;
    [SerializeField] Image pointerArrow;
    [SerializeField] float arrowSpeed;

    SOPackage selectedSOPackage;
    House requestedHouse;
    Transform _transform;
    int index;

    void Awake()
    {
        _transform = transform;
    }

    void Update()
    {
        FollowRequestedHouse();
    }

    void FollowRequestedHouse()
    {
        try{requestedHouse = playerPickup.GetRequestedHouses()[0];} catch(ArgumentOutOfRangeException){requestedHouse = null;}

        if(requestedHouse == null)
        {
            pointerArrow.gameObject.SetActive(false);
            return;
        }
            Transform targetTransform = requestedHouse.transform;
            pointerArrow.gameObject.SetActive(true);
            Vector3 direction = targetTransform.position - _transform.position;
            float angle = MathF.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
            _transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

