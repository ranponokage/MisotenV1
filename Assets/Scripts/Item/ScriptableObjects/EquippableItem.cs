using System.Collections;
using UnityEngine;

public enum EquipmentType
{
    UsableItem,
}

[CreateAssetMenu(menuName = "Item/EquippableItemSO")]
public class EquippableItemSO : ItemSO
{
    public int SpeedBonus;
    public int HunggerBonus;
    public int SamBonus;

    [Space]
    public float SpeedPercentBonus;
    public float HunggerPercentBonus;
    public float SamPercentBonus;

    [Space]
    public EquipmentType _EquipmentType;

}
