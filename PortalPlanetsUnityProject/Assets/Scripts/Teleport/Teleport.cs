using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This class allows the player to teleport. The new gravity attractor is assigned to the player and a potential carried item.
///  - Natalia Pietrzak
/// </summary>
public class Teleport : MonoBehaviour
{
    public Transform teleportDestination;

    [SerializeField] private AudioClip _teloportClip;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _teloportClip;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _audioSource.Play();
            other.transform.SetPositionAndRotation(teleportDestination.position, teleportDestination.rotation);
            other.gameObject.GetComponent<GravityBody>().planet = teleportDestination.transform.root.GetComponent<GravityAttractor>(); // find the PLANET parent and take it as gravity attractor point

            // Check if player is holding an item, if yes, change the point of gravity attraction to the target planet from teleport
            if (other.gameObject.GetComponent<ThirdPersonController>().PickUpSlot.childCount == 1)
            {
                other.gameObject.GetComponent<ThirdPersonController>().PickUpSlot.GetChild(0).GetComponent<GravityBody>().planet =
                    teleportDestination.transform.root.GetComponent<GravityAttractor>(); // find the PLANET parent and take it as gravity attractor point
            }
        }
    }
}