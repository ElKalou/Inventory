using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using kInventory;

public class MoneyTransfer : MonoBehaviour, IEventDataUser<ScriptableObject>, IEventDataUser<InventoryEventData>
{
    private GameEventData dataInput;
    private int moneyToTransfer;
    private Inventory[] openedInventories;
    private Inventory playerInventory;
    private Inventory otherInventory;

    public void ReceiveData(ScriptableObject _dataInput)
    {
        dataInput = kCaster.tryCast<GameEventData>(_dataInput);
        if (dataInput == null)
            return;

        moneyToTransfer = dataInput.attachedInt;
        openedInventories = GetComponentInParent<PanelManagerLink>().GetOpenedInventory();

        playerInventory = openedInventories[0].owner == InventoryOwner.player ? openedInventories[0] : openedInventories[1];
        otherInventory = openedInventories[0].owner != InventoryOwner.player ? openedInventories[0] : openedInventories[1];

        playerInventory.AddMoney(otherInventory.money);
        otherInventory.RemoveMoney(otherInventory.money);
    }

    public void ReceiveData(InventoryEventData _dataInput)
    {
        
    }
}
