using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This class contains extenstion functions.
///  - Natalia Pietrzak
/// </summary>
public static class Extensions
{
    public static bool HasComponent<T>(this GameObject obj) where T : Component
    {
        return obj.GetComponent<T>() != null;
    }
}