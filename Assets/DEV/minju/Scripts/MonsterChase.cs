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
    public IEnumerator Chase()
    {
        //걷기 애니메이션
        anim.SetBool("isWalk", true);
        while (manager.test.state == MonsterData.Monster.States.Chase)
        {
            if (manager.test.isFound)
            {
                //목적지를 플레이어 위치로 설정
                //manager.test.destPosition = PlayerController.tr;

                //출발지에서 목적지까지의 방향
                Vector3 direction = manager.test.destPosition.position - transform.position;

                //목적지 향해 이동 (Z값 설정 아직)
                rig.AddForce(direction, ForceMode.Impulse);
            }
        }
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
