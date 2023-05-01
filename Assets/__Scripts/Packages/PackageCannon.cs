using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageCannon : MonoBehaviour
{
    public event EventHandler<OnCannonShootEventArgs> OnCannonShoot;
    public event EventHandler<OnCannonShootEventArgs> OnRocketShoot;
    public class OnCannonShootEventArgs : EventArgs
    {
        public Vector3 e_cannonEndPointPosition;
        public Vector3 e_shootPosition;
        public Transform e_cannon;
    }

    [Tooltip("Since the cannon is a different object this a reference to it")]
    [SerializeField] Transform cannonTransform;
    [Tooltip("Reference to the object at the end of the cannon where the packages are spawned")]
    [SerializeField] Transform cannonEndPoint;
    
    List<SOPackage> SOpackages;
    List<House> requestingHouses;
    PlayerPickup playerPickup;
    Transform _transform;

    //NOTE: 0 means the start of the list/array

    void Awake()
    {
        _transform = transform;
        playerPickup = GetComponent<PlayerPickup>();
    }

    void Start()
    {
        InputManager.Instance.OnCannonClick += InputManager_OnCannonClick;
        InputManager.Instance.OnRocketClick += InputManager_OnRocketClick;
        InputManager.Instance.OnNumberClick += InputManager_OnNumberClick;
        playerPickup.OnPickupPackage += PlayerPickup_OnPickupPackage;
    }

    void PlayerPickup_OnPickupPackage(object sender, EventArgs e)
    {
        SOpackages = playerPickup.GetSOPackages();
        requestingHouses = playerPickup.GetRequestedHouses();
    }

    void InputManager_OnNumberClick(object sender, InputManager.OnNumberClickEventArgs e)
    {
        SelectPackage(e.e_numberPressed);
    }

    void InputManager_OnCannonClick(object sender, EventArgs e)
    {
        ShootCannon();
    }

    void InputManager_OnRocketClick(object sender, EventArgs e)
    {
        ShootRocket();
    }

    void Update()
    {
        MoveCannon();
    }

    void SelectPackage(int inputNumber)
    {
        SOpackages = playerPickup.GetSOPackages(); //Update the list of SOpackages
        requestingHouses = playerPickup.GetRequestedHouses(); // Update the list of requesting houses

        if(inputNumber > SOpackages.Count-1) {return;} //Make sure were in range

        SOPackage packageToMove = SOpackages[inputNumber]; //Get package to move
        SOpackages.RemoveAt(inputNumber); //Remove that package so we can insert
        SOpackages.Insert(0, packageToMove); //Insert that package at the 0 index so we can fire it

        House houseToMove = requestingHouses[inputNumber];
        requestingHouses.RemoveAt(inputNumber);
        requestingHouses.Insert(0, houseToMove);
    }

    void ShootCannon()
    {
        if(GameManager.Instance.state != GameManager.GameState.Gameplay) {return;}

        Vector3 _mousePosition = GetMouseWorldPosition();
        Vector3 _aimDirection = (_mousePosition - _transform.position).normalized;
        SOpackages = playerPickup.GetSOPackages();
        
        if(SOpackages.Count > 0)
        {
            Transform shotPackageTransform = Instantiate
            (SOpackages[0].packagePrefab, 
            cannonEndPoint.position, 
            Quaternion.identity
            );

            shotPackageTransform.TryGetComponent<Package>(out Package shotPackage);
            shotPackage.MovePackage(_aimDirection);
            shotPackage.SetRequestingHouse(requestingHouses[0]);
            
            OnCannonShoot?.Invoke(this, new OnCannonShootEventArgs
            {
                e_cannonEndPointPosition = cannonEndPoint.position,
                e_shootPosition = _mousePosition,
                e_cannon = cannonTransform
            });

        }
    }

    void ShootRocket()
    {
        if(GameManager.Instance.state != GameManager.GameState.Gameplay) {return;}

        Vector3 _mousePosition = GetMouseWorldPosition();
        Vector3 _aimDirection = (_mousePosition - _transform.position).normalized;
        SOpackages = playerPickup.GetSOPackages();

        OnRocketShoot?.Invoke(this, new OnCannonShootEventArgs
            {
                e_cannonEndPointPosition = cannonEndPoint.position,
                e_shootPosition = _mousePosition,
                e_cannon = cannonTransform
            });
    }

    void MoveCannon()
    {
        if(GameManager.Instance.state != GameManager.GameState.Gameplay) {return;}
        
        Vector3 _mousePosition = GetMouseWorldPosition();
        Vector3 _aimDirection = (_mousePosition - _transform.position).normalized;
        float angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x)*Mathf.Rad2Deg;

        cannonTransform.eulerAngles = new Vector3(0f, 0f, angle);
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 _vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        _vec.z = 0f;
        return _vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera _worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, _worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 _screenPosition, Camera _worldCamera)
    {
        Vector3 _worldPosition = _worldCamera.ScreenToWorldPoint(_screenPosition);
        return _worldPosition;
    }
}
