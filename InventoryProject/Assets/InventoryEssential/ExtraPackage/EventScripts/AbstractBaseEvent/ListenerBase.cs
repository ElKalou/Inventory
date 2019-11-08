using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class ListenerBase<EventFamily> : MonoBehaviour
    where EventFamily : EventBase
{
    public EventFamily eventToReact;

    public abstract void OnReceiveEvent(EventFamily receivedEvent);

    protected abstract void OnEnable();

    protected abstract void OnDisable();

    public static IEnumerator WaitForPossibleEvent(int frameToWait)
    {
        int i = 0;
        while (i < frameToWait)
        {
            i++;
            yield return null;
        }
    }
}

