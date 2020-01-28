using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace kInventory
{

    public class QuantityPanel : MonoBehaviour, IPanel, IQuantityGiver, IEventDataUser<InventoryEventData>
    {
        [Header("Components in children")]
        [SerializeField]
        private TextMeshProUGUI messageText = null;
        [SerializeField]
        private TextMeshProUGUI quantityText = null;
        [SerializeField]
        private Scrollbar scrollbar = null;
        [Header("Inventory event to send to other panels")]
        [SerializeField]
        private InventoryEvent answerForQuantity = null;

        private InventoryEventData dataOutput;

        private int quantityToReturn;
        private int quantityMax;

        public void ReceiveData(InventoryEventData _dataInput)
        {
            quantityMax = _dataInput.itemSlot.Quantity;
            UpdatePanelMessage(_dataInput.attachedMessage);
            ReceiveAskForQuantity(quantityMax);
        }

        private void UpdatePanelMessage(string _message)
        {
            messageText.text = _message;
        }

        public void ReceiveAskForQuantity(int _quantityMax)
        {
            scrollbar.value = 1;
            quantityToReturn = _quantityMax;
            UpdateText();
        }

        public void OnConfirmInput()
        {
            ReturnQuantity(quantityToReturn);
        }

        public void ReturnQuantity(int quantityReturned)
        {
            dataOutput.Initiate(null, quantityToReturn);
            answerForQuantity.dataToSend = dataOutput;
            answerForQuantity.Raise();
        }

        public void UpdateQuantity()
        {
            quantityToReturn = Mathf.RoundToInt(quantityMax * scrollbar.value);
            UpdateText();
        }

        private void UpdateText()
        {
            quantityText.text = quantityToReturn.ToString();
        }

        public void ResetPanel()
        {
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            dataOutput = InventoryEventData.GetInstance();
        }
    }
}
