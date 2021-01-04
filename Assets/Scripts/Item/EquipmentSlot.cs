﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    public EquipmentType EquipmentType;

    protected virtual void OnValidate()
    {
        base.OnValidate();
        gameObject.name = EquipmentType.ToString() + " Slot";
    }
}
