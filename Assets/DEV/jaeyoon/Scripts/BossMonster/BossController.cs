using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public BossManager bossManager;


    /* Monster Data & Monster Manager */
    //public MonsterData monsterData;
    //public MonsterManager monsterManager;

    private Animator animator;    // 보스몬스터 애니메이터
    //protected string attackTrigger; // 몬스터 기본 공격 애니메이션의 트리거(명)

    public Transform bossTarget = null; // 추적할 대상의 좌표
    protected float distance;
    protected float attackRange;    // 몬스터가 추격을 멈추고 공격을 시작할 거리

    public float speed = 1.5f;


    private GameObject Chasing;



    /* Boss Monster HP Bar */
    public Slider HpBar;
    public Text HpText;

    private float maxHealth = 100;
    private float minHealth = 0;
    private float hp;
    public float damage;




    /* 테스트용~!~!~~!~!~! UI button, 어쩌구들~!~!!~!~! */
    public Button Attack1Btn;
    public Button Attack2Btn;
    public Button DamagedBtn;






    private void Awake()
    {
        bossManager = GameObject.Find("BossManager").GetComponent<BossManager>();



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


        Chasing = GameObject.Find("ChaseRange");
    }


    private void Update()
    {
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
        Chasing.SetActive(false);
        animator.SetTrigger("Punch");
        Chasing.SetActive(true);
    }

    public void Roll()
    {
        Chasing.SetActive(false);
        bossManager.isAttacking = true;
        StartCoroutine(Forward());
        Chasing.SetActive(true);
        bossManager.isAttacking = false;
    }
    IEnumerator Forward()
    {
        animator.SetTrigger("Roll");

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
    *              DAMAGED - 플레이어 공격으로 보스 데미지
    * ----------------------------------------------------*/

    private void onDamage()
    {
        if (hp > 0)
        {
            hp -= damage;

            if (hp > 0)
            {
                animator.SetTrigger("Damaged");
            }
            else
            {
                hp = 0;
                Debug.Log("Boss Monster died!");
                animator.SetBool("Dead", true);

                //StartCoroutine(Die());
            }
        }

        Chasing.SetActive(false);
    }


    /*------------------------------------------------------
    *              DIE - 몬스터 사망
    * ----------------------------------------------------*/

    IEnumerator Die()
    {
        yield return new WaitForSeconds(10.0f);

        this.gameObject.SetActive(false);
    }







    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player damaged!");
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("데미지 계속 받는 중");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        // 음
    }
}
