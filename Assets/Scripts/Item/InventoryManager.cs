using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Inventory _inventory;
    [SerializeField] EquitpmentPanel _equitpmentPanel;

    private void Awake()
    {
        _inventory.OnItemRightClickedEvent += EquipFromInventory;
    }

    private void EquipFromInventory(ItemSO itemSO)
    {
        if(itemSO is EquippableItemSO)
        {
            Equip((EquippableItemSO)itemSO);
        }
    }

    public void Equip(EquippableItemSO equippableItemSO)
    {
        if(_inventory.RemoveItem(equippableItemSO))
        {
            EquippableItemSO previousItem;
            if(_equitpmentPanel.AddItem(equippableItemSO,out previousItem))
            {
                if(previousItem != null)
                {
                    _inventory.AddItem(previousItem);
                }
            }
            else
            {
                _inventory.AddItem(equippableItemSO); 
            }
        }
    }

    public void Unequip(EquippableItemSO equippableItemSO)
    {
        if (!_inventory.IsFull()&& _equitpmentPanel.RemoveItem(equippableItemSO))
        {
            _inventory.AddItem(equippableItemSO);
        }
    }

}
