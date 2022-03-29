using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    ////몬스터 매니저 스크립트
    //public MonsterManager manager;
    ////추격 스크립트
    //public MonsterChase chase;
    ////플레이어 스크립트
    //public PlayerController player;
    ////애니메이터
    //Animator anim;

    //// 공격 주기 시간 (public)
    //[HideInInspector]
    //public float attackTime;

    //public IEnumerator Attack()
    //{
    //    //공격 주기 시간
    //    attackTime = 0f;

    //    //공격 상태 동안 무한 반복
    //    while (manager.test.state == MonsterManager.Monster.States.Attack)
    //    {
    //        //Timer start
    //        attackTime += Time.deltaTime;

    //        //플레이어가 보이는지, 플레이어 공격 범위 내에 있는지 검사
    //        if (!manager.test.isFound || Vector3.Distance(transform.position, manager.test.destPosition.position) > 5.0f)
    //        {
    //            //플레이어가 안보이거나, 공격 범위 내에 없을 때
    //            //다시 추격
    //            //몬스터 상태를 Attack 상태로 변환
    //            manager.test.state = MonsterManager.Monster.States.Chase;
    //            StartCoroutine(chase.Chase());
    //            yield break;
    //        }

    //        //공격 주기 2.767초 초과인지 검사
    //        if (attackTime >= 2.767f)
    //        {
                

    //            //timer reset
    //            attackTime = 0f;

    //            print(manager.test.destPosition.position);

    //            //Start Attack
    //            //목표 플레이어가 있을 때 (chase 후 || 검색 collider 반경에 걸린 후)
    //            if (manager.test.destPosition != null)
    //            {
    //                //플레이어 HP 깎기

    //                Invoke("damaging", 1.0f);

    //                //애니메이션 첫 변경 시
    //                if (anim.GetBool("isAttack") == false)
    //                {
    //                    Animating();
    //                }
    //            }
    //        }
    //        //Wait until next frame
    //        yield return null;
    //    }
        
    //    // 다음 프레임까지 기다린다.
    //    yield return null;
    //}

    // Start is called before the first frame update
    void Start()
    {
        //manager = GetComponent<MonsterManager>();
        //chase = GetComponent<MonsterChase>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void Animating()
    //{
    //    //공격 애니메이션
    //    anim.SetBool("isAttack", true);
    //    anim.SetBool("isWalk", false);
    //    anim.SetBool("isIdle", false);
    //}

    //void damaging()
    //{
    //    player.Damaged(-10);
    //}
}
