using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMonsterControl : MonoBehaviour
{
    public Transform target;
    float attackDelay;

    BossMonster boss;
    Animator bossAnimator;

    void Start()
    {
        boss = GetComponent<BossMonster>();
        bossAnimator = boss.bossAnimator();
    }

    void Update()
    {
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0)
            attackDelay = 0;

        // 타겟과 자신의 거리를 확인
        float distance = Vector3.Distance(transform.position, target.position);

        // 공격 딜레이(쿨타임)가 0일 때, 시야 범위에 들어올 때
        if (attackDelay == 0 && distance <= boss.fieldOfVision)
        {
            // FaceTarget();    // 타겟 바라보기 (???? 일단은 안 넣음)

            if (distance <= boss.atkRange) // 공격 범위 내에 들어올 경우 공격
            {
                AttackTarget();
            }
            else   // 공격 애니메이션 실행 중이 아닐 경우 추적
            {
                if (!bossAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    MoveToTarget();
                }
            }
        }
        else   // 시야 범위 밖에 있을 때 Idle 애니메이션으로 전환
        {
            bossAnimator.SetBool("isMoving", false);
        }
    }

    private void MoveToTarget()
    {
        float dir = target.position.x - transform.position.x;

        dir = (dir < 0) ? -1 : 1;

        transform.Translate(new Vector2(dir, 0) * boss.moveSpeed * Time.deltaTime);
        bossAnimator.SetBool("isMoving", true);
    }

    /*
    void FaceTarget()
    {
        if (target.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    */

    void AttackTarget()
    {
        target.GetComponent<Player>().nowHP -= boss.atkDmg;
        bossAnimator.SetTrigger("attack"); // 공격 애니메이션 실행
        attackDelay = boss.atkSpeed;   // 딜레이 충전
    }
}
