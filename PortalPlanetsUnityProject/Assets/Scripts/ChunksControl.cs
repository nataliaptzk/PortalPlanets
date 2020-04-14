using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ChunksControl : MonoBehaviour
{
    [SerializeField] private List<Transform> _children;
    [SerializeField] private List<Vector3> _initialPosition;
    private CameraFollow _mainCamera;
    private PuzzleManager _puzzleManager;

    private void Awake()
    {
        _mainCamera = FindObjectOfType<CameraFollow>();
        _puzzleManager = FindObjectOfType<PuzzleManager>();
    }

    private void Start()
    {
        StartCoroutine(ExplodeChildren());
    }

    [ContextMenu("ReadChildren")]
    public void ReadChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _children.Add(transform.GetChild(i).transform);
            _initialPosition.Add(transform.GetChild(i).position);
        }
    }

    private IEnumerator ExplodeChildren()
    {
        _mainCamera.isFollowing = false;
        _mainCamera.lookAt.position = Vector3.zero;

        foreach (var planet in _puzzleManager.planetsWithPuzzles)
        {
            planet.planet.BeamOff(false);
        }

        yield return new WaitForSeconds(2.5f);


        // move children away from vector 0 normalised
        var middle = Vector3.zero;
        foreach (var child in _children)
        {
            var temp = (child.localPosition - middle).normalized;
            var random = Random.Range(1.2f, 2.5f);
            var newPosition = child.localPosition + temp * random;
            LeanTween.moveLocal(child.gameObject, newPosition, 2f).setEase(LeanTweenType.easeInOutElastic).setOnComplete(OnComplete);
        }
    }

    private void OnComplete()
    {
        StartCoroutine(OnCompleteWait());
    }

    private IEnumerator OnCompleteWait()
    {
        yield return new WaitForSeconds(1f);
        _mainCamera.isFollowing = true;
        _puzzleManager.TutorialScreenActive(true);
        _puzzleManager.ChangeTimeScale(0);
    }

    public void PutTogetherChildren()
    {
        _mainCamera.lookAt.position = Vector3.zero;

        for (int i = 0; i < _children.Count; i++)
        {
            LeanTween.move(_children[i].gameObject, _initialPosition[i], 2f).setEase(LeanTweenType.easeInOutElastic);
        }
    }
}