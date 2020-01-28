using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace kInventory
{
    // disable panels depending on the inventory owners

    public class PanelManager : MonoBehaviour
    {
        private List<PanelManagerLink> panelManagerLinkers = new List<PanelManagerLink>();

        public Inventory[] receivedInventories { get; private set; } 
        private bool inventoryOpen;

        private void Awake()
        {
            receivedInventories = new Inventory[2];
        }

        public void Register(PanelManagerLink openerCloserToRegister)
        {
            if (panelManagerLinkers.Contains(openerCloserToRegister))
                return;

            panelManagerLinkers.Add(openerCloserToRegister);
        }

        public void OnReceiveOpenInventory(ScriptableObject scriptableObject)
        {
            if (receivedInventories[0] == null)
            {
                ResetLinkersPanel();
                receivedInventories[0] = kCaster.tryCast<Inventory>(scriptableObject);
                if (receivedInventories[0] == null)
                    return;
                StartCoroutine(WaitForPossibleSecondInventory());
            }
            else if (receivedInventories[1] == null)
            {
                receivedInventories[1] = kCaster.tryCast<Inventory>(scriptableObject);
                if (receivedInventories[1] == null)
                    return;
            }
        }

        public void OnReceiveCloseInventory()
        {
            if (inventoryOpen == true)
            {
                inventoryOpen = false;
                receivedInventories = new Inventory[2];
                ResetLinkersPanel();
            }
        }

        private IEnumerator WaitForPossibleSecondInventory()
        {
            yield return StartCoroutine(InventoryEventListener.WaitForPossibleEvent(1));

            if (receivedInventories[1] == null)
            {
                EnablePanels(receivedInventories[0].owner);
            }
            else
            {
                InventoryOwner nonPlayerOwner = GetNonPlayerOwner();
                EnablePanels(nonPlayerOwner);
            }

            inventoryOpen = true;
        }

        private void EnablePanels(InventoryOwner owner)
        {
            List<PanelManagerLink> toEnable = panelManagerLinkers.FindAll(x => !x.disableOn.Contains(owner));
            foreach (PanelManagerLink item in toEnable)
            {
                item.gameObject.SetActive(true);
            }
        }

        private void ResetLinkersPanel()
        {
            foreach (var panelLinker in panelManagerLinkers)
            {
                panelLinker.ResetPanelDisplay();
                panelLinker.gameObject.SetActive(false);
            }
        }

        private InventoryOwner GetNonPlayerOwner()
        {
            if (receivedInventories[0].owner != InventoryOwner.player)
                return receivedInventories[0].owner;
            else
                return receivedInventories[1].owner;
        }

    }
}
