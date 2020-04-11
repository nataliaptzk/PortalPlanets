using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEvents : MonoBehaviour
{
    public static CustomEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<PuzzleTypes.Puzzles> OnPuzzleTriggerEnter;

    public void PuzzleTriggerEnter(PuzzleTypes.Puzzles type)
    {
        if (OnPuzzleTriggerEnter != null)
        {
            OnPuzzleTriggerEnter(type);
        }
    }
    
    
    public event Action<PuzzleTypes.Puzzles> OnPuzzleTriggerExit;

    public void PuzzleTriggerExit(PuzzleTypes.Puzzles type)
    {
        if (OnPuzzleTriggerExit != null)
        {
            OnPuzzleTriggerExit(type);
        }
    }
}