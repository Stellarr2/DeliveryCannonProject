using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPackages : MonoBehaviour
{
    [SerializeField] PlayerPickup playerPickup;
    [SerializeField] PackageCannon cannon;
    [SerializeField] Transform iconContainer;
    [SerializeField] Transform iconTemplate;
    List<SOPackage> packages;

    void Awake()
    {
        packages = new List<SOPackage>();
    }

    void Start()
    {
        UpdateVisual();
    }

    void Update()
    {
        UpdateVisual();
    }

    void UpdateVisual()
    {
            int i = 0; //Declare i for the package index
            packages = playerPickup.GetSOPackages();

            foreach(Transform child in iconContainer)
            {
                if(child == iconTemplate) {continue;}
                Destroy(child.gameObject);
            }

            foreach(SOPackage soPackage in packages)
            {
                i++;
                Transform newIcon = Instantiate(iconTemplate, iconContainer);
                newIcon.TryGetComponent(out UIPackagesSingle uI);
                uI.SetIcon(soPackage, i-1); //subtract 1 because weird array stuff
            }
            i = 0; //set it back to 0 for safety

            if(packages.Count <= 0)
            {
                foreach(Transform child in iconContainer)
                {
                    if(child == iconTemplate) {continue;}
                    Destroy(child.gameObject);
                }
            }
        }
    }
