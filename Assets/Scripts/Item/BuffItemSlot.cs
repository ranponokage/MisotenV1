using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffItemSlot : MonoBehaviour
{
    [SerializeField] private Image _Image;

    private BuffItemSO _buffItemSO;
    public BuffItemSO _BuffItemSO
    {
        get { return _buffItemSO; }
        set 
        {
            _buffItemSO = value;
            if(_buffItemSO == null)
            {
                _Image.enabled = false;
            }
            else { 
                _Image.sprite = _buffItemSO.Icon;
                _Image.color = _buffItemSO.Color;
                _Image.enabled = true;
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (_Image == null)
            _Image = GetComponent<Image>();
    }
}
