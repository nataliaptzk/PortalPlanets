using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI _puzzlesCountUI;

    public int puzzleCount;
    public int puzzleSolvedCount;

    private void Start()
    {
        DisplayPuzzleAmount();
    }

    private void DisplayPuzzleAmount()
    {
        _puzzlesCountUI.text = puzzleSolvedCount + " / " + puzzleCount;
    }

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


        CheckHowManyAreSolvedOnThePlanet();

        DisplayPuzzleAmount();
        CheckIfAllPlanetsAreSolved();
    }

    private void CheckHowManyAreSolvedOnThePlanet()
    {
        int solvedCount = 0;


        for (int i = 0; i < _planetsWithPuzzles.Count; i++)
        {
            for (int j = 0; j < _planetsWithPuzzles[i].planet._puzzleTriggers.Count; j++)
            {
                if (_planetsWithPuzzles[i].planet._puzzleTriggers[j].isPuzzleAreaFinished)
                {
                    solvedCount++;
                }
            }
        }

        puzzleSolvedCount = solvedCount;
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