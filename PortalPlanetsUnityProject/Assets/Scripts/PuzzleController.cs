using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public int id;

    private void Start()
    {
        CustomEvents.current.onPuzzleTriggerEnter += OnPuzzleEnter;
    }

    private void OnPuzzleEnter(int areaId)
    {
        if (areaId == id) // check if it matches with the puzzleArea ID
        {
            Debug.Log("puzzle in area " + areaId);
        }
    }


    private void OnDestroy()
    {
        CustomEvents.current.onPuzzleTriggerEnter -= OnPuzzleEnter;

    }
}