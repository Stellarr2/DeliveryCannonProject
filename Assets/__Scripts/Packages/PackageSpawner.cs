using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    [Tooltip("Scriptable Object containing a list of all Package Scriptable Objects")]
    [SerializeField] SOPackageList soPackageList;

    [Tooltip("Empty object to hold all the packages")]
    [SerializeField] Transform packageContainer;

    [Tooltip("Reference for spawning packages")]
    [SerializeField] Transform spawnObject;

    [Tooltip("Minimum and Maximum time to spawn a package")]
    [SerializeField] float spawnTimeMin, spawnTimeMax;

    [Tooltip("Vector3 containing values to randomize the spawn area")]
    [SerializeField] Vector3 spawnArea;

    Transform _transform;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawnObject.position, spawnArea);
    }
    void Awake()
    {
        _transform = transform;
    }

    void Start()
    {
        House.OnAnyPackageRequested += House_OnAnyPackageRequested;
    }

    void House_OnAnyPackageRequested(object sender, House.OnPackageRequestedEventArgs e)
    {
        StartCoroutine(SpawnPackage(e.e_soPackage, e.e_requestingHouse));
    }

    IEnumerator SpawnPackage(SOPackage soPackage, House house)
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(spawnTimeMin, spawnTimeMax));
        Transform packageTransform = Instantiate
        (soPackage.packagePrefab, 
            new Vector3
            (
                spawnObject.position.x + UnityEngine.Random.Range(-spawnArea.x, spawnArea.x), //x
                spawnObject.position.y + UnityEngine.Random.Range(-spawnArea.y, 0.5f), //y
                0f //z
            ),
            Quaternion.identity,
            packageContainer
        );

        if(packageTransform.TryGetComponent(out Package package))
        {
            package.SetRequestingHouse(house); //Setting requested house for pointer UI
        }
    }
}
