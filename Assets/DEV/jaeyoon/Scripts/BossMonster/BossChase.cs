using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossChase : MonoBehaviour
{
    public BossManager bossManager;


    /* (부모) 보스몬스터 오브젝트 변수 */
    private Transform boss = null;
    private Animator animator;

    public Transform bossTarget = null; // 추적할 대상의 좌표
    private float distance;
    private float attackRange;    // 몬스터가 추격을 멈추고 공격을 시작할 거리
    private float speed = 1.5f; // 이동 속도





    private void Awake()
    {
        bossManager = GameObject.Find("BossManager").GetComponent<BossManager>();


        animator = transform.parent.gameObject.GetComponent<Animator>();
        boss = transform.parent.gameObject.transform;

        attackRange = 2.0f;
    }

    private void Update()
    {
        /* 추적 범위 내에서 플레이어 발견! */
        if (bossTarget != null && bossManager.isAttacking == false)
        {
            distance = Vector3.Distance(transform.position, bossTarget.position);   // 현재 몬스터-플레이어 사이 거리 측정

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
                //StartCoroutine("Attack", attackDelay);
            }
        }

        else
        {
            animator.SetBool("Walk", false);
        }
    }



    /*------------------------------------------------------
    *              CHASE - 몬스터가 플레이어 추격
    * ----------------------------------------------------*/

    protected void Chase()
    {
        boss.LookAt(bossTarget);   // 타겟 바라보게 함

        // 타겟 위치 받아와서 따라가도록 설정
        Vector3 dir = bossTarget.position - boss.position;
        boss.position += dir.normalized * speed * Time.deltaTime;
    }







    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            bossTarget = other.gameObject.transform;
            Debug.Log("Boss : Target found");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        bossTarget = null;
        Debug.Log("Boss : Target lost");
    }



}
