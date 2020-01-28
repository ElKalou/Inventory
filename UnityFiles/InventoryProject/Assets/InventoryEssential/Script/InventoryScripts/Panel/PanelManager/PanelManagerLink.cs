using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kInventory
{
    public class PanelManagerLink : MonoBehaviour
    {
        [Header("Info for panelManager")]
        [SerializeField]
        private PanelManager panelManager;
        public List<InventoryOwner> disableOn;

        private IPanel attachedPanel;

        public void ResetPanelDisplay()
        {
            if (attachedPanel == null)
                attachedPanel = GetComponentInChildren<IPanel>();
            if (attachedPanel != null)
                attachedPanel.ResetPanel();
        }

        private void Awake()
        {
            if (panelManager == null)
            {
                panelManager = GetComponentInParent<PanelManager>();
                if (panelManager == null)
                    Debug.LogError("Missing " + typeof(PanelManager) + " on " +
                        typeof(PanelManagerLink) + " " + this.name);
            }

            panelManager.Register(this);
        }

        public Inventory[] GetOpenedInventory()
        {
            return panelManager.receivedInventories;
        }

    }
}
