using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChase : MonoBehaviour
{
    //몬스터 매니저 스크립트
    MonsterManager manager;
    //몬스터 정지 스크립트
    MonsterIdle idle;
    //몬스터 걷기 스크립트
    MonsterPatrol patrol;
    //몬스터 공격 스크립트
    MonsterAttack attack;
    //몬스터 애니메이터
    Animator anim;
    //본인 물리작용
    Rigidbody rig;

    public Vector3 direction;

    //플레이어 위치
    public Transform player;

    public IEnumerator Chase()
    {
        //걷기 애니메이션
        //anim.SetBool("isWalk", true);

        //계속 추격
        while (manager.test.state == MonsterManager.Monster.States.Chase)
        {
            // 타겟을 볼 수 있을 때
            if (manager.test.isFound)
            {
                //목적지를 플레이어 위치로 설정
                manager.test.destPosition = player;

                
                //출발지에서 목적지까지의 방향
                Vector3 direction = manager.test.destPosition.position - transform.position;

                
                //목적지 향해 이동
                rig.AddForce(direction, ForceMode.Impulse);

                // 타겟 방향으로 회전함
                transform.LookAt(Vector3.Lerp(transform.position, manager.test.destPosition.position, 0.1f * Time.deltaTime));
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 0.8f * Time.deltaTime);

                //1초 뒤 다음 프레임
                yield return new WaitForSeconds(1.0f);
            }
        }

        //플레이어 근처 일정 거리(1f)에 도달했다면 공격
        if(Vector3.Distance(transform.position, manager.test.destPosition.position) <= 1f)
        {
            //몬스터 상태를 Attack 상태로 변환
            manager.test.state = MonsterManager.Monster.States.Attack;

            //공격 함수 호출
            StartCoroutine(attack.Attack());
            yield break;
        }

        //다음 프레임까지 기다린다.
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        //몬스터 매니저 스크립트
        manager = GetComponent<MonsterManager>();
        //몬스터 쫓기 스크립트
        idle = GetComponent<MonsterIdle>();
        //몬스터 걷기 스크립트
        patrol = GetComponent<MonsterPatrol>();
        //몬스터 공격 스크립트
        attack = GetComponent<MonsterAttack>();
        //몬스터 애니메이터
        anim = GetComponent<Animator>();
        //본인 물리작용
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
