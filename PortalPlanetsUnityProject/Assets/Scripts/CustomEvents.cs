using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This class contains declaration of custom events in the game.
///  - Natalia Pietrzak
/// </summary>
public class CustomEvents : MonoBehaviour
{
    public static CustomEvents current;
    public event Action<PuzzleTriggerArea, GameObject> OnPuzzleTriggerEnter;
    public event Action<PuzzleTriggerArea, GameObject> OnPuzzleTriggerExit;

    private void Awake()
    {
        current = this;
    }


    public void PuzzleTriggerEnter(PuzzleTriggerArea triggerArea, GameObject puzzleItem)
    {
        if (OnPuzzleTriggerEnter != null)
        {
            OnPuzzleTriggerEnter(triggerArea, puzzleItem);
        }
    }


    public void PuzzleTriggerExit(PuzzleTriggerArea triggerArea, GameObject puzzleItem)
    {
        if (OnPuzzleTriggerExit != null)
        {
            OnPuzzleTriggerExit(triggerArea, puzzleItem);
        }
    }
}