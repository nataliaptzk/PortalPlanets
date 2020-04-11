using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public List<GameObject> slotsForItems = new List<GameObject>();

    [SerializeField] private GameObject _slotPrefab;

    private void Awake()
    {
        _puzzleManager = FindObjectOfType<PuzzleManager>();
        PopulateSlots();
    }


    private void PopulateSlots()
    {
        Vector3 centre = new Vector3(0, 0, 0);


        if (multiPuzzles.Count > 1)
        {
            for (int i = 0; i < multiPuzzles.Count; i++)
            {
                int angle = 360 / multiPuzzles.Count * i;
                Vector3 pos = NextPositionInCircle(centre, 1.0f, angle);

                GameObject slot = Instantiate(_slotPrefab, pos, Quaternion.identity, gameObject.transform);
                slotsForItems.Add(slot);
            }
        }
        else if (multiPuzzles.Count == 1)
        {

            GameObject slot = Instantiate(_slotPrefab, centre, Quaternion.identity, gameObject.transform);
            slotsForItems.Add(slot);
        }
    }

    private Vector3 NextPositionInCircle(Vector3 centre, float radius, int angle)
    {
        Vector3 positionInCircle;
        positionInCircle.x = centre.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        positionInCircle.y = centre.y;
        positionInCircle.z = centre.z + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        return positionInCircle;
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