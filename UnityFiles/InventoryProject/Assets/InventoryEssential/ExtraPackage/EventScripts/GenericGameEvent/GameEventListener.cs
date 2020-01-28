using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameEventListener : ListenerBase<GameEvent>
{
    public attachedUnityEvent onReceiveEvent;

    protected override void OnDisable()
    {
        eventToReact.DeRegister(this);
    }

    protected override void OnEnable()
    {
        if (eventToReact == null)
            return;

        eventToReact.Register(this);
    }

    public override void OnReceiveEvent(GameEvent receivedEvent)
    {
        onReceiveEvent.Invoke(receivedEvent.dataToSend);
    }

    public static void AddComponentAtRunTime(GameObject _entity, GameEvent _eventToReact,
        UnityAction<ScriptableObject> _onInvoke)
    {
        GameEventListener newListener = _entity.AddComponent<GameEventListener>();
        newListener.eventToReact = _eventToReact;
        _eventToReact.Register(newListener);
        newListener.onReceiveEvent = new attachedUnityEvent();
        newListener.onReceiveEvent.AddListener(_onInvoke);
    }

    [Serializable]
    public class attachedUnityEvent : UnityEvent<ScriptableObject> { }

}








