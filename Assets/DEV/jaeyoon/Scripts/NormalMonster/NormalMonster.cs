using System.Collections;
using UnityEngine;

public class NormalMonster : MonoBehaviour
{
    public Transform target = null; // 추적할 대상의 좌표
    protected float speed = 1.5f;  // 몬스터의 이동(추적) 속도
    protected float distance;
    protected float attackRange;
    protected float attackDelay;

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

    // 몬스터가 폭탄 맞았을 때
    protected void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Bomb")
        {
            StartCoroutine(Bursting(other.gameObject));
        }
    }

    // 폭탄 맞은 후 상태 이상 함수 5초간 지속
    private IEnumerator Bursting(GameObject bomb)
    {
        float duration = 0;
        while (duration < 5)
        {
            // MonsterData.curHP -= 1;
            // UpdateHPBar();
            yield return new WaitForSeconds(1f);
            duration++;
            
            if (duration >= 5)
            {
                // 폭탄 이펙트 끄기
                // 폭탄 오브젝트 제거
                Destroy(bomb, 0.2f);
                yield break;
            }
        }
        yield break;
    }

}