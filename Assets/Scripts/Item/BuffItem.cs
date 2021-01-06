using System.Collections;
using UnityEngine;


public class BuffItem : MonoBehaviour
{
    [SerializeField] UsableItemSO _BufferitemSO;
    // Use this for initialization

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<PlayerControl>();
        if (player == null)
            return;

        _BufferitemSO.UseItem(player);
    }
}
