﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This class contains the functions to animate an object.
///  - Natalia Pietrzak
/// </summary>
public class MessageBoxAnimation : MonoBehaviour
{
    public void Appear()
    {
        LeanTween.scale(gameObject, new Vector3(1f, 1f), 0.4f).setEase(LeanTweenType.easeOutExpo);
    }

    public void Disappear()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0), 0.4f).setEase(LeanTweenType.easeOutExpo);
    }
}