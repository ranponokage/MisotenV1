using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemSO> _itemSOs;
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ItemSlot[] _itemSlots;

    public event Action<ItemSO> OnItemRightClickedEvent;

    private void Awake()
    {
        for(int i = 0; i< _itemSlots.Length; i++)
        {
            _itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }
    }

    private void OnValidate()
    {
        if (_itemsParent != null)
            _itemSlots = _itemsParent.GetComponentsInChildren<ItemSlot>();
        RefreshUI();
    }

    private void RefreshUI()
    {
        int i = 0;
        for(;i< _itemSOs.Count&& i<_itemSlots.Length;i++)
        {
            _itemSlots[i]._ItemSO = _itemSOs[i];
        }
        for(; i< _itemSlots.Length; i++)
        {
            _itemSlots[i]._ItemSO = null;
        }
    }

    public bool AddItem(ItemSO itemSO)
    {
        if (IsFull())
            return false;
        _itemSOs.Add(itemSO);
        RefreshUI();
        return true;
    }

    public bool RemoveItem(ItemSO itemSO)
    {
        if(_itemSOs.Remove(itemSO))
        {
            RefreshUI();
            return true;
        }
        return false;

    }

    public bool IsFull()
    {
        return _itemSOs.Count >= _itemSlots.Length;
    }
}
