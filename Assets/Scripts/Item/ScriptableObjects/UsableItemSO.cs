using System;
using System.Collections;
using UnityEngine;
using Wyt.CharacterStats;

[CreateAssetMenu(menuName = "Item/UsableItemSO")]
public class UsableItemSO : ItemSO
{
    public int SpeedBonus;
    public int HunggerBonus;
    public int SamBonus;

    [Space]
    public float SpeedPercentBonus;
    public float HunggerPercentBonus;
    public float SamPercentBonus;

    public float Duration = -1;

    internal void UpdatePlayerUI(PlayerControl player)
    {
        throw new NotImplementedException();
    }

    public bool InvertAxis;
    public bool BlurCamera;


    public void UseItem(PlayerControl player)
    {
        player.defaultSpeed.AddModifier(new StatModifier(SpeedBonus, StatModType.Flat, this));
        player.Hunger.AddModifier(new StatModifier(HunggerBonus, StatModType.Flat, this));
        player.Sam.AddModifier(new StatModifier(SamBonus, StatModType.Flat, this));

        if (Duration <= -1)
            return;

        if (!InvertAxis) return;
        else player.InVertAxis(Duration);

        if (!BlurCamera) return;
        else player.BlurCamera(Duration);

        player.StartCoroutine(Timer(Duration, player));
        
    }

    private IEnumerator Timer(float duration, PlayerControl playerStats)
    {
        yield return new WaitForSeconds(duration);
        playerStats.Hunger.RemoveAllModifiersFromSource(this);
        playerStats.defaultSpeed.RemoveAllModifiersFromSource(this);
        playerStats.Sam.RemoveAllModifiersFromSource(this);

    }
}
