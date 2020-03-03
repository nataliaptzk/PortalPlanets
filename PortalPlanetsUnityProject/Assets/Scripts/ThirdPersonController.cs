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

        transform.Rotate(0, x * 360f * Time.deltaTime, 0);


        if (Input.GetButtonDown("Jump"))
        {
            if (_grounded)
            {
                _rigidbody.AddForce(transform.up * _jumpForce);
            }
        }


        if (Input.GetMouseButtonUp(0) && _pickUpSlot.transform.childCount == 1)
        {
            ItemDrop(_pickUpSlot.transform.GetChild(0).gameObject);
        }
    }


    void FixedUpdate()
    {
        Vector3 localMove = transform.TransformDirection(_moveAmount) * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + localMove);
    }

    private void ItemPickUp(GameObject item)
    {
        if (_pickUpSlot.transform.childCount == 0)
        {
            item.transform.SetParent(_pickUpSlot);
            item.transform.localPosition = Vector3.zero;
            item.GetComponent<Rigidbody>().isKinematic = true;
            item.GetComponent<GravityBody>().enabled = false;
        }
    }

    private void ItemDrop(GameObject item)
    {
        if (_pickUpSlot.transform.childCount == 1)
        {
            _pickUpSlot.transform.GetChild(0).GetComponent<GravityBody>().enabled = true;
            _pickUpSlot.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            _pickUpSlot.transform.GetChild(0).SetParent(null);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButton(0))
        {
            if (other.gameObject.HasComponent<PickUpItem>())
            {
                ItemPickUp(other.gameObject);
            }
        }
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