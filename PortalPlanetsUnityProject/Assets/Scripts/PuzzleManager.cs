using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Serializable]
    public class PlanetPuzzle
    {
        public PuzzlesOnThePlanet planet;
        public bool planetSolved;
    }

    [SerializeField] private List<PlanetPuzzle> _planetsWithPuzzles = new List<PlanetPuzzle>();

    public void CheckIfPlanetAllPuzzlesAreSolved(PuzzlesOnThePlanet planetWithPuzzles)
    {
        var findPlanetIndex = _planetsWithPuzzles.FindIndex(planet =>
            planet.planet == planetWithPuzzles);

        for (int i = 0; i < _planetsWithPuzzles[findPlanetIndex].planet._puzzleTriggers.Count; i++)
        {
            if (_planetsWithPuzzles[findPlanetIndex].planet._puzzleTriggers.Exists(area => area.isPuzzleAreaFinished == false))
            {
                _planetsWithPuzzles[findPlanetIndex].planetSolved = false;
            }
            else
            {
                _planetsWithPuzzles[findPlanetIndex].planetSolved = true;
            }
        }

        CheckIfAllPlanetsAreSolved();
    }

    private void CheckIfAllPlanetsAreSolved()
    {
        if (_planetsWithPuzzles.Exists(planet => planet.planetSolved == false))
        {
            return;
        }

        FinishLevel();
    }

    private void FinishLevel()
    {
        Debug.Log("Level Finished");
    }

    // send stream from the planet to the core

    // fix the core

    // check if puzzles finished every time the puzzle is solved, so it can see whether someone broke the puzzle or not
}