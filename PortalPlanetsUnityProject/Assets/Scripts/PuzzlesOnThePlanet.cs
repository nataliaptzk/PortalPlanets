using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlesOnThePlanet : MonoBehaviour
{
    public List<PuzzleTriggerArea> _puzzleTriggers = new List<PuzzleTriggerArea>();
    public bool isBeamOn;

    private LineRenderer _beam;
    private Vector3 _from, _to;
    [SerializeField] private float _time;
    private CameraFollow _camera;
    private PuzzleManager _puzzleManager;

    [SerializeField] private AudioClip _beamOnAudioClip;
    [SerializeField] private AudioClip _beamOffAudioClip;
    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _camera = Camera.main.GetComponent<CameraFollow>();
        _puzzleManager = FindObjectOfType<PuzzleManager>();
        _from = new Vector3(0, 0, 0);
        _to = new Vector3(0, 0, 10);
        _beam = gameObject.GetComponentInChildren<LineRenderer>();
        isBeamOn = true;
    }


    public void BeamOn()
    {
        isBeamOn = true;
        
        _audioSource.clip = _beamOnAudioClip;
        _audioSource.Play();
        
        _camera.lookAt.position = (Vector3.zero + gameObject.transform.position) / 2;
        _camera.isFollowing = false;
        LeanTween.value(gameObject, _from, _to, _time).setOnUpdate((Vector3 val) => { _beam.SetPosition(1, val); }).setEase(LeanTweenType.easeInQuart).setOnComplete(OnComplete);
    }

    private void OnComplete()
    {
        _camera.isFollowing = true;
        _puzzleManager.CheckIfAllPlanetsAreSolved();
    }

    public void BeamOff(bool value)
    {
        _audioSource.clip = _beamOffAudioClip;
        _audioSource.Play();
        _camera.lookAt.position = (Vector3.zero + gameObject.transform.position) / 2;
        _camera.isFollowing = false;

        if (value)
        {
            LeanTween.value(gameObject, _to, _from, _time).setOnUpdate((Vector3 val) => { _beam.SetPosition(1, val); }).setEase(LeanTweenType.easeInQuart).setOnComplete(OnComplete);
        }
        else
        {
            LeanTween.value(gameObject, _to, _from, _time).setOnUpdate((Vector3 val) => { _beam.SetPosition(1, val); }).setEase(LeanTweenType.easeInQuart);
        }

        isBeamOn = false;
    }
}