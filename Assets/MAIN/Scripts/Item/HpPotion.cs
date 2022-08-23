using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPotion : Consumable
{
    public override void Use()
    {
        print("체력 회복");
    }
}
