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
            var itemPuzzle = other.GetComponent<PuzzleItem>();

            var temp = multiPuzzles.FindIndex(puzzle =>
                puzzle.type == itemPuzzle.type && !puzzle.isSolved);

            if (temp >= 0) // check if there's a puzzle in the puzzles list that matches the type of the brought item and is not solved
            {
                {
                    CustomEvents.current.PuzzleTriggerEnter(multiPuzzles[temp].type); // dispatch event
                    multiPuzzles[temp].isSolved = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) // other is a puzzle item taken away from the triggerArea
    {
        if (other is CapsuleCollider && other.CompareTag("PuzzleItem"))
        {
            var itemPuzzle = other.GetComponent<PuzzleItem>();

            var index = multiPuzzles.FindIndex(puzzle =>
                puzzle.type == itemPuzzle.type && !puzzle.isSolved);

            if (index >= 0) // check if there's a puzzle in the puzzles list that matches the type of the brought item and is not solved
            {
                CustomEvents.current.PuzzleTriggerExit(multiPuzzles[index].type); // dispatch event
                multiPuzzles[index].isSolved = true;
            }
        }
    }
}