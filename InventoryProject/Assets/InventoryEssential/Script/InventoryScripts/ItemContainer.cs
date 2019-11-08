using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kInventory
{
    [RequireComponent(typeof(Interactable))]
    public class ItemContainer : MonoBehaviour
    {
        [Header("This ItemContainer inventory")]
        public Inventory inventory;
        [Header("Event to send")]
        public GameEvent openInventory;

        public void OnReceiveInteractEvent()
        {
            openInventory.dataToSend = inventory;
            openInventory.Raise();
        }
    }
}
