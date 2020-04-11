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

    public bool isPuzzleAreaFinished;

    private PuzzleManager _puzzleManager;

    public List<SinglePuzzle> multiPuzzles = new List<SinglePuzzle>();


    private void Awake()
    {
        _puzzleManager = FindObjectOfType<PuzzleManager>();
    }

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

    public void ChangeSolved(int index, bool value, PuzzlesOnThePlanet planet)
    {
        multiPuzzles[index].isSolved = value;

        isPuzzleAreaFinished = CheckIfPuzzleAreaFinished();
        _puzzleManager.CheckIfPlanetAllPuzzlesAreSolved(planet);
    }

    private bool CheckIfPuzzleAreaFinished()
    {
        for (int i = 0; i < multiPuzzles.Count; i++)
        {
            if (multiPuzzles[i].isSolved == false)
            {
                return false;
            }
        }

        return true;
    }
}