using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpPotion : Consumable
{
    public override void Use()
    {
        print("마나 회복");
        PlayerManager.instance.playerCombat.DirectMpHeal(5f);
    }
}
