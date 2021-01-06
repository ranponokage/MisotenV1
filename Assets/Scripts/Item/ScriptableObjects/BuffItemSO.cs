using System;
using System.Collections;
using UnityEngine;
using Wyt.CharacterStats;

[CreateAssetMenu(menuName = "Item/BuffItemSO", fileName = "BuffItemSO")]
public class BuffItemSO : ItemSO
{
    public int SpeedBonus;
    public int HunggerBonus;
    public int SamBonus;

    public int MassBonus;
    public int SizeBonus;

    [Space]
    public float SpeedPercentBonus;
    public float HunggerPercentBonus;
    public float SamPercentBonus;

    public float Duration = -1;


    public bool InvertAxis;
    public bool BlurCamera;


    public void UseItem(PlayerControl player)
    {
        if (SpeedBonus != 0)
        {
            player.Speed.AddModifier(new StatModifier(SpeedBonus, StatModType.Flat, this));
        }
        if (HunggerBonus != 0)
        {
            player.Hunger.AddModifier(new StatModifier(HunggerBonus, StatModType.Flat, this));
        }
        if (SamBonus != 0)
        {
            player.Sam.AddModifier(new StatModifier(SamBonus, StatModType.Flat, this));
        }
        if (MassBonus != 0)
        {
            player.Mass.AddModifier(new StatModifier(MassBonus, StatModType.Flat, this));
            player._rigidBody.mass = player.Mass.Value;
        }
        if (SizeBonus != 0)
        {
            player.Size.AddModifier(new StatModifier(SizeBonus, StatModType.Flat, this));
            player.CharacterModel.transform.localScale *= player.Size.Value;
        }

        if (Duration <= -1)
            return;
        player.StartCoroutine(BufferTimer(Duration, player));

        if (!InvertAxis) return;
        else player.InVertAxis(Duration);

        if (!BlurCamera) return;
        else player.BlurCamera(Duration);


    }

    private IEnumerator BufferTimer(float duration, PlayerControl playerStats)
    {
        yield return new WaitForSeconds(duration);
        playerStats.Hunger.RemoveAllModifiersFromSource(this);
        playerStats.Speed.RemoveAllModifiersFromSource(this);
        playerStats.Sam.RemoveAllModifiersFromSource(this);
        playerStats.Mass.RemoveAllModifiersFromSource(this);
        playerStats.Size.RemoveAllModifiersFromSource(this);

    }

}
