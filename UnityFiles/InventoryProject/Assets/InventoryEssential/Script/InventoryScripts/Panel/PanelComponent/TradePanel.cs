using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace kInventory
{
    public class TradePanel : MonoBehaviour, IEventDataUser<InventoryEventData>, 
        IPanel, IItemMover, IStringMessageSender
    {
        [Header("Component in children")]
        [SerializeField] private Scrollbar scrollbar = null;
        [SerializeField] private TextMeshProUGUI quantityTextSlidder = null;
        [SerializeField] private TextMeshProUGUI priceTextSlidder = null;
        [SerializeField] private TextMeshProUGUI recapText = null;
        [Header("Event to send to other panels")]
        [SerializeField] private InventoryEvent askQuantity = null;
        [SerializeField] private InventoryEvent answerQuantity = null;
        [SerializeField] private InventoryEvent sendMessage = null;

        private InventoryEventData inputData;
        private Inventory inventorySeller;
        private Inventory inventoryBuyer;

        private InventoryEventData outputData;

        private int itemPrice;
        private int maxMovingQuantity;
        private int quantityToMove;

        public void ReceiveData(InventoryEventData _inputData)
        { 
            if (_inputData.itemSlot.Item == null)
            {
                ResetPanel();
                return;
            }
                

            inputData = _inputData;
            inventorySeller = _inputData.container;
            inventoryBuyer = FindOtherInventory();
            itemPrice = _inputData.itemSlot.Item.price;
            maxMovingQuantity = _inputData.itemSlot.Quantity;
            InitPanel();
            askQuantity.Raise();
        }

        private void InitPanel()
        {
            scrollbar.value = 0;
            quantityToMove = 0;
            UpdateTexts();
        }

        public void OnUpdateSlider()
        {
            if (inputData == null)
                return;
            quantityToMove = Mathf.RoundToInt(scrollbar.value * maxMovingQuantity);
            UpdateTexts();
        }

        private void UpdateTexts()
        {
            quantityTextSlidder.text = quantityToMove.ToString();
            priceTextSlidder.text = (quantityToMove * itemPrice).ToString();
            recapText.text = MakeRecapText();
        }

        public void OnConfirmInput()
        {

            if(quantityToMove * itemPrice > inventoryBuyer.money)
            {
                int missingMoney = quantityToMove * itemPrice - inventoryBuyer.money;
                SendNotEnoughMoneyMessage(missingMoney);
            }
            else
                MoveItem(quantityToMove);

            answerQuantity.Raise();
            
            gameObject.SetActive(false);
        }

        public void MoveItem(int _quantityToMove)
        {
            int toLeave = maxMovingQuantity - _quantityToMove;
            int remain = inventoryBuyer.StackItem(inputData.itemSlot, inventorySeller, null, toLeave);
            int transactionPrice = (this.quantityToMove - remain) * itemPrice;
            inventorySeller.AddMoney(transactionPrice);
            inventoryBuyer.RemoveMoney(transactionPrice);
            if (remain != 0)
                SendInventoryFullMessage(remain);
            ResetPanel();
        }

        private string MakeRecapText()
        {
            string buyOrSell = inputData.container.owner == InventoryOwner.player ? "Sell " : "Buy ";
            string recapText = buyOrSell + quantityToMove + " " + inputData.itemSlot.Item.name + " for " +
                quantityToMove * itemPrice + " gold.";
            return recapText;
        }

        private void SendNotEnoughMoneyMessage(int missingMoney)
        {
            string message = inventoryBuyer.ownerName + " is missing " + missingMoney.ToString() + " to complete the trade.";
            SendStringMessage(message);
        }

        public void SendInventoryFullMessage(int numberOfItemThatCouldntFit)
        {
            string itemName = inputData.itemSlot.Item.name;
            string message = "Can not fit " + numberOfItemThatCouldntFit.ToString() + itemName + 
                " in inventory of " + inventoryBuyer.ownerName +
                ". Transaction completed for " + (quantityToMove - numberOfItemThatCouldntFit).ToString() + " " + itemName + 
                " at a price of " + ((quantityToMove - numberOfItemThatCouldntFit)*itemPrice).ToString();
            SendStringMessage(message);
        }

        public void ResetPanel()
        {
            inputData = null;
            inventoryBuyer = null;
            inventorySeller = null;
            gameObject.SetActive(false);
        }

        public void CancelTransaction()
        {
            answerQuantity.Raise();
            ResetPanel();
        }

        private Inventory FindOtherInventory()
        {
            Inventory[] openedInventory = GetComponentInParent<PanelManagerLink>().GetOpenedInventory();
            if (openedInventory[0] != inventorySeller)
                return openedInventory[0];
            else
                return openedInventory[1];
        }

        public void SendStringMessage(string messageToSend)
        {
            outputData.Initiate(inputData);
            outputData.AddMessage(messageToSend);
            sendMessage.dataToSend = outputData;
            sendMessage.Raise();
        }

        private void Awake()
        {
            if (quantityTextSlidder == null)
                quantityTextSlidder = GetComponentInChildren<TextMeshProUGUI>();
            if (priceTextSlidder == null)
                priceTextSlidder = GetComponentInChildren<TextMeshProUGUI>();
            if (recapText == null)
                recapText = GetComponentInChildren<TextMeshProUGUI>();
            if (scrollbar == null)
            {
                scrollbar = GetComponentInChildren<Scrollbar>();
                if (scrollbar == null)
                    Debug.LogError("Missing Scrollbar component in prefab " + gameObject.name);
            }

            outputData = InventoryEventData.GetInstance();
        }

        

        


        
    }
}

