using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class InputEventListener : ListenerBase<InputEvent>
{
    public attachedUnityEvent onReceiveEvent;

    private InputEventController controller;

    private void Start()
    {
        controller = GetComponentInParent<InputEventController>();
    }

    public override void OnReceiveEvent(InputEvent receivedEvent)
    {
        if(controller == null || controller.isMaster)
            onReceiveEvent.Invoke(receivedEvent.dataToSend);
    }

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

    public static void AddComponentAtRunTime(GameObject _entity, InputEvent _eventToReact,
        UnityAction<ScriptableObject> _onInvoke)
    {
        InputEventListener newListener = _entity.AddComponent<InputEventListener>();
        newListener.eventToReact = _eventToReact;
        _eventToReact.Register(newListener);
        newListener.onReceiveEvent = new attachedUnityEvent();
        newListener.onReceiveEvent.AddListener(_onInvoke);
    }

    [Serializable]
    public class attachedUnityEvent : UnityEvent<ScriptableObject> { }

}
