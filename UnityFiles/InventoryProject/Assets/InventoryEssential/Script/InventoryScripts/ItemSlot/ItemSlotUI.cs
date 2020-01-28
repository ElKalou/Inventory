using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace kInventory
{
    public class ItemSlotUI : MonoBehaviour
    {
        [Header("Component in children")]
        [SerializeField]
        private Image imageInChildren;
        [SerializeField]
        private TextMeshProUGUI textInChildren;

        public void Awake()
        {
            if (imageInChildren == null)
                imageInChildren = GetComponentInChildren<Image>();
            if (textInChildren == null)
                textInChildren = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void UpdateUI(ItemSlot newItemSlot)
        {
            SetIcon(newItemSlot);
            SetQuantity(newItemSlot);
        }

        private void SetIcon(ItemSlot itemSlot)
        {
            if (itemSlot.Item != null)
            {
                imageInChildren.sprite = itemSlot.Item.icon;
                imageInChildren.color = new Color(1, 1, 1, 1);
            }
            else
            {
                imageInChildren.sprite = null;
                imageInChildren.color = new Color(1, 1, 1, 0);
            }
        }

        private void SetQuantity(ItemSlot itemSlot)
        {
            if (itemSlot.Quantity != 0)
                textInChildren.text = itemSlot.Quantity.ToString();
            else
                textInChildren.text = "";
        }

    }
}
