using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kInventory
{
    [System.Serializable]
    public class ItemSlot
    {

        [SerializeField]
        private Item item;
        [SerializeField]
        private int quantity;

        public Item Item => item;
        public int Quantity => quantity;

        public ItemSlot(Item _item, int _quantity)
        {
            if (_quantity == 0)
            {
                EmptySlot();
            }
            else
            {
                item = _item;
                quantity = _quantity;
            }
        }

        public ItemSlot()
        {
            EmptySlot();
        }

        private void EmptySlot()
        {
            item = null;
            quantity = 0;
        }

        public int AddQuantity(int toAdd)
        {
            int remain = 0;
            int room = item.maxStack - quantity;
            if (room > toAdd)
                quantity += toAdd;
            else
            {
                remain = toAdd - room;
                quantity = item.maxStack;
            }
            return remain;
        }

        public void RemoveQuantity(int toRemove)
        {
            if (quantity - toRemove < 0)
                return;

            quantity -= toRemove;
        }
    }
}