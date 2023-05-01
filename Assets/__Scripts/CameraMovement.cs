using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{   
    [Header("Zoom Stats")]
    [Tooltip("How fast the camera zooms")]
    [SerializeField] float zoomSpeed;
    [Tooltip("Minimum amount of zoom the camera can have")]
    [SerializeField] float minZoom;
    [Tooltip("Maximum amount of zoom the camera can have")]
    [SerializeField] float maxZoom;

    CinemachineVirtualCamera virtualCamera;
    Transform _transform;
    float zoomAmount;

    void Awake()
    {
        _transform = transform;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Start()
    {
        InputManager.Instance.OnQuickZoomed += InputManager_OnQuickZoomed;
    }

    void InputManager_OnQuickZoomed(object sender, EventArgs e)
    {
        if(virtualCamera.m_Lens.OrthographicSize <= minZoom)
        {
            virtualCamera.m_Lens.OrthographicSize = maxZoom;
        }
        else
        {
            virtualCamera.m_Lens.OrthographicSize = minZoom;
        }

    }

    void Update()
    {
        Zoomcamera();
    }

    void Zoomcamera()
    {
        zoomAmount = InputManager.Instance.GetZoomVector().y;

        virtualCamera.m_Lens.OrthographicSize += zoomAmount*zoomSpeed*Time.deltaTime;

        if(virtualCamera.m_Lens.OrthographicSize > maxZoom)
        {
            virtualCamera.m_Lens.OrthographicSize = maxZoom;
        }

        if(virtualCamera.m_Lens.OrthographicSize < minZoom)
        {
            virtualCamera.m_Lens.OrthographicSize = minZoom;
        }
    }
}
