using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterChase : MonoBehaviour
{
    public Transform target = null; // 추적할 대상의 좌표
    float enemyMoveSpeed = 2f;  // 몬스터의 이동(추적) 속도

    public NavMeshAgent nav;

    private Animator animator;

    
    void Awake()
    {
        animator = GetComponent<Animator>();

        // 네비 변수 초기화
        nav = GetComponent<NavMeshAgent>();
        //nav.enabled = false;
    }

    
    void Update()
    {
        //nav.enabled = true;

        if (target != null)
        {
            animator.SetBool("Walk", true);
            transform.LookAt(target);   // 타겟 바라보게 함
            nav.SetDestination(target.position);    // 도착할 목표 위치 지정
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject.transform;
            Debug.Log("Monster : Target found");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
        Debug.Log("Monster : Target lost");
    }
}