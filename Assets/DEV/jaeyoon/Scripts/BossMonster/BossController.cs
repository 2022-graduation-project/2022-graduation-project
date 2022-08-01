using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    /* Monster Data & Monster Manager */
    //public MonsterData monsterData;
    //public MonsterManager monsterManager;

    private Animator animator;    // 보스몬스터 애니메이터
    //protected string attackTrigger; // 몬스터 기본 공격 애니메이션의 트리거(명)

    public Transform bossTarget = null; // 추적할 대상의 좌표
    protected float distance;
    protected float attackRange;    // 몬스터가 추격을 멈추고 공격을 시작할 거리

    public float speed = 1.5f;



    /* Boss Monster HP Bar */
    public Slider HpBar;
    public Text HpText;

    private float maxHealth = 100;
    private float minHealth = 0;
    private float hp;
    private float damage = 30;




    /* 테스트용~!~!~~!~!~! UI button, 어쩌구들~!~!!~!~! */
    public Button Attack1Btn;
    public Button Attack2Btn;
    public Button DamagedBtn;






    private void Awake()
    {
        hp = maxHealth;


        attackRange = 1;
        animator = GetComponent<Animator>();

        //HpBar = GameObject.Find("Canvas").GetComponent<Slider>();
        //HpText = GameObject.Find("Canvas").GetComponent<Text>();

        if (Attack1Btn != null && Attack2Btn != null && DamagedBtn != null)
        {
            Attack1Btn.onClick.AddListener(Punch);
            Attack2Btn.onClick.AddListener(Roll);
            DamagedBtn.onClick.AddListener(onDamage);
        }
    }


    private void Update()
    {
        /* 추적 범위 내에서 플레이어 발견! */
        if (bossTarget != null)
        {
            Debug.Log("Target is not null");

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


        /* HP bar 세팅 */
        if (hp <= 10)
        {
            HpText.color = Color.red;
        }
        HpText.text = hp.ToString();
        HpBar.value = (hp / maxHealth);

        if (HpBar.value <= minHealth)
            HpBar.transform.Find("Fill Area").gameObject.SetActive(false);
        else
            HpBar.transform.Find("Fill Area").gameObject.SetActive(true);
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
        StartCoroutine(Forward());
    }
    IEnumerator Forward()
    {
        float time = 1.0f;
        float curTime = 0f;
        while (curTime < time)
        {
            curTime += Time.deltaTime;
            transform.position += transform.forward * 8f * Time.deltaTime;
            yield return null;
        }
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



    /*------------------------------------------------------
    *              DAMAGED - 플레이어 공격으로 보스 데미지
    * ----------------------------------------------------*/

    private void onDamage()
    {
        if (hp > 0)
        {
            hp -= damage;

            if (hp > 0)
            {
                Debug.Log("HP : " + hp);
                animator.SetTrigger("Damaged");
            }
            else
            {
                hp = 0;
                Debug.Log("Boss Monster died!");
                animator.SetBool("Dead", true);
            }
        }
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
}
