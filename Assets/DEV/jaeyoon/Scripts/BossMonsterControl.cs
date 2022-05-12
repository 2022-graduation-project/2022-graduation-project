// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;

// public class BossMonsterControl : MonoBehaviour
// {
//     public Transform target;
//     float attackDelay;

//     BossMonster boss;
//     Animator bossAnimator;

//     void Start()
//     {
//         boss = GetComponent<BossMonster>();
//         bossAnimator = boss.bossAnimator();
//     }

//     void Update()
//     {
//         attackDelay -= Time.deltaTime;
//         if (attackDelay < 0)
//             attackDelay = 0;

//         // Ÿ�ٰ� �ڽ��� �Ÿ��� Ȯ��
//         float distance = Vector3.Distance(transform.position, target.position);

//         // ���� ������(��Ÿ��)�� 0�� ��, �þ� ������ ���� ��
//         if (attackDelay == 0 && distance <= boss.fieldOfVision)
//         {
//             // FaceTarget();    // Ÿ�� �ٶ󺸱� (???? �ϴ��� �� ����)

//             if (distance <= boss.atkRange) // ���� ���� ���� ���� ��� ����
//             {
//                 AttackTarget();
//             }
//             else   // ���� �ִϸ��̼� ���� ���� �ƴ� ��� ����
//             {
//                 if (!bossAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
//                 {
//                     MoveToTarget();
//                 }
//             }
//         }
//         else   // �þ� ���� �ۿ� ���� �� Idle �ִϸ��̼����� ��ȯ
//         {
//             bossAnimator.SetBool("isMoving", false);
//         }
//     }

//     private void MoveToTarget()
//     {
//         float dir = target.position.x - transform.position.x;

//         dir = (dir < 0) ? -1 : 1;

//         transform.Translate(new Vector2(dir, 0) * boss.moveSpeed * Time.deltaTime);
//         bossAnimator.SetBool("isMoving", true);
//     }

//     /*
//     void FaceTarget()
//     {
//         if (target.position.x - transform.position.x < 0)
//         {
//             transform.localScale = new Vector3(-1, 1, 1);
//         }
//         else
//         {
//             transform.localScale = new Vector3(1, 1, 1);
//         }
//     }
//     */

//     void AttackTarget()
//     {
//         target.GetComponent<Player>().nowHP -= boss.atkDmg;
//         bossAnimator.SetTrigger("attack"); // ���� �ִϸ��̼� ����
//         attackDelay = boss.atkSpeed;   // ������ ����
//     }
// }
