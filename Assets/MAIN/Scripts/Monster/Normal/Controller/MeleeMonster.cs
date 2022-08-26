using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : NormalMonster
{
    /*----------------------------------------------------------------
     *              Inherited Methods (SetMonsterData, Set)
     * --------------------------------------------------------------*/

    public override void SetMonsterData()
    {
        monsterData = DataManager.instance.LoadJsonFile
              <Dictionary<string, MonsterData>>
              (Application.dataPath + "/MAIN/Data", "monster")
              ["000_skeleton"];
    }

    public override void Set()
    {
        base.Set();

        SetMonsterData();

        /* Protected Variables */
        attackDistance = 1.75f;
        attackCool = 2.0f;
    }


    /*----------------------------------------------------------------
     *              [Melee Monster] 몬스터 별 공격 코루틴
     * --------------------------------------------------------------*/

    public override IEnumerator coAttack()
    {
        yield return new WaitForSeconds(attackCool);
        animator.SetTrigger("MeleeAttack"); // 몬스터 타입에 따라 공격 애니메이션 발동
    }
}
