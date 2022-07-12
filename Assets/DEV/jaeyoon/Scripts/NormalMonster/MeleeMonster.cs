using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : NormalMonster
{
    protected override void Awake()
    {
        base.Awake();
        attackRange = 1.75f;
    }
}
