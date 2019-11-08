using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace kInventory
{
    public class DropPanel : MonoBehaviour, IEventDataUser<InventoryEventData>, IPanel, 
        IQuantityAsker, IStringMessageSender, IItemMover
    {

        [Header("GameEvents to send to other panels")]
        [SerializeField]
        private InventoryEvent askForQuantity = null;
        [SerializeField]
        private InventoryEvent sendMessage = null;

        private InventoryEventData dataOutput;

        private InventoryEventData dataInputAtStart;
        private InventoryEventData dataInputAtEnd;

        private bool dropOnItemSlot;

        private bool waitingForAnswer;

        public void ReceiveData(InventoryEventData _dataInput)
        {  

            if (dataInputAtStart == null)
            {
                if (_dataInput.itemSlot.Item == null)
                {
                    NoItemInPanel();
                    return;
                }
                dataInputAtStart = _dataInput;
            }
            else if (dataInputAtEnd == null)
            {
                dropOnItemSlot = true;
                dataInputAtEnd = _dataInput;
            }
            else
                Debug.LogError("error, no more than two items expected in drop");

        }

        private void NoItemInPanel()
        {
            gameObject.SetActive(false);
        }

        public void OnStopDraging()
        {
            if (dataInputAtStart == null)
                return;
            StartCoroutine(DropCoroutine());
        }

        private IEnumerator DropCoroutine()
        {
            yield return StartCoroutine(InventoryEventListener.WaitForPossibleEvent(5));

            if (!dropOnItemSlot) // drop item on floor
            {
                dataInputAtStart.container.DropItemOnFloor(dataInputAtStart.itemSlot);
                string infoMessage = MakeDropOnFloorMessage(dataInputAtStart.itemSlot.Quantity, 
                    dataInputAtStart.itemSlot.Item.name);
                SendStringMessage(infoMessage);
                ResetPanel();
            }
            else if (dataInputAtStart.itemSlot == dataInputAtEnd.itemSlot) // depart = destination
            {
                ResetPanel();
            }
            else 
            {
                Item itemAtStart = dataInputAtStart.itemSlot.Item;
                Item itemAtEnd = dataInputAtEnd.itemSlot.Item;

                if (itemAtEnd == null || itemAtEnd != itemAtStart) // swap itemSlot
                {
                    dataInputAtStart.container.SwapItemSlot(dataInputAtStart.container, dataInputAtStart.itemSlot,
                        dataInputAtEnd.container, dataInputAtEnd.itemSlot);
                    ResetPanel();
                }
                else if (itemAtEnd == itemAtStart) // stack itemSlot;
                {
                    if (dataInputAtStart.container == dataInputAtEnd.container ||
                        dataInputAtStart.itemSlot.Quantity == 1)
                    {
                        MoveItem(dataInputAtStart.itemSlot.Quantity);
                    }
                    else
                    {
                        AskForQuantity();
                    }
                }
            }  
        }

        public void AskForQuantity()
        {
            waitingForAnswer = true;
            dataOutput.Initiate(dataInputAtStart);
            askForQuantity.dataToSend = dataOutput;
            askForQuantity.Raise();
        }

        public void ReceiveQuantity(InventoryEventData _dataInput)
        {
            if (!waitingForAnswer)
                return;
            waitingForAnswer = false;

            MoveItem(_dataInput.attachedInt);
        }

        public void MoveItem(int _quantityToMove)
        {
            int toLeaveAtStart = dataInputAtStart.itemSlot.Quantity - _quantityToMove;
            int numberOfItemThatCouldntFit = dataInputAtEnd.container.StackItem(dataInputAtStart.itemSlot,
                            dataInputAtStart.container, dataInputAtEnd.itemSlot, toLeaveAtStart);

            if (numberOfItemThatCouldntFit != 0 && dataInputAtStart.container != dataInputAtEnd.container)
                SendInventoryFullMessage(numberOfItemThatCouldntFit);

            ResetPanel();
        }

        public void SendInventoryFullMessage(int numberOfItemThatCouldntFit)
        {
            string message = "Could not fit " + numberOfItemThatCouldntFit.ToString() + " "
                + dataInputAtStart.itemSlot.Item.name + ". Inventory of " + dataInputAtEnd.container.ownerName + " full.";
            SendStringMessage(message);
        }

        private string MakeDropOnFloorMessage(int numberOfItemDropped, string nameOfItem)
        {
            return "Dropped " + numberOfItemDropped.ToString() + " " + nameOfItem + " on floor.";
        }

        public void ResetPanel()
        {
            dataInputAtStart = null;
            dataInputAtEnd = null;
            dropOnItemSlot = false;
            waitingForAnswer = false;
            gameObject.SetActive(false);
        }

        public void SendStringMessage(string messageToSend)
        {
            dataOutput.Initiate(dataInputAtStart, messageToSend);
            sendMessage.dataToSend = dataOutput;
            sendMessage.Raise();
        }

        private void Awake()
        {
            dataOutput = InventoryEventData.GetInstance();
        }



    }
}
