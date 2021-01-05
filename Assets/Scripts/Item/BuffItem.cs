using System.Collections;
using UnityEngine;


public class BuffItem : MonoBehaviour
{
    [SerializeField] UsableItemSO _itemSO;
    // Use this for initialization

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<PlayerControl>();
        if (player == null)
            return;

        _itemSO.UseItem(player);

        _itemSO.UpdatePlayerUI(player);
    }
}
