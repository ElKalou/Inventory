using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Asset/InventoryUIParameter")]
public class InventoryUIParameter : ScriptableObject
{
    [SerializeField]
    private Vector2 _margin = Vector2.zero;
    [SerializeField]
    private Vector2 _extraScale = Vector2.zero;
    [SerializeField]
    private GameObject _itemSlotPrefab = null;
    [SerializeField]
    private float _spaceBetweenItemSlot = 0;
    [SerializeField]
    private GameObject _nameHolderPrefab = null;
    [SerializeField]
    private GameObject _moneyHolderPrefab = null;


    public GameObject itemSlotPrefab => _itemSlotPrefab;
    public Vector2 margin => _margin;
    public Vector2 extraScale => _extraScale;
    public GameObject nameHolderPrefab => _nameHolderPrefab;
    public float spaceBetweenItemSlot => _spaceBetweenItemSlot;
    public GameObject moneyHolderPrefab => _moneyHolderPrefab;

    private Vector2 slotSize;

    private void OnEnable()
    {
        slotSize = Vector2.zero;
    }

    public Vector2 GetSlotSize()
    {
        if (slotSize != Vector2.zero)
            return slotSize;

        Rect rectSlot = _itemSlotPrefab.GetComponent<RectTransform>().rect;
        slotSize = new Vector2(rectSlot.width, rectSlot.height);
        return slotSize;
    }

}
