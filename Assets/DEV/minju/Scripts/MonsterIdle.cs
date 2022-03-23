using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdle : MonoBehaviour
{
    //몬스터 매니저 스크립트
    MonsterManager manager;
    //몬스터 쫓기 스크립트
    MonsterChase chase;
    //몬스터 걷기 스크립트
    MonsterPatrol patrol;
    //몬스터 애니메이터
    Animator anim;

    public IEnumerator Idle()
    {
        //몬스터 애니메이션 변경
        anim.SetBool("isIdle", true);

        //Idle 상태일 때 무한 반복
        while(manager.test.state == MonsterData.Monster.States.Idle)
        {
            //플레이어를 찾았을 때
            if (manager.test.isFound)
            {
                //몬스터 상태를 추적 상태로 변환
                manager.test.state = MonsterData.Monster.States.Chase;

                //플레이어를 볼 수 있으면, 추적한다.
                StartCoroutine(chase.Chase());
                yield break;
            }
            //Patrol 어떻게?
        }
        // 다음 프레임까지 기다린다.
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        //몬스터 매니저 스크립트
        manager = GetComponent<MonsterManager>();
        //몬스터 쫓기 스크립트
        chase = GetComponent<MonsterChase>();
        //몬스터 걷기 스크립트
        patrol = GetComponent<MonsterPatrol>();
        //몬스터 애니메이터
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
