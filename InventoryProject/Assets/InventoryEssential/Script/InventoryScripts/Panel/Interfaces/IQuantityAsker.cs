using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kInventory
{
    public interface IQuantityAsker
    {
        void AskForQuantity();
        void ReceiveQuantity(InventoryEventData inventoryEventData);
    }
}

