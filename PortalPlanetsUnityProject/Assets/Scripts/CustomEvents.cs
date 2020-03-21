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

    public event Action<int> onPuzzleTriggerEnter;

    public void PuzzleTriggerEnter(int id)
    {
        if (onPuzzleTriggerEnter != null)
        {
            onPuzzleTriggerEnter(id);
        }
    }
}