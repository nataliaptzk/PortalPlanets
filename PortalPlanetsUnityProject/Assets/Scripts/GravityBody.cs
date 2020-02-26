using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {
	
   private GravityAttractor _planet;
   private Rigidbody _rb;
	
    void Awake () {
        _planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        _rb = GetComponent<Rigidbody> ();

        _rb.useGravity = false;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
	
    void FixedUpdate () {
        _planet.Attract(_rb);
    }
}