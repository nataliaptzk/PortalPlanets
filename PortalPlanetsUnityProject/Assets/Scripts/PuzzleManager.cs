using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

/// <summary>
///  This class manages all puzzles in the game. It hold references to the puzzles and manages the state of the game.
///  - Natalia Pietrzak
/// </summary>
public class PuzzleManager : MonoBehaviour
{
    [Serializable]
    public class PlanetPuzzle
    {
        public PuzzlesOnThePlanet planet;
        public bool planetSolved;
    }

    public List<PlanetPuzzle> planetsWithPuzzles = new List<PlanetPuzzle>();
    [SerializeField] private TextMeshProUGUI _puzzlesCountUI;
    [SerializeField] private GameObject _tutorialScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _gameUI;
    private CameraFollow _camera;
    private ChunksControl _chunksControl;
    private PauseMenu _pauseMenu;

    public int puzzleCount;
    public int puzzleSolvedCount;

    // public 

    private void Awake()
    {
        _pauseMenu = FindObjectOfType<PauseMenu>();
        _camera = Camera.main.GetComponent<CameraFollow>();
        _chunksControl = FindObjectOfType<ChunksControl>();
    }

    private void Start()
    {
        GameScreenActive(false);
        DisplayPuzzleAmount();
    }

    private void DisplayPuzzleAmount()
    {
        _puzzlesCountUI.text = puzzleSolvedCount + " / " + puzzleCount;
    }

    public void CheckIfPlanetAllPuzzlesAreSolved(PuzzlesOnThePlanet planetWithPuzzles)
    {
        var findPlanetIndex = planetsWithPuzzles.FindIndex(planet =>
            planet.planet == planetWithPuzzles);

        for (int i = 0; i < planetsWithPuzzles[findPlanetIndex].planet._puzzleTriggers.Count; i++)
        {
            if (planetsWithPuzzles[findPlanetIndex].planet._puzzleTriggers.Exists(area => area.isPuzzleAreaFinished == false))
            {
                planetsWithPuzzles[findPlanetIndex].planetSolved = false;
                if (planetWithPuzzles.isBeamOn)
                {
                    planetWithPuzzles.BeamOff(true);
                }
            }
            else
            {
                planetsWithPuzzles[findPlanetIndex].planetSolved = true;
                if (!planetWithPuzzles.isBeamOn)
                {
                    planetWithPuzzles.BeamOn();
                }
            }
        }


        CheckHowManyAreSolvedOnThePlanet();

        DisplayPuzzleAmount();
    }

    private void CheckHowManyAreSolvedOnThePlanet()
    {
        int solvedCount = 0;


        for (int i = 0; i < planetsWithPuzzles.Count; i++)
        {
            for (int j = 0; j < planetsWithPuzzles[i].planet._puzzleTriggers.Count; j++)
            {
                if (planetsWithPuzzles[i].planet._puzzleTriggers[j].isPuzzleAreaFinished)
                {
                    solvedCount++;
                }
            }
        }

        puzzleSolvedCount = solvedCount;
    }

    public void CheckIfAllPlanetsAreSolved()
    {
        if (planetsWithPuzzles.Exists(planet => planet.planetSolved == false))
        {
            return;
        }

        StartCoroutine(FinishLevel());
    }

    private IEnumerator FinishLevel()
    {
        _camera.isFollowing = false;
        _camera.lookAt.position = Vector3.zero;
        GameScreenActive(false);
        _chunksControl.PutTogetherChildren();
        yield return new WaitForSeconds(2);
        //open win screen
        WinScreenActive(true);

        Debug.Log("Level Finished");
    }

    public void TutorialScreenActive(bool value)
    {
        _tutorialScreen.SetActive(value);
    }

    public void WinScreenActive(bool value)
    {
        _winScreen.SetActive(value);
    }

    public void GameScreenActive(bool value)
    {
        _gameUI.SetActive(value);
    }

    public void ChangeTimeScale(int value)
    {
        Time.timeScale = value;
    }
}