using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[Serializable]
public class CharacterStat
{
    public float BaseValue;

    public float Value
    {
        get
        {
            if (isDirty|| BaseValue != _lastBaseValue)
            {
                _lastBaseValue = BaseValue;
                _lastFinalValue = CalculateFinalValue();
                isDirty = false;
            }
            return _lastFinalValue;
        }
    }

    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

    protected bool isDirty = true;
    protected float _lastFinalValue;
    protected float _lastBaseValue = float.MinValue;

    protected readonly List<StatModifier> statModifiers;


    public CharacterStat(float baseValue)
    {
        BaseValue = baseValue;
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }

    public void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }

    protected int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0;//
    }
    public bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    public bool RemoveAllModifierFromSource(object source)
    {
        bool didRemove = false;
        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    protected float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0;
        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];
            switch (mod.Type)
            {
                case StatModType.Flat:
                    finalValue += mod.Value;
                    break;
                case StatModType.PercentAdd:
                    sumPercentAdd += mod.Value;
                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                    break;
                case StatModType.PercentMult:
                    finalValue *= 1 + mod.Value;
                    break;
                default:
                    break;
            }

            finalValue += statModifiers[i].Value;
        }

        return (float)Math.Round(finalValue, 4);
    }
}
