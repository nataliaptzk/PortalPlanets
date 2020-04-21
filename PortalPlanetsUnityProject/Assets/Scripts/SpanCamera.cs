using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This class updates the position of the camera in the main menu.
///  - Natalia Pietrzak
/// </summary>
public class SpanCamera : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private Vector3 _lookAt;


    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(_lookAt, transform.up, Time.deltaTime * _rotationSpeed);
        transform.LookAt(Vector3.zero);
    }
}