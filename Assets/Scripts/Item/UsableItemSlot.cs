using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UsableItemSlot : MonoBehaviour
{
    [SerializeField] private Image _Image;

    private UsableItemSO _usableItemSO;
    public UsableItemSO _UsableItemSO
    {
        get { return _usableItemSO; }
        set
        {
            _usableItemSO = value;
            if (_usableItemSO == null)
            {
                _Image.enabled = false;
            }
            else
            {
                _Image.sprite = _usableItemSO.Icon;
                _Image.color = _usableItemSO.Color;
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
