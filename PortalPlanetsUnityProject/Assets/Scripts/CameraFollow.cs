﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _cameraTargetToFollow;
    [SerializeField] private Transform _lookAt;
    private float smoothSpeed = 0.125f;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _offset;
    private Vector3 _mouseScrollOffset;
    [SerializeField] private float _offsetA, _offsetB, _mouseSensitivity;

    public bool isFollowing;

    public Transform lookAt
    {
        get => _lookAt;
        set => _lookAt = value;
    }

    private void Start()
    {
        lookAt.position = Vector3.zero;
        isFollowing = false;
    }

    private void Update()
    {
        var i = Input.GetAxis("Mouse ScrollWheel"); // -0.4 scroll down 0.4 scroll up
        _offsetA += i * _mouseSensitivity * -1;
        _offsetA = Mathf.Clamp(_offsetA, 1f, 25f);
    }

    void FixedUpdate()
    {
        if (isFollowing)
        {
            Transform myTransform = transform;

            _mouseScrollOffset = new Vector3();


            _offset = (_cameraTargetToFollow.transform.up.normalized * _offsetA + -_cameraTargetToFollow.transform.forward.normalized * _offsetB) + _mouseScrollOffset;
            Vector3 newPos = _cameraTargetToFollow.position + _offset;
            Vector3 smoothedPos = Vector3.SmoothDamp(myTransform.position, newPos, ref _velocity, smoothSpeed);

            transform.position = smoothedPos;

            // Version 1 of Camera Follow: Camera is always up to the world
            transform.LookAt(_cameraTargetToFollow, _cameraTargetToFollow.transform.up);
        }
        else
        {
            Transform myTransform = transform;

            _mouseScrollOffset = new Vector3();


            _offset = lookAt.transform.up.normalized * 300f;
            //Vector3 newPos = (lookAt.position - lookAt.up * -250f) + _offset;
            Vector3 newPos = (lookAt.position + _cameraTargetToFollow.position + _offset);
            Vector3 smoothedPos = Vector3.SmoothDamp(myTransform.position, newPos, ref _velocity, smoothSpeed);

            transform.position = smoothedPos;

            // Version 1 of Camera Follow: Camera is always up to the world
            transform.LookAt(lookAt, lookAt.transform.up);
        }
    }
}