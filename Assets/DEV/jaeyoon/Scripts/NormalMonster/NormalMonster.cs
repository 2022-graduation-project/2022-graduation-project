using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalMonster : MonoBehaviour
{
    public Vector3 dir;
    public Transform target = null; // 추적할 대상의 좌표
    protected float speed = 1.5f;  // 몬스터의 이동(추적) 속도
    protected float distance;
    protected float attackRange;
    protected float attackDelay = 1.0f;

    protected Animator animator;

    
    virtual protected void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target.position);

            if (distance > attackRange)
            {
                animator.SetBool("Walk", true);
                Chase();
            }

            // 공격 범위 진입 -> 추적 중지, 공격 시작
            else
            {
                animator.SetBool("Walk", false);
                StartCoroutine("Attack");
            }
        }
    }

    IEnumerator Attack()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDelay);
    }

    private void Chase()
    {
        transform.LookAt(target);   // 타겟 바라보게 함
        dir = target.position - transform.position;
        transform.position += dir.normalized * speed * Time.deltaTime;
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