using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventDataCatcher : EventDataCatcherBase<GameEvent, GameEventListener, ScriptableObject>
{
    protected override void InstantiateDisableDataUserListeners()
    {
        for (int i = 0; i < disableDataUserOn.Count; i++)
        {
            GameEventListener.AddComponentAtRunTime(gameObject, disableDataUserOn[i], DisableDataUser);
        }
    }

    protected override void InstantiateSuspendDataCatcherListeners()
    {
        for (int i = 0; i < suspendOn.Count; i++)
        {
            GameEventListener.AddComponentAtRunTime(gameObject, suspendOn[i], SuspendDataTransmission);
        }
    }

    protected override void InstantiateTransmitDataListeners()
    {
        for (int i = 0; i < transmitDataOn.Count; i++)
        {
            GameEventListener.AddComponentAtRunTime(gameObject, transmitDataOn[i], TransmitDataToUser);
        }
    }

    protected override void InstantiateUnsuspendDataCatcherListeners()
    {
        for (int i = 0; i < unsuspendOn.Count; i++)
        {
            GameEventListener.AddComponentAtRunTime(gameObject, unsuspendOn[i], UnsuspendDataTransmission);
        }
    }
}
