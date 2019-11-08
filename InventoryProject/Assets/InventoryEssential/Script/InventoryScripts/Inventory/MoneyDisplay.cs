using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;

namespace kInventory
{
    public class MoneyDisplay : MonoBehaviour, IPointerDownHandler, IEventSender<GameEvent>
    {
        [Header("Component in children")]
        [SerializeField]
        private TextMeshProUGUI quantityText = null;
        [Header("Event to send to panels")]
        [SerializeField]
        private GameEvent transferMoney = null;

        private GameEventData dataOutput;
        private int money;
        private InventoryOwner thisOwner;

        private int clicked;

        private void Awake()
        {
            if (quantityText == null)
                quantityText = GetComponentInChildren<TextMeshProUGUI>();

            dataOutput = GameEventData.GetInstance();
        }

        public void UpdateUI(int _money)
        {
            quantityText.text = _money.ToString();
            money = _money;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (thisOwner != InventoryOwner.container)
                return;

            clicked = InputEmitter.MultipleMouseClick(clicked);
            if (clicked == 2)
                SendEvent(transferMoney);
        }

        public void SendEvent(GameEvent eventToSend)
        {
            dataOutput.attachedInt = money;
            eventToSend.dataToSend = dataOutput;
            eventToSend.Raise();
        }

        internal void Init(Inventory inventory)
        {
            money = inventory.money;
            quantityText.text = money.ToString();
            thisOwner = inventory.owner;
        }
    }
}


