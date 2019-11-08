using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kCaster
{

	public static castTo tryCast<castTo>(ScriptableObject ScriptableObjectToCast) where castTo : ScriptableObject
    {
        try
        {
            return (castTo)ScriptableObjectToCast;
        }
        catch (InvalidCastException)
        {
            Debug.LogError("cast failed");
            return null;
        }
    }
}
