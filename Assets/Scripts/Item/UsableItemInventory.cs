using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UsableItemInventory : MonoBehaviour
{
    [SerializeField] List<UsableItemSO> _UsableItemSOs;
    [SerializeField] Transform _itemsParent;
    [SerializeField] UsableItemSlot[] _usableItemSlots;

    private void OnValidate()
    {
        if (_itemsParent != null)
        {
            _usableItemSlots = _itemsParent.GetComponentsInChildren<UsableItemSlot>();
        }
        RefreshUI();
    }

    private void RefreshUI()
    {
        int i = 0;
        for (; i < _UsableItemSOs.Count && i < _usableItemSlots.Length; i++)
        {
            _usableItemSlots[i]._UsableItemSO = _UsableItemSOs[i];
        }

        for (; i < _usableItemSlots.Length; i++)
        {
            _usableItemSlots[i]._UsableItemSO = null;
        }
    }
}
