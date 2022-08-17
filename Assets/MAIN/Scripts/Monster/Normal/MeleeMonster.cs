using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : NormalMonster
{
    public override void SetMonsterData()
    {
        monsterData = DataManager.instance.LoadJsonFile
              <Dictionary<string, MonsterData>>
              (Application.dataPath + "/MAIN/Data", "monster")
              ["000_skeleton"];

        Debug.Log("모델명 " + monsterData.name
            + ", 체력 " + monsterData.curHp + " / " + monsterData.maxHp
            + ", 스피드 " + monsterData.moveSpeed + "& " + monsterData.turnSpeed
            + ", 공격력 " + monsterData.attackForce);
    }

    public override void Set()
    {
        SetMonsterData();

        /* Protected Variables */
        attackDistance = 1.75f;
        attackCool = 2.0f;
}



    private void Update()
    {
        // 추적 범위 내에서 플레이어 발견!
        if (target != null)
        {
            isFound = true;
            distance = Vector3.Distance(transform.position, target.position);   // 현재 몬스터-플레이어 사이 거리 측정

            // 공격 범위보다 더 멀리 떨어져 있는 경우 -> 추적 계속
            if (distance > attackDistance)
            {
                animator.SetBool("Walk", true);
                Chase(/*monsterData.moveSpeed*/1.5f);
            }

            // 공격 범위 진입 -> 추적 중지, 공격 시작
            else
            {
                animator.SetBool("Walk", false);
                StartCoroutine(coAttack());
            }
        }

        else
        {
            animator.SetBool("Walk", false);
            isFound = false;
        }
    }



    private IEnumerator coAttack()
    {
        yield return new WaitForSeconds(attackCool);
        animator.SetTrigger("MeleeAttack"); // 몬스터 타입에 따라 공격 애니메이션 발동
    }
}
