using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kInventory
{

    [CreateAssetMenu(menuName = "Event/InventoryEvent")]
    public class InventoryEvent : EventBase
    {

        private List<InventoryEventListener> gameEventListeners = new List<InventoryEventListener>();
        public InventoryEventData dataToSend { get; set; }

        public override void Raise()
        {
            for (int i = gameEventListeners.Count - 1; i >= 0; i--)
            {
                gameEventListeners[i].OnReceiveEvent(this);
            }
        }

        public void Register(InventoryEventListener listenerToAdd)
        {
            if (!gameEventListeners.Contains(listenerToAdd))
                gameEventListeners.Add(listenerToAdd);
        }

        public void Unregister(InventoryEventListener listenerToRemove)
        {
            if (gameEventListeners.Contains(listenerToRemove))
                gameEventListeners.Remove(listenerToRemove);
        }
    }
}



