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

    public event Action<PuzzleTypes.Puzzles> onPuzzleTriggerEnter;

    public void PuzzleTriggerEnter(PuzzleTypes.Puzzles type)
    {
        if (onPuzzleTriggerEnter != null)
        {
            onPuzzleTriggerEnter(type);
        }
    }
}