using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTriggerArea : MonoBehaviour
{
    public PuzzleTypes.Puzzles type;

    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider && other.CompareTag("PuzzleItem"))
        {
            if (other.GetComponent<PuzzleItem>().type == type)
            {
                CustomEvents.current.PuzzleTriggerEnter(type); // dispatch event
            }
        }
    }
}