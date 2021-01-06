using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferItemInventory : MonoBehaviour
{
    [SerializeField] List<BuffItemSO> _buffItemSOs;
    [SerializeField] Transform _itemsParent;
    [SerializeField] BuffItemSlot[] _buffItemSlots;

    private void OnValidate()
    {
        if(_itemsParent != null)
        {
            _buffItemSlots = _itemsParent.GetComponentsInChildren<BuffItemSlot>();
        }
        RefreshUI();
    }

    private void RefreshUI()
    {
        int i = 0;
        for(;i<_buffItemSOs.Count && i< _buffItemSlots.Length; i++)
        {
            _buffItemSlots[i]._BuffItemSO = _buffItemSOs[i];
        }

        for(; i< _buffItemSlots.Length; i++)
        {
            _buffItemSlots[i]._BuffItemSO = null; 
        }
    }
}
