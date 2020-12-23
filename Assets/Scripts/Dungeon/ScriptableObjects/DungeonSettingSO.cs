using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon Setting")]
public class DungeonSettingSO : ScriptableObject
{
    [Header("Room Info")]
    public int RoomNumber;

    [Header("Item")]
    public List<GameObject> Items;
    public int maxItem;
}
