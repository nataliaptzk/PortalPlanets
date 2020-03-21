using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTriggerArea : MonoBehaviour
{
    public int id;

    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider && other.CompareTag("PuzzleItem"))
        {
            if (other.GetComponent<PuzzleController>().id == id)
            {
                CustomEvents.current.PuzzleTriggerEnter(id); // dispatch event
            }
        }
    }
}