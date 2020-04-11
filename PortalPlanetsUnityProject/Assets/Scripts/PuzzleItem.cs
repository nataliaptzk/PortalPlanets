using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public PuzzleTypes.Puzzles type;

    private void Start()
    {
        CustomEvents.current.OnPuzzleTriggerEnter += OnPuzzleEnter;
        CustomEvents.current.OnPuzzleTriggerExit += OnPuzzleExit;
    }

    private void OnPuzzleEnter(PuzzleTriggerArea triggerArea, GameObject puzzleItem)
    {
        if (puzzleItem == gameObject)
        {
            Debug.Log("OnPuzzleEnter");
            var findIndex = triggerArea.multiPuzzles.FindIndex(puzzle =>
                puzzle.type == type && !puzzle.isSolved);

            if (findIndex >= 0) // check if there's a puzzle in the puzzles list that matches the type of the puzzle item and is not solved, if -1 = item not found
            {
                {
                    triggerArea.multiPuzzles[findIndex].isSolved = true;
                }
            }
        }
    }

    private void OnPuzzleExit(PuzzleTriggerArea triggerArea, GameObject puzzleItem)
    {
        if (puzzleItem == gameObject)
        {
            Debug.Log("OnPuzzleExit");
            var findIndex = triggerArea.multiPuzzles.FindIndex(puzzle =>
                puzzle.type == type && puzzle.isSolved);

            if (findIndex >= 0) // check if there's a puzzle in the puzzles list that matches the type of the puzzle item and is solved, if -1 = item not found
            {
                {
                    triggerArea.multiPuzzles[findIndex].isSolved = false;
                }
            }
        }
    }


    private void OnDestroy()
    {
        CustomEvents.current.OnPuzzleTriggerEnter -= OnPuzzleEnter;
        CustomEvents.current.OnPuzzleTriggerExit -= OnPuzzleExit;
    }
}