﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
///  This class manages the puzzle trigger area. It checks for puzzle item collisions. It also sets up the particles for the puzzle area.
///  - Natalia Pietrzak
/// </summary>
public class PuzzleTriggerArea : MonoBehaviour
{
    [Serializable]
    public class SinglePuzzle
    {
        public PuzzleTypes.Puzzles type;
        public bool isSolved;
        public GameObject snappedItem;
    }

    [Serializable]
    public class Slots
    {
        public GameObject slot;
        public bool isEmpty;

        public Slots(GameObject slot, bool isEmpty)
        {
            this.slot = slot;
            this.isEmpty = isEmpty;
        }
    }

    public bool isPuzzleAreaFinished;

    private PuzzleManager _puzzleManager;

    public List<SinglePuzzle> multiPuzzles = new List<SinglePuzzle>();

    public List<Slots> slotsForItems = new List<Slots>();

    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] List<Color> _colors = new List<Color>();
    [SerializeField] List<Material> _materials = new List<Material>();

    private void Awake()
    {
        _puzzleManager = FindObjectOfType<PuzzleManager>();
        _puzzleManager.puzzleCount++;
        PopulateSlots();
    }


    private void PopulateSlots()
    {
        Vector3 centre = transform.GetChild(0).transform.position;
        Quaternion rotation = transform.GetChild(0).transform.rotation;

        if (multiPuzzles.Count > 1)
        {
            for (int i = 0; i < multiPuzzles.Count; i++)
            {
                int angle = 360 / multiPuzzles.Count * i;
                Vector3 pos = NextPositionInCircle(centre, 1.0f, angle);

                GameObject slot = Instantiate(_slotPrefab, pos, rotation, transform.GetChild(0).transform);
                slotsForItems.Add(new Slots(slot, true));

                ChangeParticleColour(slot, i);
            }

            transform.GetChild(0).transform.localRotation = transform.rotation;
        }
        else if (multiPuzzles.Count == 1)
        {
            GameObject slot = Instantiate(_slotPrefab, centre, rotation, transform.GetChild(0).transform);
            slotsForItems.Add(new Slots(slot, true));

            ChangeParticleColour(slot, 0);
        }
    }

    private void ChangeParticleColour(GameObject slot, int i)
    {
        var particleSystems = slot.GetComponentsInChildren<ParticleSystemRenderer>();

        switch (multiPuzzles[i].type)
        {
            case PuzzleTypes.Puzzles.FIRE:
            {
                foreach (var module in particleSystems)
                {
                    module.trailMaterial = _materials[0];
                    module.material = _materials[5];
                }

                break;
            }
            case PuzzleTypes.Puzzles.WIND:
            {
                foreach (var module in particleSystems)
                {
                    module.trailMaterial = _materials[1];
                    module.material = _materials[6];
                }

                break;
            }
            case PuzzleTypes.Puzzles.EARTH:
            {
                foreach (var module in particleSystems)
                {
                    module.trailMaterial = _materials[2];
                    module.material = _materials[7];
                }

                break;
            }
            case PuzzleTypes.Puzzles.WATER:
            {
                foreach (var module in particleSystems)
                {
                    module.trailMaterial = _materials[3];
                    module.material = _materials[8];
                }

                break;
            }
            case PuzzleTypes.Puzzles.AETHER:
            {
                foreach (var module in particleSystems)
                {
                    module.trailMaterial = _materials[4];
                    module.material = _materials[9];
                }

                break;
            }
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

    public Vector3 AssignSlot(int i, bool value)
    {
        slotsForItems[i].isEmpty = value;

        return slotsForItems[i].slot.transform.position;
    }
}