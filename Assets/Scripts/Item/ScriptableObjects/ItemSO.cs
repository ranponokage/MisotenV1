using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string ItemName;
    public Sprite Icon;
    public Color Color;
}
