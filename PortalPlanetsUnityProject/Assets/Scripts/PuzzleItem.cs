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
            var findIndex = triggerArea.multiPuzzles.FindIndex(puzzle =>
                puzzle.type == type && !puzzle.isSolved);

            if (findIndex >= 0) // check if there's a puzzle in the puzzles list that matches the type of the puzzle item and is not solved, if -1 = item not found
            {
                {
                    triggerArea.ChangeSolved(findIndex, true, gameObject.GetComponent<GravityBody>().planet.gameObject.GetComponent<PuzzlesOnThePlanet>());
                    var newPosition = triggerArea.AssignSlot(findIndex, true);
                    triggerArea.multiPuzzles[findIndex].snappedItem = gameObject;
                    gameObject.transform.SetPositionAndRotation(newPosition, Quaternion.identity);
                    gameObject.GetComponent<GravityBody>().enabled = false;
                    gameObject.GetComponent<PickUpItem>().ItemDrop(false);
                }
            }
        }
    }

    private void OnPuzzleExit(PuzzleTriggerArea triggerArea, GameObject puzzleItem)
    {
        if (puzzleItem == gameObject)
        {
            var findIndex = triggerArea.multiPuzzles.FindIndex(puzzle =>
                puzzle.type == type && puzzle.isSolved);

            if (findIndex >= 0 && triggerArea.multiPuzzles[findIndex].snappedItem == gameObject
            ) // check if there's a puzzle in the puzzles list that matches the type of the puzzle item and is solved, if -1 = item not found
            {
                {
                    triggerArea.ChangeSolved(findIndex, false, gameObject.GetComponent<GravityBody>().planet.gameObject.GetComponent<PuzzlesOnThePlanet>());
                    triggerArea.AssignSlot(findIndex, false);
                    triggerArea.multiPuzzles[findIndex].snappedItem = null;

                    //gameObject.GetComponent<GravityBody>().enabled = true;
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