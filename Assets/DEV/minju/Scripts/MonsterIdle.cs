using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdle : MonoBehaviour
{
    //몬스터 애니메이터
    Animator anim;
    //매니저에서 배당받은 몬스터
    MonsterManager.Monster thisMon;


    public IEnumerator Idle(MonsterManager.Monster tempMon)
    {
        thisMon = tempMon;
        //Idle 상태일 때 무한 반복
        while (tempMon.state == MonsterManager.Monster.States.Idle)
        {
            //애니메이션 첫 변경 시
            if(anim.GetBool("isIdle") == false)
            {
                //몬스터 애니메이션 변경
                anim.SetBool("isIdle", true);
                anim.SetBool("isWalk", false);
                anim.SetBool("isAttack", false);
            }

            //플레이어를 찾았을 때
            if (tempMon.isFound)
            {
                //몬스터 상태를 추적 상태로 변환
                tempMon.state = MonsterManager.Monster.States.Chase;

                //플레이어를 볼 수 있으면, 추적한다.
                StartCoroutine(Chase());
                yield break;
            }

            //Patrol 어떻게?

            yield return new WaitForSeconds(1.0f);
        }
        // 다음 프레임까지 기다린다.
        yield return null;
    }


    //플레이어 스크립트
    PlayerController player;

    // 공격 주기 시간 (public)
    [HideInInspector]
    public float attackTime;

    public IEnumerator Attack()
    {
        //공격 주기 시간
        attackTime = 0f;

        //공격 상태 동안 무한 반복
        while (thisMon.state == MonsterManager.Monster.States.Attack)
        {
            //Timer start
            attackTime += Time.deltaTime;

            //플레이어가 보이는지, 플레이어 공격 범위 내에 있는지 검사
            if (!thisMon.isFound || Vector3.Distance(transform.position, thisMon.destPosition.position) > 5.0f)
            {
                //플레이어가 안보이거나, 공격 범위 내에 없을 때
                //다시 추격
                //몬스터 상태를 Attack 상태로 변환
                thisMon.state = MonsterManager.Monster.States.Chase;
                StartCoroutine(Chase());
                yield break;
            }

            //공격 주기 2.767초 초과인지 검사
            if (attackTime >= 2.767f)
            {


                //timer reset
                attackTime = 0f;

                print(thisMon.destPosition.position);

                //Start Attack
                //목표 플레이어가 있을 때 (chase 후 || 검색 collider 반경에 걸린 후)
                if (thisMon.destPosition != null)
                {
                    //플레이어 HP 깎기

                    Invoke("damaging", 1.0f);

                    //애니메이션 첫 변경 시
                    if (anim.GetBool("isAttack") == false)
                    {
                        Animating();
                    }
                }
            }
            //Wait until next frame
            yield return null;
        }

        // 다음 프레임까지 기다린다.
        yield return null;
    }

    //본인 물리작용
    Rigidbody rig;

    //움직이는 방향
    //moving direction
    public Vector3 direction;

    public IEnumerator Chase()
    {
        //애니메이션 첫 변경 시
        if (anim.GetBool("isWalk") == false)
        {
            //걷기 애니메이션
            anim.SetBool("isWalk", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isAttack", false);
        }

        //계속 추격
        while (thisMon.state == MonsterManager.Monster.States.Chase)
        {
            // 타겟을 볼 수 있을 때
            if (thisMon.isFound)
            {


                //목적지를 플레이어 위치로 설정
                thisMon.destPosition = player.GetComponent<Transform>();


                //출발지에서 목적지까지의 방향
                Vector3 direction = thisMon.destPosition.position - transform.position;


                //목적지 향해 이동
                rig.AddForce(direction, ForceMode.VelocityChange);

                // 타겟 방향으로 회전함
                transform.LookAt(Vector3.Lerp(transform.position, thisMon.destPosition.position, 0.1f * Time.deltaTime));
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 1000f * Time.deltaTime);

                //플레이어 근처 일정 거리(2f)에 도달했다면 공격
                if (Vector3.Distance(transform.position, thisMon.destPosition.position) <= 5f)
                {
                    //몬스터 상태를 Attack 상태로 변환
                    thisMon.state = MonsterManager.Monster.States.Attack;

                    //공격 함수 호출
                    StartCoroutine(Attack());
                    yield break;
                }

                //1초 뒤 다음 프레임
                yield return new WaitForSeconds(1.0f);
            }

            //타겟을 볼 수 없을 때
            else if (!thisMon.isFound)
            {
                //몬스터 상태를 Idle 상태로 변환
                thisMon.state = MonsterManager.Monster.States.Idle;

                //Idle 함수 호출
                StartCoroutine(Idle(thisMon));
                yield break;
            }
        }



        //다음 프레임까지 기다린다.
        yield return null;
    }

    void Animating()
    {
        //공격 애니메이션
        anim.SetBool("isAttack", true);
        anim.SetBool("isWalk", false);
        anim.SetBool("isIdle", false);
    }

    void damaging()
    {
        player.Damaged(-10);
    }
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 추적반경 안에 들어왔을 경우
        // player is in range of Monster's Sight
        if (other.tag == "Player")
        {
            // 찾았다고 저장
            // Monster found some player
            thisMon.isFound = true;
            
            // 목적지 플레이어 위치로 저장
            // save destination as player's location
            thisMon.destPosition = other.transform;

            // 반경 안 플레이어 저장
            // save the player
            player = other.GetComponent<PlayerController>();
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        // 플레이어가 추적반경 안에서 벗어났을 경우
        // player is out of range of Monster's Sight
        if (other.tag == "Player")
        {
            // 못찾았다고 저장
            // player is leaving the range
            thisMon.isFound = false;

            // 목적지 기본값으로 저장
            // save destination as default
            thisMon.destPosition = null;

            // 플레이어 지우기
            // remove the player
            player = null;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
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
