using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, new Vector3(1.2f, 1.2f), 0.2f).setEase(LeanTweenType.easeOutExpo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, new Vector3(1f, 1f), 0.2f).setEase(LeanTweenType.easeOutExpo);
    }
}