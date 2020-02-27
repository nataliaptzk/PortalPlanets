using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportDestination;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetPositionAndRotation(teleportDestination.position, teleportDestination.rotation);  
            other.gameObject.GetComponent<GravityBody>().planet = teleportDestination.transform.root.GetComponent<GravityAttractor>();
        }
    }
}