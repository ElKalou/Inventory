using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kInventory
{
    public class InventoryEventData : ScriptableObject
    {
        public int posInInventory { get; private set; }

        public Inventory container { get; private set; }

        public ItemSlot itemSlot { get; private set; }

        public Vector2 slotUISize { get; private set; }

        public Vector3 worldPosition { get; private set; }

        public string attachedMessage { get; private set; }

        public int attachedInt { get; private set; }

        public void Initiate(ItemSlotDataSender maker, string _attachMesage = null)
        {
            posInInventory = maker.posInInventory;
            container = maker.container;
            itemSlot = maker.thisItemSlot;
            slotUISize = new Vector2(
                maker.GetComponent<RectTransform>().rect.width,
                maker.GetComponent<RectTransform>().rect.height);
            worldPosition = maker.transform.position;
            attachedMessage = _attachMesage;
        }

        public void Initiate(InventoryEventData toCopy, string _attachMesage = null, int _intToCarry = 0)
        {
            posInInventory = toCopy.posInInventory;
            container = toCopy.container;
            itemSlot = toCopy.itemSlot;
            worldPosition = toCopy.worldPosition;
            attachedMessage = _attachMesage;
        }

        public void Initiate(string _attachMesage = null, int _intToCarry = 0)
        {
            attachedMessage = _attachMesage;
            attachedInt = _intToCarry;
        }

        public void AddMessage(string _messageToSend)
        {
            attachedMessage = _messageToSend;
        }

        public static InventoryEventData GetInstance()
        { 
            return (InventoryEventData)CreateInstance(typeof(InventoryEventData));
        }

    }
}
