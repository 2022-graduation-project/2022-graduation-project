using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    //public string enemyName;
    public int maxHp;
    public int curHp;
    public int atkDmg;
    public float atkSpeed;
    public float moveSpeed;
    public float atkRange;
    public float fieldOfVision;

    private void SetBossStatus(/*string _enemyName*/, int _maxHp, int _curHp, int _atkDmg, float _atkSpeed, float _moveSpeed, float _atkRange, float _fieldOfVision)
    {
        //enemyName = _enemyName;
        maxHp = _maxHp;
        curHp = _curHp;
        atkDmg = _atkDmg;
        atkSpeed = _atkSpeed;
        moveSpeed = _moveSpeed;
        atkRange = _atkRange;
        fieldOfVision = _fieldOfVision;
    }

    public GameObject HpBar_prf;
    public GameObject canvas;
    RectTransform hpBar;    // Transform
    public float height = 1.7f;

    void Start()
    {
        hpBar = Instantiate(HpBar_prf, canvas.transform).GetComponent<RectTransform>(); // Hp Bar 정리 추가로 하자

        /* 얘는 보스 이름 들어간 거
        if (name.Equals("BossMonster"))
        {
            SetEnemyStatus("BossMonster", 100, 10, 1.5f, 2, 1.5f, 7f);
        }
        */
        SetEnemyStatus(100, 10, 1.5f, 2, 1.5f, 7f);
        curHpBar = hpBar.transform.GetChild(0).GetComponent<Image>();

        SetAttackSpeed(atkSpeed);
    }

    void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;

        curHpBar.fillAmount = (float)curHp / (float)maxHp;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompoareTag("Player"))
            {
                if (sword_man.attacked)
                {
                    curHp -= sword_man.atkDmg;
                    sword_man.attacked = false;

                    if (curHp <= 0) // 적 사망
                    {
                        Die();
                    }
                }
            }
        }
    }

    void Die()
    {
        enemyAnimator.SetTrigger("die");    // die 애니메이션 실행
        GetComponent<EnemyAI>().enabled = false;    // 추적 비활성화
        GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
        Destroy(GetComponent<Rigidbody2D>());   // 중력 비활성화
        Destroy(gameObject, 3); // 3초 후 제거
        Destroy(hpBar.gameObject, 3);   // 3초후 체력바 제거
    }

    void SetAttackSpeed(float speed)
    {
        enemyAnimator.SetFloat("attackSpeed", speed);
    }
}


    /*
    public enum CurrentState { idle, trace, attack, dead };
    public CurrentState boss = CurrentState.idle;

    private Transform bossT;
    private Transform playerT;
    private NavMeshAgent nvAgent;   // 추적

    int flag = 0;

    void Start()
    {
        bossT = this.gameObject.GetComponent<Transform>();
        //playerT = GameObject.FindWithTag("Player이름").GetComponent<Transform>();

        StartCoroutine(this.RandomMovement());
    }

    IEnumerator RandomMovement()    // IEnumerator 함수 선언
    {
        // Random Change Movement
        flag = Random.Range(0, 3);

        // Mapping Animation
        if (flag == 0)
            boss.SetBool("isMoving", false);
        else
            boss.SetBool("isMoving", true);

        // Wait 3 Seconds
        yield return new WaitForSeconds(3f);

        // Restart Logic
        StartCoroutine("MoveBoss");
    }
*/