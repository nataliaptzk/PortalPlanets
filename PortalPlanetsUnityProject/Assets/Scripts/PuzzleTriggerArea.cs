using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleTriggerArea : MonoBehaviour
{
    [Serializable]
    public class SinglePuzzle
    {
        public PuzzleTypes.Puzzles type;
        public bool isSolved;
    }

    public List<SinglePuzzle> multiPuzzles = new List<SinglePuzzle>();

    private void OnTriggerEnter(Collider other) // other is a puzzle item dropped/brought in the triggerArea
    {
        if (other is CapsuleCollider && other.CompareTag("PuzzleItem"))
        {
            CustomEvents.current.PuzzleTriggerEnter(this, other.gameObject); // dispatch event
        }
    }

    private void OnTriggerExit(Collider other) // other is a puzzle item taken away from the triggerArea
    {
        if (other is CapsuleCollider && other.CompareTag("PuzzleItem"))
        {
            CustomEvents.current.PuzzleTriggerExit(this, other.gameObject); // dispatch event
        }
    }
}