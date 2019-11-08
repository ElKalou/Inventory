using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kInventory
{
    public class InventoryUIOpenerCloser : MonoBehaviour
    {
        private List<InventoryUI> instanciatedUI = new List<InventoryUI>();
        private Inventory receivedInventory;

        public void ReceivedOpenInventory(ScriptableObject scriptableObject)
        {
            if (scriptableObject == null)
                return;

            receivedInventory = kCaster.tryCast<Inventory>(scriptableObject);
            if (receivedInventory == null)
                return;

            InventoryUI inventoryUIAssociated = IsAlreadyExisting(receivedInventory);
            if (inventoryUIAssociated == null)
                inventoryUIAssociated = MakeNewInventoryUI();

            OpenInventoryUI(inventoryUIAssociated);
        }

        public void ReceivedCloseInventory()
        {
            CloseInventoriesUI();
        }

        private void OpenInventoryUI(InventoryUI toOpen)
        {
            toOpen.inventory.OnOpen();
            toOpen.gameObject.SetActive(true);
        }

        private void CloseInventoriesUI()
        {
            for (int i = 0; i < instanciatedUI.Count; i++)
            {
                instanciatedUI[i].inventory.OnClose();
                instanciatedUI[i].gameObject.SetActive(false);
            }
        }

        private InventoryUI MakeNewInventoryUI()
        {
            GameObject backGroundUI = Instantiate(receivedInventory.inventoryUIPrefab,
                transform);

            InventoryUI newInventoryUI = backGroundUI.GetComponent<InventoryUI>();
            newInventoryUI.Init(receivedInventory);
            instanciatedUI.Add(newInventoryUI);
            return newInventoryUI;
        }

        private InventoryUI IsAlreadyExisting(Inventory inventoryToCheck)
        {
            foreach (InventoryUI inventoryUI in instanciatedUI)
            {
                if (inventoryUI.inventory == inventoryToCheck)
                    return inventoryUI;
            }

            return null;
        }
    }
}
