using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GravityBody))]
public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _backWalkSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundedLayerMask;
    [SerializeField] private Transform _pickUpSlot;

    public Transform PickUpSlot => _pickUpSlot;

    private bool _grounded;
    private float _verticalLookRotation;
    private Vector3 _moveAmount;
    private Vector3 _smoothMoveVelocity;
    private Rigidbody _rigidbody;


    void Awake()
    {
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

        if (Input.GetButtonDown("Jump"))
        {
            if (_grounded)
            {
                _rigidbody.AddForce(transform.up * _jumpForce);
            }
        }

        if (Input.GetMouseButtonUp(0) && _pickUpSlot.transform.childCount == 1)
        {
            _pickUpSlot.transform.GetChild(0).gameObject.GetComponent<PickUpItem>().ItemDrop();
        }
    }


    void FixedUpdate()
    {
        Vector3 localMove = transform.TransformDirection(_moveAmount) * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + localMove);
    }


    private void OnCollisionStay(Collision other)
    {
        if ((_groundedLayerMask.value & 1 << other.gameObject.layer) == 1 << other.gameObject.layer) // check for ground
        {
            _grounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if ((_groundedLayerMask.value & 1 << other.gameObject.layer) == 1 << other.gameObject.layer) // check for ground
        {
            _grounded = false;
        }
    }
}