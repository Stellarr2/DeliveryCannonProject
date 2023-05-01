using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public static event EventHandler<OnPackageRequestedEventArgs> OnAnyPackageRequested;
    public event EventHandler<OnPackageRequestedEventArgs> OnPackageRequested;
    public class OnPackageRequestedEventArgs : EventArgs
    {
        public SOPackage e_soPackage;
        public House e_requestingHouse;
    }

    public static event EventHandler<OnDeliveredEventArgs> OnAnyDelivered;
    public event EventHandler<OnDeliveredEventArgs> OnDelivered;
    public class OnDeliveredEventArgs : EventArgs
    {
        public bool e_successStatus;
        public Vector3 e_housePosition;
        public int e_packageScore;
    }

    public static event EventHandler OnAnyFail;
    public event EventHandler OnFail;

    [Tooltip("Scriptable Object containing a list of all Package Scriptable Objects")]
    [SerializeField] SOPackageList soPackageList;
    
    [Tooltip("This represents how long a house can wait before a delivery fails")]
    [SerializeField] float waitingTimerMax;

    [Tooltip("Minimum and Maximum times it will take for house to request a delivery.")]
    [SerializeField] float minDeliveryTime, maxDeliveryTime;

    Transform _transform;
    SOPackage requestedSOPackage;
    float deliveryTimer, deliveryTimerMax, waitingTimer;
    bool waiting = false;
    bool deliveryStatus;

    void Awake()
    {
        _transform = transform;
        deliveryTimerMax = UnityEngine.Random.Range(minDeliveryTime, maxDeliveryTime);
        requestedSOPackage = null;
    }

    void Update()
    {
        HandleTimers();
    }

    void ResetAllTimers()
    {
        deliveryTimer = 0f; waitingTimer = 0f;
    }

    public void TakePackage(Package package)
    {
        if(package.GetRequestedHouse() == this)
        {
            deliveryStatus = true;
        }
        else if(requestedSOPackage == null)
        {
            deliveryStatus = false;
        }
        else
        {
            deliveryStatus = false;
        }
        requestedSOPackage = null;
        Destroy(package.gameObject);
        waiting = false;
        ResetAllTimers();
        
        OnAnyDelivered?.Invoke(this, new OnDeliveredEventArgs
        {
            e_successStatus = deliveryStatus,
            e_housePosition = _transform.position,
            e_packageScore = package.GetSOPackage().packageScore
        });

        OnDelivered?.Invoke(this, new OnDeliveredEventArgs
        {
            e_successStatus = deliveryStatus,
            e_housePosition = _transform.position,
            e_packageScore = package.GetSOPackage().packageScore
        });
    }

    void FailDelivery()
    {
        requestedSOPackage = null;
        waiting = false;
        ResetAllTimers();

        OnAnyDelivered?.Invoke(this, new OnDeliveredEventArgs
        {
            e_successStatus = false,
            e_housePosition = _transform.position
        });

        OnDelivered?.Invoke(this, new OnDeliveredEventArgs
        {
            e_successStatus = false,
            e_housePosition = _transform.position
        });

        OnAnyFail?.Invoke(this, EventArgs.Empty);
        OnFail?.Invoke(this, EventArgs.Empty);
    }

    void HandleTimers()
    {
        if(GameManager.Instance.state != GameManager.GameState.Gameplay) {return;}

            if(waiting == false)
            {
                  deliveryTimer += Time.deltaTime;
                if(deliveryTimer >= deliveryTimerMax)
                {
                    deliveryTimer = 0f;
                    deliveryTimerMax = UnityEngine.Random.Range(minDeliveryTime, maxDeliveryTime);
                    RequestPackage();
                    waiting = true;
                }
            }

        if(waiting)
        {
            waitingTimer += Time.deltaTime;
            if(waitingTimer >= waitingTimerMax)
            {
                FailDelivery();
                waiting = false;
                deliveryTimer = 0f;
                waitingTimer = 0f;
            }
        }
    }

    void RequestPackage()
    {
        waiting = true;

        requestedSOPackage = soPackageList.packageList
        [UnityEngine.Random.Range(0, soPackageList.packageList.Count)];

        OnAnyPackageRequested?.Invoke(this, new OnPackageRequestedEventArgs
        {
            e_soPackage = requestedSOPackage,
            e_requestingHouse = this
        });
        OnPackageRequested?.Invoke(this, new OnPackageRequestedEventArgs
        {
            e_soPackage = requestedSOPackage,
            e_requestingHouse = this
        });
    }

    public static void ResetStaticData()
    {
        OnAnyDelivered = null;
        OnAnyFail = null;
        OnAnyPackageRequested = null;
    }

    public float GetWaitingTimerNormalized()
    {
        return waitingTimer / waitingTimerMax;
    }
    
    public float GetWaitingTimerMax()
    {
        return waitingTimerMax;
    }
}
