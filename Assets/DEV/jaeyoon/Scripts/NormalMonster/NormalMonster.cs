using System.Collections;
using UnityEngine;

public class NormalMonster : MonoBehaviour
{
    public Transform target = null; // 추적할 대상의 좌표
    protected float speed = 1.5f;  // 몬스터의 이동(추적) 속도
    protected float distance;
    protected float attackRange;
    protected float attackDelay = 1.0f;

    protected Animator animator;

    
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    protected virtual void Update()
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

    protected void Chase()
    {
        transform.LookAt(target);   // 타겟 바라보게 함
        // 타겟 위치 받아와서 따라가도록 설정
        Vector3 dir = target.position - transform.position;
        transform.position += dir.normalized * speed * Time.deltaTime;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject.transform;
            Debug.Log("Monster : Target found");
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        target = null;
        Debug.Log("Monster : Target lost");
    }
}