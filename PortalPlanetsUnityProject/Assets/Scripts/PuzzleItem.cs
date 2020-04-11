using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public PuzzleTypes.Puzzles type;

    private void Start()
    {
        CustomEvents.current.OnPuzzleTriggerEnter += OnPuzzleEnter;
        CustomEvents.current.OnPuzzleTriggerEnter += OnPuzzleExit;
    }

    private void OnPuzzleEnter(PuzzleTypes.Puzzles areaType)
    {
        if (areaType == type) // check if it matches with the puzzleArea type
        {
            Debug.Log("puzzle in area " + areaType);
        }
    }
    
    private void OnPuzzleExit(PuzzleTypes.Puzzles areaType)
    {
        if (areaType == type) // check if it matches with the puzzleArea type
        {
            Debug.Log("puzzle in area " + areaType);
        }
    }


    private void OnDestroy()
    {
        CustomEvents.current.OnPuzzleTriggerEnter -= OnPuzzleEnter;
        CustomEvents.current.OnPuzzleTriggerEnter -= OnPuzzleExit;

    }
}