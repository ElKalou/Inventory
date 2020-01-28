using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kInventory
{
    [CreateAssetMenu(menuName = "Inventory Asset/Inventory")]
    public class Inventory : ScriptableObject
    {

        [SerializeField]
        private string _ownerName = "";
        [SerializeField]
        private InventoryOwner _owner = 0;
        [SerializeField]
        private GameObject _inventoryUIPrefab = null;
        [SerializeField]
        private InventoryEvent _updateInventoryUI = null;
        [SerializeField]
        private int _money = 0;
        [SerializeField]
        private List<ItemSlot> _itemSlots = null;


        public string ownerName => _ownerName;
        public int capacity => _itemSlots.Count;
        public List<ItemSlot> itemSlots => _itemSlots;
        public InventoryOwner owner => _owner;
        public GameObject inventoryUIPrefab => _inventoryUIPrefab;
        public int money => _money;

        public bool isOpen { get; private set; }

        private void OnEnable()
        {
            isOpen = false;
        }

        public void OnOpen()
        {
            isOpen = true;
        }

        public void OnClose()
        {
            isOpen = false;
        }

        public void SwapItemSlot(Inventory inventory1, ItemSlot itemSlot1,
            Inventory inventory2, ItemSlot itemSlot2)
        {
            int index1 = inventory1.GetIndexOfItemSlot(itemSlot1);
            int index2 = inventory2.GetIndexOfItemSlot(itemSlot2);
            inventory1._itemSlots[index1] = itemSlot2;
            inventory2._itemSlots[index2] = itemSlot1;
            _updateInventoryUI.Raise();
        }

        public int StackItem(ItemSlot itemSlotAtStart, Inventory inventoryAtStart,
            ItemSlot itemSlotAtEnd = null,
            int toLeaveOnSlot = 0)
        {
            int remainToStack = itemSlotAtStart.Quantity - toLeaveOnSlot;
            int indexStart = inventoryAtStart.GetIndexOfItemSlot(itemSlotAtStart);

            if (itemSlotAtEnd != null)
                remainToStack = TryStackOnItemSlot(itemSlotAtEnd, remainToStack);
            if (remainToStack != 0)
                remainToStack = TryStackOnItemSlotWithSimilarItem(itemSlotAtStart, remainToStack);
            // if same start and end inventory we do not want to fill an empty slot
            if (remainToStack != 0 && inventoryAtStart != this) 
                remainToStack = TryStackOnEmptyItemSlot(itemSlotAtStart, remainToStack);

            inventoryAtStart._itemSlots[indexStart] = new ItemSlot(itemSlotAtStart.Item, remainToStack + toLeaveOnSlot);

            _updateInventoryUI.Raise();
            return remainToStack;
        }

        private int TryStackOnEmptyItemSlot(ItemSlot itemSlot, int _remain)
        {
            int emptyItemSlotIndex = _itemSlots.FindIndex(x => x.Item == null);
            if (emptyItemSlotIndex != -1)
            {
                _itemSlots[emptyItemSlotIndex] = new ItemSlot(itemSlot.Item, _remain);
                return 0;
            }
            return _remain;
        }

        private int TryStackOnItemSlotWithSimilarItem(ItemSlot startItemSlot, int _remain)
        {
            foreach (var itemSlot in _itemSlots.FindAll(x => x.Item == startItemSlot.Item && x != startItemSlot))
            {
                _remain = TryStackOnItemSlot(itemSlot, _remain);
                if (_remain == 0)
                    return _remain;
            }
            return _remain;
        }
        
        private int TryStackOnItemSlot(ItemSlot itemSlotToStack, int _remain)
        {
            return (itemSlotToStack.AddQuantity(_remain));
        }

        public void DropItemOnFloor(ItemSlot itemToDrop)
        {
            int indexToDrop = GetIndexOfItemSlot(itemToDrop);
            _itemSlots[indexToDrop] = new ItemSlot();
            _updateInventoryUI.Raise();
        }

        private int GetIndexOfItemSlot(ItemSlot toFind)
        {
            return _itemSlots.FindIndex(x => x == toFind);
        }

        public void AddMoney(int moneyToAdd)
        {
            _money += moneyToAdd;
            _updateInventoryUI.Raise();
        }

        public void RemoveMoney(int moneyToRemove)
        {
            _money -= moneyToRemove;
            _updateInventoryUI.Raise();
        }
    }

    public enum InventoryOwner
    {
        container, merchant, player
    }
}
