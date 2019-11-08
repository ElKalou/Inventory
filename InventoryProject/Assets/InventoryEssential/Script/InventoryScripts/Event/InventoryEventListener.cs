using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace kInventory
{
    public class InventoryEventListener : ListenerBase<InventoryEvent>
    {

        public attachedUnityEvent onReceiveEvent;

        public override void OnReceiveEvent(InventoryEvent receivedEvent)
        {
            onReceiveEvent.Invoke(receivedEvent.dataToSend);
        }

        protected override void OnEnable()
        {
            if (eventToReact == null)
                return;

            eventToReact.Register(this);
        }

        protected override void OnDisable()
        {
            eventToReact.Unregister(this);
        }

        public static void AddComponentAtRunTime(GameObject _entity, InventoryEvent _eventToReact, 
            UnityAction<InventoryEventData> _onInvoke)
        {
            InventoryEventListener newListener = _entity.AddComponent<InventoryEventListener>();
            newListener.eventToReact = _eventToReact;
            _eventToReact.Register(newListener);
            newListener.onReceiveEvent = new attachedUnityEvent();
            newListener.onReceiveEvent.AddListener(_onInvoke);
        }

        [System.Serializable]
        public class attachedUnityEvent : UnityEvent<InventoryEventData> { }

    }

}



