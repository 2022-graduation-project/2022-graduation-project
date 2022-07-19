using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonster : MonoBehaviour
{
    /* Monster Data & Monster Manager */
    public MonsterData monsterData;
    public MonsterManager monsterManager;

    protected Animator animator;    // 몬스터 애니메이터
    protected string attackTrigger; // 몬스터 기본 공격 애니메이션의 트리거(명)

    public Transform target = null; // 추적할 대상의 좌표
    protected float distance;
    protected float attackRange;    // 몬스터가 추격을 멈추고 공격을 시작할 거리
    protected float attackDelay;    // 자동 공격 지연 시간



    protected virtual void Awake()
    {
        /*
        monsterData = DataManager.instance.LoadJsonFile
                      <Dictionary<string, MonsterData>>
                      (Application.dataPath + "/MAIN/Data", "goblin")
                      ["001_goblin"];
        */
        monsterManager = GameObject.Find("MonsterManager").GetComponent<MonsterManager>();
        animator = GetComponent<Animator>();
        //monsterData.moveSpeed = 1.5f;  // 몬스터 이동 속도

        /* 몬스터 초기 HP 설정 */
        //monsterData.curHp = monsterData.maxHp = 100f;
    }

    
    protected virtual void Update()
    {
        /* 추적 범위 내에서 플레이어 발견! */
        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target.position);   // 현재 몬스터-플레이어 사이 거리 측정

            /* 공격 범위보다 더 멀리 떨어져 있는 경우 -> 추적 계속 */
            if (distance > attackRange)
            {
                animator.SetBool("Walk", true);
                Chase();
            }

            /* 공격 범위 진입 -> 추적 중지, 공격 시작 */
            else
            {
                animator.SetBool("Walk", false);
                StartCoroutine("Attack", attackDelay);
            }
        }
    }


    /* 추적 범위 내에 플레이어 진입 -> Target 설정 */
    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject.transform;
            Debug.Log("Monster : Target found");
        }
    }

    /* 추적 범위 내에서 플레이어 사라짐 */
    protected void OnTriggerExit(Collider other)
    {
        target = null;
        Debug.Log("Monster : Target lost");
    }




    /*------------------------------------------------------
     *              CHASE - 몬스터가 플레이어 추격
     * ----------------------------------------------------*/

    protected void Chase()
    {
        transform.LookAt(target);   // 타겟 바라보게 함
        // 타겟 위치 받아와서 따라가도록 설정
        Vector3 dir = target.position - transform.position;
        transform.position += dir.normalized * /*monsterData.moveSpeed*/1.5f * Time.deltaTime;
    }


    /*------------------------------------------------------
     *              ATTACK - 몬스터가 플레이어 주기적으로 공격
     * ----------------------------------------------------*/

    IEnumerator Attack()
    {
        animator.SetTrigger(attackTrigger); // 몬스터 타입에 따라 공격 애니메이션 발동
        yield return new WaitForSeconds(attackDelay);
    }


    /*------------------------------------------------------
     *              DAMAGED - 플레이어 공격으로 몬스터 데미지
     * ----------------------------------------------------*/

    public void Damaged(float scale)
    {
        animator.SetTrigger("Damaged"); // 애니메이션

        /*
        if (monsterData.curHp > 0) // 아직 체력이 남아 있을 때
        {
            monsterData.curHp += scale; // scale(-)만큼 몬스터 체력 감소
            //UpdateHpBar(monsterData.curHp);   // 몬스터 체력바 반영
        }

        else  // 남은 체력이 없을 때 -> 사망
        {
            animator.SetBool("Dead", true);
            //DeleteHpBar();    // 몬스터 체력바 삭제
            Invoke("Die", 1f);
        }

        
        */
        print("Monster damaged!");
    }

    // 몬스터가 폭탄 맞았을 때
    protected void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Bomb")
        {
            StartCoroutine(Bursting(other.gameObject));
        }
    }

    // 폭탄 맞은 후 상태 이상 함수 5초간 지속
    private IEnumerator Bursting(GameObject bomb)
    {
        float duration = 0;
        while (duration < 5)
        {
            // MonsterData.curHP -= 1;
            // UpdateHPBar();
            yield return new WaitForSeconds(1f);
            duration++;
            
            if (duration >= 5)
            {
                // 폭탄 이펙트 끄기
                // 폭탄 오브젝트 제거
                Destroy(bomb, 0.2f);
                yield break;
            }
        }
        yield break;
    }


    /*------------------------------------------------------
     *              DIE - 몬스터 사망
     * ----------------------------------------------------*/

    public void Die()
    {
        Transform itemLocation;
        // 죽은 위치+1에 아이템 떨구기
        itemLocation = transform;
        itemLocation.position += new Vector3(0, 1, 0);
        // 아이템 떨어트리기
        monsterManager.DropItem(itemLocation);
        // 몬스터 삭제
        monsterManager.DeleteMonster(gameObject);
    }
}