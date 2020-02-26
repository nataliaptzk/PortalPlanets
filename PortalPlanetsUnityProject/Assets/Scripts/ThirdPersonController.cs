using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GravityBody))]
public class ThirdPersonController : MonoBehaviour
{
    // public vars
    public float walkSpeed;
    public float backWalkSpeed;
    public float jumpForce;
    public LayerMask groundedMask;

    // System vars
    private bool _grounded;
    private Vector3 _moveAmount;

    private Vector3 _smoothMoveVelocity;

    private float _verticalLookRotation;

    private Rigidbody _rigidbody;


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 targetMoveAmount;
        Vector3 moveDir = new Vector3(x, 0, y).normalized;

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

        CheckGround();
    }

    private void CheckGround() // todo change this to oncolllison enter etc
    {
        // Grounded check
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask))
        {
            _grounded = true;
        }
        else
        {
            _grounded = false;
        }
    }

    void FixedUpdate()
    {
        Vector3 localMove = transform.TransformDirection(_moveAmount) * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + localMove);
    }
}