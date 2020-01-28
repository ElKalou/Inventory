using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace kInventory
{
    public class InfoPanel : MonoBehaviour, IPanel, IEventDataUser<InventoryEventData>
    {

        [Header("Component in Children")]
        [SerializeField]
        private TextMeshProUGUI textToDisplay;
        [SerializeField]
        private Image imageToDisplay;

        private Item currentItemDisplayed;

        private void Awake()
        {
            if (textToDisplay == null)
                textToDisplay = GetComponentInChildren<TextMeshProUGUI>();
            if (imageToDisplay == null)
                imageToDisplay = GetComponentInChildren<Image>();
        }

        public void ReceiveData(InventoryEventData _dataInput)
        {
            currentItemDisplayed = _dataInput.itemSlot.Item;
            if (currentItemDisplayed == null)
            {
                NoItemInPanel();
                return;
            }
            textToDisplay.text = currentItemDisplayed.GetItemInfo();
            imageToDisplay.sprite = currentItemDisplayed.icon;
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
