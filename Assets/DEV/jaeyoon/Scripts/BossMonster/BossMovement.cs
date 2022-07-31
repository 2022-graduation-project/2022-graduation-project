using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMovement : MonoBehaviour
{
    /* Monster Data & Monster Manager */
    //public MonsterData monsterData;
    //public MonsterManager monsterManager;

    private Animator animator;    // 몬스터 애니메이터
    //protected string attackTrigger; // 몬스터 기본 공격 애니메이션의 트리거(명)

    public Transform bossTarget = null; // 추적할 대상의 좌표
    protected float distance;
    protected float attackRange;    // 몬스터가 추격을 멈추고 공격을 시작할 거리

    public float speed = 1.5f;


    public Button Attack1;
    public Button Attack2;






    private void Awake()
    {
        attackRange = 1;
        animator = GetComponent<Animator>();

        if (Attack1 != null && Attack2 != null)
        {
            Attack1.onClick.AddListener(Punch);
            Attack2.onClick.AddListener(Roll);
        }
    }


    private void Update()
    {
        /* 추적 범위 내에서 플레이어 발견! */
        if (bossTarget != null)
        {
            print("Target is not null");

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
    }


    /*------------------------------------------------------
     *              ATTACK - 보스몬스터의 플레이어 공격 스킬
     * ----------------------------------------------------*/

    public void Punch()
    {
        animator.SetTrigger("Punch");
    }

    public void Roll()
    {
        animator.SetTrigger("Roll");
        transform.position += transform.forward * 6;
    }



    /*------------------------------------------------------
    *              CHASE - 몬스터가 플레이어 추격
    * ----------------------------------------------------*/

    protected void Chase()
    {
        transform.LookAt(bossTarget);   // 타겟 바라보게 함
        // 타겟 위치 받아와서 따라가도록 설정
        Vector3 dir = bossTarget.position - transform.position;
        transform.position += dir.normalized * speed * Time.deltaTime;
    }







    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player damaged!");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        // 음
    }





    /*

    void Update()
    {
        float h = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        
        Vector3 direction = new Vector3(h, 0, v);

        // 이동했을 때
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // 뒤를 바라볼 때는 이동만
            if (Mathf.Abs(angle) < 180)
            {
                angle = angle * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, angle);
            }
        }
        transform.position += direction * speed * Time.deltaTime;
    }

    */

}
