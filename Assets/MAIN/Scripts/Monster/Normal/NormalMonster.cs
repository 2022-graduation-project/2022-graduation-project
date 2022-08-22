using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonster : Monster
{
    /* Monster Manager */
    //public MonsterManager monsterManager;


    public Transform target = null; // 추적할 대상의 좌표
    public bool isFound = false;
    public string spawnLoc = "";


    /* Protected Variables */
    protected float attackDistance;    // 몬스터가 추격을 멈추고 공격을 시작할 거리
    protected float attackCool;    // 자동 공격 쿨타임

    /* Local Variables */
    protected float distance;



    protected virtual void Update()
    {
        // 추적 범위 내에서 플레이어 발견!
        if (target != null)
        {
            isFound = true;
            transform.LookAt(target);   // target 바라보게 함
            distance = Vector3.Distance(transform.position, target.position);   // 현재 몬스터-플레이어 사이 거리 측정

            // 공격 범위보다 더 멀리 떨어져 있는 경우 -> 추적 계속
            if (distance > attackDistance)
                StartChasing();
            // 공격 범위 진입 -> 추적 중지, 공격 시작
            else
                StopChasing();
        }
        // 플레이어 아직 발견 못 했거나 놓침
        else
        {
            animator.SetBool("Walk", false);
            isFound = false;
        }
    }



    /*----------------------------------------------------------------
     *              CHASE - 몬스터가 플레이어 추격
     * --------------------------------------------------------------*/

    public override void Chase(float _speed)
    {
        // 타겟 위치 받아와서 따라가도록 설정
        Vector3 dir = target.position - transform.position;
        transform.position += dir.normalized * _speed * Time.deltaTime;
    }

    public virtual void StartChasing()
    {
        animator.SetBool("Walk", true);
        Chase(monsterData.moveSpeed);
    }
    public virtual void StopChasing()
    {
        animator.SetBool("Walk", false);
        StartCoroutine(coAttack());
    }


    /*----------------------------------------------------------------
     *              ATTACK, SKILL - 몬스터 공격
     * --------------------------------------------------------------*/

    public override void Attack() { }
    public override void Skill() { }

    public virtual IEnumerator coAttack() { yield return null; }


    /*----------------------------------------------------------------
     *              DAMAGED - 플레이어 공격으로 몬스터 데미지
     * --------------------------------------------------------------*/

    public override void Damaged(float _damage, PlayerController _player = null)
    {
        animator.SetTrigger("Damaged"); // 애니메이션


        if (monsterData.curHp > 0) // 아직 체력이 남아 있을 때
        {
            monsterData.curHp -= _damage; // scale(+)만큼 몬스터 체력 감소
            //UpdateHpBar(monsterData.curHp);   // 몬스터 체력바 반영
        }

        else  // 남은 체력이 없을 때 -> 사망
        {
            animator.SetBool("Dead", true);
            //DeleteHpBar();    // 몬스터 체력바 삭제
            Invoke("Die", 1f);
        }

        print("Monster HP: " + monsterData.curHp);
    }

    // PlayerController 오류 나서 임시로 카피해둔 것 -> 오류 해결하고 삭제할 것 !!!
    public void dDamaged(float _damage)
    {
        if (monsterData.curHp - _damage > 0) // 아직 체력이 남아 있을 때
        {
            animator.SetTrigger("Damaged"); // 애니메이션
            monsterData.curHp -= _damage; // scale(+)만큼 몬스터 체력 감소
            //UpdateHpBar(monsterData.curHp);   // 몬스터 체력바 반영
        }

        else  // 남은 체력이 없을 때 -> 사망
        {
            animator.SetBool("Dead", true);
            //DeleteHpBar();    // 몬스터 체력바 삭제
            Invoke("Die", 3f);
        }

        print("Monster HP: " + monsterData.curHp);
    }

    // 몬스터가 폭탄 맞았을 때
    protected void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Bomb")
        {
            StartCoroutine(Bursting());
        }
    }

    // 폭탄 맞은 후 상태 이상 함수 5초간 지속
    private IEnumerator Bursting()
    {
        float duration = 0;
        while (duration < 5)
        {
            
            // 몬스터 공격
            Damaged(10);
            print("Damaged -10 by bomb");
            // 몬스터 상태바
            // monster.UpdateHPBar();
            // 지속 시간 재기
            yield return new WaitForSeconds(1f);
            duration++;
            
            // 지속 시간 초과
            if (duration >= 5)
            {
                // 폭탄 이펙트 끄기
                // 폭탄 오브젝트 제거
                yield break;
            }
        }
        yield break;
    }


    /*----------------------------------------------------------------
     *              DIE - 몬스터 사망
     * --------------------------------------------------------------*/

    public override void Die()
    {
        // Transform itemLocation;
        // // 죽은 위치+1에 아이템 떨구기
        // itemLocation = transform;
        // itemLocation.position += new Vector3(0, 1, 0);
        // 아이템 떨어트리기
        //monsterManager.DropItem(itemLocation);
        // 몬스터 삭제
        //monsterManager.DeleteMonster(gameObject);

        /*
        print(spawnLoc+"'s Monster DIED");
        if(spawnLoc!="")
        {
            //monsterManager.gameObject.GetComponent<MonsterSpawn>().Decode(spawnLoc);
        }
        */

        gameObject.SetActive(false);
    }
}