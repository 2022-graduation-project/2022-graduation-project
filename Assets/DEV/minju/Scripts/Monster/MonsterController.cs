using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    public MonsterData monsterData;
    // MonsterMananger 스크립트
    // for using manager's func
    public MonsterManager manager;
    //본인 물리작용
    public Rigidbody rig;
    // 몬스터 애니메이터
    // Monster Animator
    public Animator anim;

    public GameObject hpBarPrefab;
    public GameObject hpBar;




    //움직이는 방향
    //moving direction //Move(), Chase()
    private Vector3 direction;
    // 플레이어 스크립트
    // Script which the player has
    private PlayerController player;
    // 각 몬스터 hpBar
    private Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    private Canvas uiCanvas;
    private Image hpBarImage;

    virtual public void Start()
    {
        SetHpBar();
        hpBar.SetActive(false);

        // 몬스터 애니메이터
        // Monster Animator
        anim = GetComponent<Animator>();
        // 몬스터 물리작용
        // Monster Rigidbody
        rig = GetComponent<Rigidbody>();
        // 몬스터 매니저 스크립트 찾기
        // get MonsterManager script from GamaManager
        manager = GameObject.Find("MonsterManager").GetComponent<MonsterManager>();


        UpdateHpBar(monsterData.curHp);

        // start with default state
        StartCoroutine(Idle());
    }

    private void DeleteHpBar()
    {
        Destroy(hpBar);
    }



    // 공격 애니메이션 함수
    // Attack aniamating
    private void Animating()
    {
        //공격 애니메이션
        anim.SetBool("isWalk", false);
        anim.SetBool("isIdle", false);
        anim.SetTrigger("Attack");
    }

    // 플레이어 공격
    // damaging player
    private void damaging()
    {

        player.Damaged(monsterData.attackForce);

    }


    // 몬스터에 어떤 것이 들어왔을 경우
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            UpdateHpBar(monsterData.curHp);
        }
    }

    // 플레이어가 추적반경 안에 들어왔을 경우
    // player is in range of Monster's Sight
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 추적반경 안에 들어왔을 경우
        // player is in range of Monster's Sight
        if (other.tag == "Player")
        {

            // 몬스터 체력 바 띄우기
            hpBar.SetActive(true);

            // 찾았다고 저장
            // Monster found some player
            monsterData.isFound = true;

            // 목적지 플레이어 위치로 저장
            // save destination as player's location
            monsterData.destPosition = other.transform;

            // 반경 안 플레이어 저장
            // save the player
            player = other.GetComponent<PlayerController>();
        }
    }


    // 플레이어가 추적반경 안에서 벗어났을 경우
    // player is out of range of Monster's Sight
    private void OnTriggerExit(Collider other)
    {
        // 플레이어가 추적반경 안에서 벗어났을 경우
        // player is out of range of Monster's Sight
        if (other.tag == "Player")
        {

            // 몬스터 체력 바 지우기
            hpBar.SetActive(false);

            // 못찾았다고 저장
            // player is leaving the range
            monsterData.isFound = false;

            // 목적지 기본값으로 저장
            // save destination as default
            monsterData.destPosition = null;

            // 플레이어 지우기
            // remove the player
            //player = null;
        }
    }

    /*---------------------------------------------
     *              DIE
     * -------------------------------------------*/

    public void Die()
    {
        Transform itemLocation;
        // 죽은 위치+1에 아이템 떨구기
        itemLocation = transform;
        itemLocation.position += new Vector3(0, 1, 0);
        // 아이템 떨어트리기
        manager.DropItem(itemLocation);
        // 몬스터 삭제
        manager.DeleteMonster(gameObject);
    }

    /*---------------------------------------------
     *              ATTACK
     * -------------------------------------------*/

    public IEnumerator Attack()
    {
        //공격 주기 시간
        float attackTime = 0f;

        //공격 상태 동안 무한 반복
        while (monsterData.state == MonsterData.States.Attack)
        {
            //Timer start
            attackTime += Time.deltaTime;

            //플레이어가 보이는지, 플레이어 공격 범위 내에 있는지 검사
            if (!monsterData.isFound || Vector3.Distance(transform.position, monsterData.destPosition.position) > monsterData.distance)
            {
                //플레이어가 안보이거나, 공격 범위 내에 없을 때
                //다시 추격
                //몬스터 상태를 Attack 상태로 변환
                monsterData.state = MonsterData.States.Chase;
                StartCoroutine(Chase());
                yield break;
            }

            //공격 주기 2.5초 초과인지 검사
            if (attackTime >= 2.5f)
            {
                //timer reset
                attackTime = 0f;

                //Start Attack
                //목표 플레이어가 있을 때 (chase 후 || 검색 collider 반경에 걸린 후)
                if (monsterData.destPosition != null)
                {
                    // 플레이어 HP 깎기
                    // damaging player
                    Invoke("damaging", 0.83f);

                    Animating();
                }
            }
            else anim.SetBool("isIdle", true);
            //Wait until next frame
            yield return null;
        }

        // 다음 프레임까지 기다린다.
        yield return null;
    }

    /*---------------------------------------------
     *              DAMAGE
     * -------------------------------------------*/


    // player가 monster 공격 했을 때 호출
    // If player damages monster this will be called
    public void Damage(float scale)
    {
        // 애니메이션
        anim.SetTrigger("Damaged");

        // 아직 체력이 남아 있을 때
        if (monsterData.curHp > 0)
        {
            //scale(-)만큼 몬스터 체력이 줄어든다.
            monsterData.curHp -= scale;
            UpdateHpBar(monsterData.curHp);
        }
        // 남은 체력이 없을 때
        else
        {
            // 죽는 애니메이션
            anim.SetBool("Dead", true);
            // 몬스터 체력바 삭제
            DeleteHpBar();
            Invoke("Die", 1f);
        }

        print(gameObject.name+"'s cur hp: " + monsterData.curHp);
    }

    /*---------------------------------------------
     *              MOVE
     * -------------------------------------------*/

    public void Move()
    {
        //목적지 향해 이동
        rig.AddForce(direction * Time.deltaTime * monsterData.moveSpeed, ForceMode.VelocityChange);

        // 타겟 방향으로 회전함
        transform.LookAt(Vector3.Lerp(transform.position, monsterData.destPosition.position, monsterData.turnSpeed * Time.deltaTime));
    }

    /*---------------------------------------------
     *              CHASE
     * -------------------------------------------*/

    public IEnumerator Chase()
    {
        //애니메이션 첫 변경 시
        if (anim.GetBool("isWalk") == false)
        {
            //걷기 애니메이션
            anim.SetBool("isWalk", true);
            anim.SetBool("isIdle", false);
        }

        //계속 추격
        while (monsterData.state == MonsterData.States.Chase)
        {
            // 타겟을 볼 수 있을 때
            if (monsterData.isFound)
            {

                // 목적지를 플레이어 위치로 설정
                monsterData.destPosition = player.GetComponent<Transform>();


                // 출발지에서 목적지까지의 방향
                Vector3 direction = (monsterData.destPosition.position - transform.position);

                //목적지 향해 이동
                Move();

                //플레이어 근처 일정 거리(2f)에 도달했다면 공격
                if (Vector3.Distance(transform.position, monsterData.destPosition.position) <= monsterData.distance)
                {
                    //몬스터 상태를 Attack 상태로 변환
                    monsterData.state = MonsterData.States.Attack;

                    //공격 함수 호출
                    StartCoroutine(Attack());
                    yield break;
                }

                //1초 뒤 다음 프레임
                yield return new WaitForSeconds(1.0f);
            }

            //타겟을 볼 수 없을 때
            else if (!monsterData.isFound)
            {
                //몬스터 상태를 Idle 상태로 변환
                monsterData.state = MonsterData.States.Idle;

                //Idle 함수 호출
                StartCoroutine(Idle());
                yield break;
            }
        }



        //다음 프레임까지 기다린다.
        yield return null;
    }

    /*---------------------------------------------
     *              Idle
     * -------------------------------------------*/

    public IEnumerator Idle()
    {
        //Idle 상태일 때 무한 반복
        while (monsterData.state == MonsterData.States.Idle)
        {
            //애니메이션 첫 변경 시
            if(anim.GetBool("isIdle") == false)
            {
                //몬스터 애니메이션 변경
                anim.SetBool("isIdle", true);
                anim.SetBool("isWalk", false);
            }

            //플레이어를 찾았을 때
            if (monsterData.isFound)
            {
                //몬스터 상태를 추적 상태로 변환
                monsterData.state = MonsterData.States.Chase;

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

    /*---------------------------------------------
     *              SETHPBAR
     * -------------------------------------------*/

    public void SetHpBar()
    {
        uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        hpBar = Instantiate<GameObject>(hpBarPrefab, transform.position, Quaternion.identity, uiCanvas.transform);
        //hpBar = Instantiate<GameObject>(hpBarPrefab, transform.position, Quaternion.identity, uiCanvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var _hpbar = hpBar.GetComponent<EnemyHpBar>();
        _hpbar.targetTr = transform;
        _hpbar.offset = hpBarOffset;
    }

    /*---------------------------------------------
     *              UPDATEHPBAR
     * -------------------------------------------*/

    public void UpdateHpBar(float hp)
    {
        hpBarImage.fillAmount = hp / monsterData.maxHp;
        if (hp <= 0f)
        {
            hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
        }
    }

}
