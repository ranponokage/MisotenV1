using System.Collections;
using UnityEngine;
public class UsableItem : MonoBehaviour
{
    [SerializeField] UsableItemSO _itemSO;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<PlayerControl>();
        if (player == null)
            return;

        player.PickedUp(_itemSO);
    }
}

