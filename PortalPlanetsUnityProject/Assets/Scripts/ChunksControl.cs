﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChunksControl : MonoBehaviour
{
    [SerializeField] private List<Transform> _children;
    [SerializeField] private List<Vector3> _initialPosition;

    [ContextMenu("ReadChildren")]
    public void ReadChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _children.Add(transform.GetChild(i).transform);
            _initialPosition.Add(transform.GetChild(i).position);
        }
    }

    [ContextMenu("ExplodeChildren")]
    public void ExplodeChildren()
    {
        // move children away from vector 0 normalised
        var middle = Vector3.zero;
        foreach (var child in _children)
        {
            var temp = (child.localPosition - middle).normalized;

            var random = Random.Range(1.2f, 2.5f);
            child.localPosition += (temp * random);
        }
    }

    [ContextMenu("PutTogetherChildren")]
    public void PutTogetherChildren()
    {
        for (int i = 0; i < _children.Count; i++)
        {
            _children[i].position = _initialPosition[i];
        }
    }
}