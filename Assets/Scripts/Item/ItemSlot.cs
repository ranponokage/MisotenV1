using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] Image _image;

    public event Action<ItemSO> OnRightClickEvent;

    private ItemSO _itemSO;
    public ItemSO _ItemSO
    {
        get { return _itemSO; }
        set
        {
            _itemSO = value;

            if (_itemSO == null)
            {
                _image.enabled = false;
            }
            else
            {
                _image.sprite = _itemSO.Icon;
                _image.color = _itemSO.Color;
                _image.enabled = true;
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (_image == null)
            _image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if(_itemSO != null&& OnRightClickEvent != null)
            {
                OnRightClickEvent(_itemSO);
            }
        }
    }
}
