using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using kUI;

namespace kInventory
{
    public class HighlightPanel : MonoBehaviour, IPanel, IEventDataUser<InventoryEventData>
    {
        private Vector3 pos;
        private Vector2 size;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            HelperUI.SetAnchorAndPivot(rectTransform);
        }

        public void ReceiveData(InventoryEventData _dataInput)
        {
            pos = _dataInput.worldPosition;
            size = _dataInput.slotUISize;

            ScaleAndPlaceHighlight();
        }

        private void ScaleAndPlaceHighlight()
        {
            rectTransform.position = pos;
            rectTransform.sizeDelta = size;
        }

        public void ResetPanel()
        {
            gameObject.SetActive(false);
        }
    }
}
