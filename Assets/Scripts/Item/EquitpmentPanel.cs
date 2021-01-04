using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquitpmentPanel : MonoBehaviour
{
    [SerializeField] Transform _equipmentSlotsParent;
    [SerializeField] EquipmentSlot[] _equipmentSlots;

    private void OnValidate()
    {
        _equipmentSlots = _equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public bool AddItem(EquippableItemSO equippableItemSO, out EquippableItemSO previousItem)
    {
        for(int i = 0; i < _equipmentSlots.Length; i++)
        {
            if(_equipmentSlots[i].EquipmentType == equippableItemSO._EquipmentType)
            {
                previousItem = (EquippableItemSO)_equipmentSlots[i]._ItemSO;
                _equipmentSlots[i]._ItemSO = equippableItemSO;
                return true;
            }
        }
        previousItem = null;
        return false;
    }

    public bool RemoveItem(EquippableItemSO equippableItemSO)
    {
        for (int i = 0; i < _equipmentSlots.Length; i++)
        {
            if (_equipmentSlots[i]._ItemSO == equippableItemSO)
            {
                _equipmentSlots[i]._ItemSO = null;
                return true;
            }
        }
        return false;
    }
}
