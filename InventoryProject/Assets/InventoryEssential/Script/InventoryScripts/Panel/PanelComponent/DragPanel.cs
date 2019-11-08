using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace kInventory
{
    public class DragPanel : MonoBehaviour, IPanel, IEventDataUser<InventoryEventData>
    {
        [Header("Component in Children")]
        [SerializeField]
        private Image imageToDisplay;

        private void Awake()
        {
            if (imageToDisplay == null)
                imageToDisplay = GetComponentInChildren<Image>();
        }

        public void ReceiveData(InventoryEventData _dataInput)
        {

            if (_dataInput.itemSlot.Item == null)
            {
                NoItemInPanel();
                return;
            }
            else
            {
                imageToDisplay.sprite = _dataInput.itemSlot.Item.icon;
            }
        }

        private void NoItemInPanel()
        {
            gameObject.SetActive(false);
        }

        public void ResetPanel()
        {
            gameObject.SetActive(false);
        }

    }
}
