using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class EventBase : ScriptableObject
{
    public abstract void Raise();
}

