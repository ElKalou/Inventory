using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace kInventory
{
    public class ItemSlotDataSender : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
     IEndDragHandler, IBeginDragHandler, IDragHandler, IDropHandler, IPointerDownHandler, IEventSender<InventoryEvent>
    {
        [Header("InventoryEvent to send to various panels")]
        [SerializeField]
        private InventoryEvent mouseOnSlot = null;
        [SerializeField]
        private InventoryEvent mouseLeaveSlot = null;
        [SerializeField]
        private InventoryEvent startDraging = null;
        [SerializeField]
        private InventoryEvent stopDraging = null;
        [SerializeField]
        private InventoryEvent drop = null;
        [SerializeField]
        private InventoryEvent mouseClick = null;
        [SerializeField]
        private InventoryEvent mouseDoubleClick = null;

        // Data to send:
        public int posInInventory { get; private set; }
        public Inventory container { get; private set; }
        public ItemSlot thisItemSlot { get; private set; }

        // Data container:
        private InventoryEventData dataOutput;

        // Doubleclick parameters
        private int clicked = 0;

        public void OnPointerEnter(PointerEventData eventData)
        {
            SendEvent(mouseOnSlot);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SendEvent(mouseLeaveSlot);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            SendEvent(startDraging);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SendEvent(stopDraging);
        }

        public void OnDrop(PointerEventData eventData)
        {
            SendEvent(drop);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            clicked = InputEmitter.MultipleMouseClick(clicked);
            if (clicked == 1)
                SendEvent(mouseClick);
            if (clicked == 2)
                SendEvent(mouseDoubleClick);
        }

        public void SendEvent(InventoryEvent eventToSend)
        {
            thisItemSlot = container.itemSlots[posInInventory];
            dataOutput.Initiate(this);
            eventToSend.dataToSend = dataOutput;
            eventToSend.Raise();
        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        public void Init(int _posInInventory, Inventory _container)
        {
            posInInventory = _posInInventory;
            container = _container;
        }


        private void Awake()
        {
            dataOutput = InventoryEventData.GetInstance();
        }

        private void OnDestroy()
        {
            Destroy(dataOutput);
        }

    }
}
