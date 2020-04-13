using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanCamera : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private Vector3 _lookAt;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(_lookAt, transform.up, Time.deltaTime * _rotationSpeed);
        transform.LookAt(Vector3.zero);
    }
}