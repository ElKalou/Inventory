using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using kUI;

namespace kInventory
{
    public class InventoryUI : MonoBehaviour
    {
        public InventoryUIParameter UIParameters;

        public Inventory inventory { get; private set; }
        private ItemSlotDataSender[] itemSlotDataSender;
        private ItemSlotUI[] itemSlotUI;
        private MoneyDisplay moneyDisplay;
        private int[] arraySize;
        private Vector2 backGroundSize;


        public void Init(Inventory _inventory)
        {
            inventory = _inventory;
            itemSlotDataSender = new ItemSlotDataSender[inventory.capacity];
            itemSlotUI = new ItemSlotUI[inventory.capacity];

            arraySize = Get2DArraySize(inventory.capacity);

            InitBackgroundUI();

            InitSlotsUI();

            InitNameHolder();

            InitPriceHolder();

            UpdateUI();
        }

        

        private void InitBackgroundUI()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            if (rectTransform == null)
                return;

            ScaleBackground(rectTransform);
            PlaceBackground(rectTransform);
        }

        private void InitSlotsUI()
        {
            RectTransform rectTransform = UIParameters.itemSlotPrefab.GetComponent<RectTransform>();
            HelperUI.SetAnchorAndPivot(rectTransform);
            for (int i = 0; i < inventory.capacity; i++)
            {
                GameObject slotsUIGO = Instantiate(UIParameters.itemSlotPrefab, transform);
                itemSlotDataSender[i] = slotsUIGO.GetComponent<ItemSlotDataSender>();
                itemSlotDataSender[i].transform.localPosition = PlaceSlotUI(i);
                itemSlotDataSender[i].Init(i, inventory);
                itemSlotUI[i] = slotsUIGO.GetComponent<ItemSlotUI>();
            }
        }

        private void InitNameHolder()
        {
            RectTransform holderRectTransform = InitHolder(UIParameters.nameHolderPrefab);
            holderRectTransform.localPosition = new Vector3(
                backGroundSize.x / 2 - holderRectTransform.rect.width / 2,
                holderRectTransform.rect.height,
                0);
            holderRectTransform.GetComponentInChildren<TextMeshProUGUI>().text = inventory.ownerName;
        }

        private void InitPriceHolder()
        {
            RectTransform holderRectTransform = InitHolder(UIParameters.moneyHolderPrefab);
            holderRectTransform.localPosition = new Vector3(
                backGroundSize.x - holderRectTransform.rect.width,
                -backGroundSize.y,
                0);
            moneyDisplay = holderRectTransform.GetComponentInChildren<MoneyDisplay>();
            moneyDisplay.Init(inventory);
        }

        private RectTransform InitHolder(GameObject toInstantiate)
        {
            GameObject nameHolderGO = Instantiate(toInstantiate, transform);
            RectTransform rectTransform = nameHolderGO.GetComponent<RectTransform>();
            HelperUI.SetAnchorAndPivot(rectTransform);
            return rectTransform;
        }


        private void ScaleBackground(RectTransform rectTransform)
        {
            Vector2 slotUISize = UIParameters.GetSlotSize();

            backGroundSize = new Vector2(
                arraySize[0] * (slotUISize.x + UIParameters.spaceBetweenItemSlot) + UIParameters.extraScale.x,
                arraySize[1] * (slotUISize.y + UIParameters.spaceBetweenItemSlot) + UIParameters.extraScale.y);
            rectTransform.sizeDelta = backGroundSize;
        }

        private void PlaceBackground(RectTransform rectTransform)
        {
            HelperUI.SetAnchorAndPivot(rectTransform);
            Vector2 backGroundSize = new Vector2(
                rectTransform.rect.width,
                rectTransform.rect.height);

            if (inventory.owner == InventoryOwner.player)
            {
                rectTransform.position = new Vector3(
                         Screen.width - backGroundSize.x - UIParameters.margin.x,
                         backGroundSize.y + UIParameters.margin.y,
                         0);
            }
            else
            {
                rectTransform.position = new Vector3(
                        UIParameters.margin.x,
                        backGroundSize.y + UIParameters.margin.y,
                        0);
            }
        }

        private Vector3 PlaceSlotUI(int index)
        {
            Vector3 pos = Vector3.zero;

            Vector2 slotSize = UIParameters.GetSlotSize();

            int rem;
            int quotient = Math.DivRem(index, arraySize[0], out rem);

            pos = new Vector3(
                (slotSize.x + UIParameters.spaceBetweenItemSlot) * rem + UIParameters.extraScale.x / 2,
                -(slotSize.y + UIParameters.spaceBetweenItemSlot) * quotient - UIParameters.extraScale.y / 2,
                0);
            return pos;
        }

        public void UpdateUI()
        {
            for (int i = 0; i < itemSlotDataSender.Length; i++)
            {
                itemSlotUI[i].UpdateUI(inventory.itemSlots[i]);
            }
            moneyDisplay.UpdateUI(inventory.money);
        }


        private int[] Get2DArraySize(int capacity)
        {
            arraySize = new int[2] { 0, 0 };

            if (capacity == 1)
            {
                arraySize[0] = 1;
                arraySize[1] = 1;
            }
            else if (capacity <= 4)
            {
                arraySize[0] = 2;
                arraySize[1] = GetArrayVertSize(capacity, arraySize[0]);

            }
            else if (capacity <= 12)
            {
                arraySize[0] = 3;
                arraySize[1] = GetArrayVertSize(capacity, arraySize[0]);
            }
            else if (capacity <= 20)
            {
                arraySize[0] = 4;
                arraySize[1] = GetArrayVertSize(capacity, arraySize[0]);
            }
            return arraySize;
        }

        private int GetArrayVertSize(int linearSize, int arrayHorSize)
        {
            int rem;
            int quotient = Math.DivRem(linearSize, arrayHorSize, out rem);
            return (quotient + (rem == 0 ? 0 : 1));
        }

    }
}
