using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldPotion : Consumable
{
    public override void Use()
    {
        float shieldDuration = 10f;
        PlayerManager.instance.playerCombat.AddEffect("Shield", 0, shieldDuration);
    }
}
