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

        Debug.Log("모델명 " + monsterData.name
            + ", 체력 " + monsterData.curHp + " / " + monsterData.maxHp
            + ", 스피드 " + monsterData.moveSpeed + " & " + monsterData.turnSpeed
            + ", 공격력 " + monsterData.attackForce);
    }

    public override void Set()
    {
        SetMonsterData();

        /* Protected Variables */
        attackDistance = 1.75f;
        attackCool = 2.0f;

        Debug.Log("(Melee Monster) Set 완료");
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
