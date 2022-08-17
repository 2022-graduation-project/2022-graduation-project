using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : NormalMonster
{
    protected override void Awake()
    {
        base.Awake();

        /* Melee Monster ; 공격 시 발동할 애니메이션의 트리거 명 */
        attackTrigger = "BasicAttack";

        /* 공격 범위 & 공격 지연 시간 */
        attackRange = 1.75f;
        attackDelay = 2.0f;
    }
}
