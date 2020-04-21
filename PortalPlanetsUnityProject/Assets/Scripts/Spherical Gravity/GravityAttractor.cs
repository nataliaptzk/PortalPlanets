using UnityEngine;
using System.Collections;

/// <summary>
///  A part of Spherical Gravity tutorial by Sebastian Lague
/// </summary>
public class GravityAttractor : MonoBehaviour
{
    [SerializeField] private float gravity = -9.8f;


    public void Attract(Rigidbody body)
    {
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 localUp = body.transform.up;

        body.AddForce(gravityUp * gravity);
        body.rotation = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;
    }
}