using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GravityBody))]
public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float backWalkSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask _groundedLayerMask;

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
            targetMoveAmount = moveDir * backWalkSpeed;
        }
        else
        {
            targetMoveAmount = moveDir * walkSpeed;
        }

        _moveAmount = Vector3.SmoothDamp(_moveAmount, targetMoveAmount, ref _smoothMoveVelocity, .15f);

        transform.Rotate(0, x * 360f * Time.deltaTime, 0);


        if (Input.GetButtonDown("Jump"))
        {
            if (_grounded)
            {
                _rigidbody.AddForce(transform.up * jumpForce);
            }
        }
    }


    void FixedUpdate()
    {
        Vector3 localMove = transform.TransformDirection(_moveAmount) * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + localMove);
    }


    private void OnCollisionStay(Collision other)
    {
        if ((_groundedLayerMask.value & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            _grounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if ((_groundedLayerMask.value & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            _grounded = false;
        }
    }
}