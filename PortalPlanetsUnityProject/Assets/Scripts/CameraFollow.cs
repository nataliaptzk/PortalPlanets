using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _cameraTargetToFollow;

    private float smoothSpeed = 0.125f;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _offset;

    void FixedUpdate()
    {
        Transform myTransform = transform;
        _offset = (_cameraTargetToFollow.transform.up.normalized * 10 + -_cameraTargetToFollow.transform.forward.normalized*5);
        Vector3 newPos = _cameraTargetToFollow.position + _offset;
        Vector3 smoothedPos = Vector3.SmoothDamp(myTransform.position, newPos, ref _velocity, smoothSpeed);

        transform.position = smoothedPos;
        // transform.rotation = Quaternion.FromToRotation(myTransform.up, (_cameraTargetToFollow.transform.position - myTransform.position).normalized) * myTransform.rotation;

        // Version 1 of Camera Follow: Camera is always up to the world
        transform.LookAt(_cameraTargetToFollow, _cameraTargetToFollow.transform.up);

        // Version  of Camera Follow: Camera is always up to the player
        // transform.LookAt(_cameraTargetToFollow);
    }
}