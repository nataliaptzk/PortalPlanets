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

    public event Action<PuzzleTriggerArea, GameObject> OnPuzzleTriggerEnter;

    public void PuzzleTriggerEnter(PuzzleTriggerArea triggerArea, GameObject puzzleItem)
    {
        if (OnPuzzleTriggerEnter != null)
        {
            OnPuzzleTriggerEnter(triggerArea, puzzleItem);
        }
    }


    public event Action<PuzzleTriggerArea, GameObject> OnPuzzleTriggerExit;

    public void PuzzleTriggerExit(PuzzleTriggerArea triggerArea, GameObject puzzleItem)
    {
        if (OnPuzzleTriggerExit != null)
        {
            OnPuzzleTriggerExit(triggerArea, puzzleItem);
        }
    }
}