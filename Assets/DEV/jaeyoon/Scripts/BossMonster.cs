// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class BossMonster : MonoBehaviour
// {
//     //public string enemyName;
//     public int maxHp;
//     public int curHp;
//     public int atkDmg;
//     public float atkSpeed;
//     public float moveSpeed;
//     public float atkRange;
//     public float fieldOfVision;

//     private void SetBossStatus(/*string _enemyName, */int _maxHp, int _curHp, int _atkDmg, float _atkSpeed, float _moveSpeed, float _atkRange, float _fieldOfVision)
//     {
//         //enemyName = _enemyName;
//         maxHp = _maxHp;
//         curHp = _curHp;
//         atkDmg = _atkDmg;
//         atkSpeed = _atkSpeed;
//         moveSpeed = _moveSpeed;
//         atkRange = _atkRange;
//         fieldOfVision = _fieldOfVision;
//     }

//     public GameObject HpBar_prf;
//     public GameObject canvas;
//     RectTransform hpBar;    // Transform
//     public float height = 1.7f;

//     void Start()
//     {
//         hpBar = Instantiate(HpBar_prf, canvas.transform).GetComponent<RectTransform>(); // Hp Bar ���� �߰��� ����

//         /* ��� ���� �̸� �� ��
//         if (name.Equals("BossMonster"))
//         {
//             SetEnemyStatus("BossMonster", 100, 10, 1.5f, 2, 1.5f, 7f);
//         }
//         */
//         SetEnemyStatus(100, 10, 1.5f, 2, 1.5f, 7f);
//         curHpBar = hpBar.transform.GetChild(0).GetComponent<Image>();

//         SetAttackSpeed(atkSpeed);
//     }

//     void Update()
//     {
//         Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
//         hpBar.position = _hpBarPos;

//         curHpBar.fillAmount = (float)curHp / (float)maxHp;
//     }

//     public void OnTriggerEnter2D(Collider2D col)
//     {
//         if (col.CompoareTag("Player"))
//         {
//             if (sword_man.attacked)
//             {
//                 curHp -= sword_man.atkDmg;
//                 sword_man.attacked = false;

//                 if (curHp <= 0) // �� ���
//                 {
//                     Die();
//                 }
//             }
//         }
//     }

//     void Die()
//     {
//         enemyAnimator.SetTrigger("die");    // die �ִϸ��̼� ����
//         GetComponent<EnemyAI>().enabled = false;    // ���� ��Ȱ��ȭ
//         GetComponent<Collider2D>().enabled = false; // �浹ü ��Ȱ��ȭ
//         Destroy(GetComponent<Rigidbody2D>());   // �߷� ��Ȱ��ȭ
//         Destroy(gameObject, 3); // 3�� �� ����
//         Destroy(hpBar.gameObject, 3);   // 3���� ü�¹� ����
//     }

//     void SetAttackSpeed(float speed)
//     {
//         enemyAnimator.SetFloat("attackSpeed", speed);
//     }
// }


//     /*
//     public enum CurrentState { idle, trace, attack, dead };
//     public CurrentState boss = CurrentState.idle;

//     private Transform bossT;
//     private Transform playerT;
//     private NavMeshAgent nvAgent;   // ����

//     int flag = 0;

//     void Start()
//     {
//         bossT = this.gameObject.GetComponent<Transform>();
//         //playerT = GameObject.FindWithTag("Player�̸�").GetComponent<Transform>();

//         StartCoroutine(this.RandomMovement());
//     }

//     IEnumerator RandomMovement()    // IEnumerator �Լ� ����
//     {
//         // Random Change Movement
//         flag = Random.Range(0, 3);

//         // Mapping Animation
//         if (flag == 0)
//             boss.SetBool("isMoving", false);
//         else
//             boss.SetBool("isMoving", true);

//         // Wait 3 Seconds
//         yield return new WaitForSeconds(3f);

//         // Restart Logic
//         StartCoroutine("MoveBoss");
//     }
// */