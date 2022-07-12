using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMonster : NormalMonster
{
    protected override void Awake()
    {
        base.Awake();
        attackRange = 3.0f;
    }
}
