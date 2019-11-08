using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kInventory
{
    public class OpenCloseInventoryButton : MonoBehaviour, IEventSender<GameEvent>
    {
        [SerializeField] private GameEvent closeInventory = null;
        [SerializeField] private GameEvent openInventory = null;
        [SerializeField] private Inventory playerInventory = null;

        public void ClickOnButton()
        {
            if (playerInventory.isOpen)
                closeInventory.Raise();
            else if (!playerInventory.isOpen)
                SendEvent(openInventory);
        }


        public void SendEvent(GameEvent eventToSend)
        {
            eventToSend.dataToSend = playerInventory;
            openInventory.Raise();
        }
    }
}
