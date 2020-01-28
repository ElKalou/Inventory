using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventData : ScriptableObject
{
    public int attachedInt;

    public static GameEventData GetInstance()
    {
        return (GameEventData)CreateInstance(typeof(GameEventData));
    }
}
