using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal;
    public MeshRenderer screen;
    private Camera _playerCamera;
    private Camera _portalCamera;
    private RenderTexture _viewTexture;


    
    
    /*private void Awake()
    {
        _playerCamera = Camera.main;
        _portalCamera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        Render();

    }

    void CreateViewTexture()
    {
        if (_viewTexture == null || _viewTexture.width != Screen.width || _viewTexture.height != Screen.height)
        {
            if (_viewTexture != null)
            {
                _viewTexture.Release();
            }

            _viewTexture = new RenderTexture(Screen.width, Screen.height, 0);
            _portalCamera.targetTexture = _viewTexture;
            linkedPortal.screen.material.SetTexture("_MainTex", _viewTexture);
        }
    }

    private void Render()
    {
        CreateViewTexture();

        var m = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix * _playerCamera.transform.localToWorldMatrix;
        _portalCamera.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
        _portalCamera.Render();

    }*/
}