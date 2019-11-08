using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kInventory
{
    public class FastDropPanel : MonoBehaviour, IPanel, IQuantityAsker, IStringMessageSender, 
        IItemMover, IEventDataUser<InventoryEventData>
    {

        [Header("GameEvents to send to other panels")]
        [SerializeField]
        private InventoryEvent askForQuantity = null;
        [SerializeField]
        private InventoryEvent sendMessage = null;

        private Inventory startInventory;
        private Inventory endInventory;
        private InventoryEventData dataInput;

        private InventoryEventData dataOutput;

        private bool waitingForAnswer;

        public void ReceiveData(InventoryEventData _dataInput)
        {
            if (_dataInput.itemSlot.Item == null)
                return;

            startInventory = _dataInput.container;
            dataInput = _dataInput;
            endInventory = FindOtherInventory();
            if (_dataInput.itemSlot.Quantity == 1)
                MoveItem(_dataInput.itemSlot.Quantity);
            else
                AskForQuantity();
        }

        private Inventory FindOtherInventory()
        {
            Inventory[] openedInventory = GetComponentInParent<PanelManagerLink>().GetOpenedInventory();
            if (openedInventory[0] != startInventory)
                return openedInventory[0];
            else
                return openedInventory[1];
        }

        public void AskForQuantity()
        {
            waitingForAnswer = true;
            dataOutput.Initiate(dataInput);
            askForQuantity.dataToSend = dataOutput;
            askForQuantity.Raise();
        }

        public void ReceiveQuantity(InventoryEventData _dataInput)
        {
            if (!waitingForAnswer)
                return;
            waitingForAnswer = false;

            int quantityToMove = _dataInput.attachedInt;

            MoveItem(quantityToMove);
        }

        public void MoveItem(int quantityToMove)
        {
            ItemSlot startItemSlot = dataInput.itemSlot;
            int toLeaveAtStart = startItemSlot.Quantity - quantityToMove;
            int numberOfItemThatCouldntFit = endInventory.StackItem(startItemSlot, startInventory, null, toLeaveAtStart);
            if (numberOfItemThatCouldntFit != 0)
                SendInventoryFullMessage(numberOfItemThatCouldntFit);

            ResetPanel();
        }

        public void SendInventoryFullMessage(int numberOfItemThatCouldntFit)
        {
            string message = "Could not fit " + numberOfItemThatCouldntFit.ToString() + " "
                + dataInput.itemSlot.Item.name + ". Inventory of " + endInventory.ownerName + " full.";
            SendStringMessage(message);
        }


        public void ResetPanel()
        {
            startInventory = null;
            endInventory = null;
            dataInput = null;
            waitingForAnswer = false;
            gameObject.SetActive(false);
        }

        public void SendStringMessage(string messageToSend)
        {
            dataOutput.Initiate(dataInput, messageToSend);
            sendMessage.dataToSend = dataOutput;
            sendMessage.Raise();
        }

        private void Awake()
        {
            dataOutput = InventoryEventData.GetInstance();
        }
    }
}
