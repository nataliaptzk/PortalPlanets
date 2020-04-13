using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GravityBody))]
public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;

    [SerializeField] private float _backWalkSpeed;

    [SerializeField] private Transform _pickUpSlot;

    public Transform PickUpSlot => _pickUpSlot;
    public bool holdingItem;

    private bool _grounded;
    private float _verticalLookRotation;
    private Vector3 _moveAmount;
    private Vector3 _smoothMoveVelocity;
    private Rigidbody _rigidbody;


    void Awake()
    {
        holdingItem = false;
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y).normalized;

        Vector3 targetMoveAmount;
        if (y <= -1)
        {
            targetMoveAmount = moveDir * _backWalkSpeed;
        }
        else
        {
            targetMoveAmount = moveDir * _walkSpeed;
        }

        _moveAmount = Vector3.SmoothDamp(_moveAmount, targetMoveAmount, ref _smoothMoveVelocity, .15f);

        transform.Rotate(0, x * 360f * Time.deltaTime, 0, Space.Self);
    }


    void FixedUpdate()
    {
        Vector3 localMove = transform.TransformDirection(_moveAmount) * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + localMove);
    }
}