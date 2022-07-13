using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDesMonster : NormalMonster
{
    protected override void Awake()
    {
        base.Awake();
        attackRange = 2.5f;
    }

    protected override void Update()
    {
        base.Update();
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);


    }
}
