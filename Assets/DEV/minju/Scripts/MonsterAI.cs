using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterAI : MonoBehaviour
{
    public float health;

    // 몬스터 데이터 클래스
    // Monster Data class
    public class Monster
    {
        //몬스터 상태
        public enum States
        {
            Idle, Patrol, Chase, Attack
        };
        public States state;
        //몬스터 이름
        public string name;
        //몬스터 체력
        public float hp;
        //몬스터 속도
        public float moveSpeed, turnSpeed;
        //몬스터 공격력
        public float attackForce;
        //플레이어 찾았는지 여부
        public bool isFound;
        //목적지 위치 (Patrol: random location, Chase: player location)
        public Transform destPosition;
        public float distance;
    }

    // 몬스터 애니메이터
    // Monster Animator
    Animator anim;

    // 이 몬스터
    // This Monster's data var
    Monster thisMon;

    // 이 몬스터의 고유번호
    // Index of this monster from List of MonsterManager
    public int monsterIdx;

    public IEnumerator Idle()
    {
        //Idle 상태일 때 무한 반복
        while (thisMon.state == Monster.States.Idle)
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
            if (thisMon.isFound)
            {
                //몬스터 상태를 추적 상태로 변환
                thisMon.state = Monster.States.Chase;

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


    // 플레이어 스크립트
    // Script which the player has
    PlayerController player;

    // 공격 주기 시간 
    float attackTime;

    // 공격 애니메이션
    // Attack aniamating
    void Animating()
    {
        //공격 애니메이션
        anim.SetBool("isAttack", true);
        anim.SetBool("isWalk", false);
        anim.SetBool("isIdle", false);
    }

    // 플레이어 공격
    // damaging player
    void damaging()
    {
        player.Damaged(-1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            hpBarImage.fillAmount = thisMon.hp / 100;
            if (thisMon.hp <= 0f)
            {
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
            }
        }
    }

    public IEnumerator Attack()
    {
        //공격 주기 시간
        attackTime = 0f;

        //공격 상태 동안 무한 반복
        while (thisMon.state == Monster.States.Attack)
        {
            //Timer start
            attackTime += Time.deltaTime;

            //플레이어가 보이는지, 플레이어 공격 범위 내에 있는지 검사
            if (!thisMon.isFound || Vector3.Distance(transform.position, thisMon.destPosition.position) > 5.0f)
            {
                //플레이어가 안보이거나, 공격 범위 내에 없을 때
                //다시 추격
                //몬스터 상태를 Attack 상태로 변환
                thisMon.state = Monster.States.Chase;
                StartCoroutine(Chase());
                yield break;
            }

            //공격 주기 2.767초 초과인지 검사
            if (attackTime >= 2.767f)
            {
                //timer reset
                attackTime = 0f;

                //Start Attack
                //목표 플레이어가 있을 때 (chase 후 || 검색 collider 반경에 걸린 후)
                if (thisMon.destPosition != null)
                {
                    // 플레이어 HP 깎기
                    // damaging player
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
        while (thisMon.state == Monster.States.Chase)
        {
            // 타겟을 볼 수 있을 때
            if (thisMon.isFound)
            {

                //목적지를 플레이어 위치로 설정
                thisMon.destPosition = player.GetComponent<Transform>();


                //출발지에서 목적지까지의 방향
                Vector3 direction = thisMon.destPosition.position - transform.position;


                //목적지 향해 이동
                rig.AddForce(direction * Time.deltaTime * thisMon.moveSpeed, ForceMode.VelocityChange);
                //transform.Translate(direction * thisMon.moveSpeed * Time.deltaTime);

                // 타겟 방향으로 회전함
                transform.LookAt(Vector3.Lerp(transform.position, thisMon.destPosition.position, 0.1f * Time.deltaTime));
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 1000f * Time.deltaTime);

                //플레이어 근처 일정 거리(2f)에 도달했다면 공격
                if (Vector3.Distance(transform.position, thisMon.destPosition.position) <= thisMon.distance)
                {
                    //몬스터 상태를 Attack 상태로 변환
                    thisMon.state = Monster.States.Attack;

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
                thisMon.state = Monster.States.Idle;

                //Idle 함수 호출
                StartCoroutine(Idle());
                yield break;
            }
        }



        //다음 프레임까지 기다린다.
        yield return null;
    }

    // 보스몬스터인지
    // is it Boss Monster?
    public bool isBossMonster;

    // 보스 체력 게이지 바 UI
    // guage bar of boss HP
    GameObject monsterUI;
    GameObject gaugeBar;

    // 플레이어가 추적반경 안에 들어왔을 경우
    // player is in range of Monster's Sight
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 추적반경 안에 들어왔을 경우
        // player is in range of Monster's Sight
        if (other.tag == "Player")
        {
            // 이 몬스터가 보스몬스터라면
            // if this is boss monster
            if (isBossMonster)
            {
                // 보스 체력 게이지 바 생성
                // able to see the guage bar
                gaugeBar.SetActive(true);
                gaugeBar.GetComponent<Image>().fillAmount = thisMon.hp / 50f;
            }

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


    // 플레이어가 추적반경 안에서 벗어났을 경우
    // player is out of range of Monster's Sight
    private void OnTriggerExit(Collider other)
    {
        // 플레이어가 추적반경 안에서 벗어났을 경우
        // player is out of range of Monster's Sight
        if (other.tag == "Player")
        {
            // 이 몬스터가 보스몬스터라면
            // if this is boss monster
            if (isBossMonster)
            {
                // 보스 체력 게이지 바 사라지기
                // unable to see the guage bar
                gaugeBar.SetActive(false);
            }

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

    // MonsterMananger 스크립트
    // for using manager's func
    MonsterManager manager;


    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    private Canvas uiCanvas;
    private Image hpBarImage;
    GameObject hpBar;

    void SetHpBar()
    {
        uiCanvas = GameObject.Find("HealthUI").GetComponent<Canvas>();
        hpBar = Instantiate<GameObject>(hpBarPrefab, transform.position, Quaternion.identity, uiCanvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var _hpbar = hpBar.GetComponent<MonsterUI>();
        _hpbar.enemyTr = transform;
        _hpbar.offset = hpBarOffset;
    }

    void DeleteHpBar()
    {
        Destroy(hpBar);
    }


    // player가 monster 공격 했을 때 호출
    // If player damages monster this will be called
    public void Damage(int scale)
    {
        // 아직 체력이 남아 있을 때
        if (thisMon.hp > 0)
        {
            //scale(-)만큼 몬스터 체력이 줄어든다.
            thisMon.hp += scale;
            hpBarImage.fillAmount = thisMon.hp / 100;
            print("MonsterHP: " + thisMon.hp);
        }
        // 남은 체력이 없을 때
        else
        {
            Invoke("Kill", 1f);
        }
    }

    public GameObject item;
    Transform itemLocation;

    public void Kill()
    {
        itemLocation = transform;
        itemLocation.position += new Vector3(0, 1, 0);
        // 아이템 떨어트리기
        manager.DropItem(itemLocation);

        // 몬스터 삭제
        manager.DeleteMonster(monsterIdx);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("MAI ThisMonster#: " + monsterIdx);

        SetHpBar();

        // 몬스터 애니메이터
        // Monster Animator
        anim = GetComponent<Animator>();
        // 몬스터 물리작용
        // Monster Rigidbody
        rig = GetComponent<Rigidbody>();
        // 몬스터 매니저 스크립트 찾기
        // get MonsterManager script from GamaManager
        manager = GameObject.Find("MonsterManager").GetComponent<MonsterManager>();
        // 게이지바 UI 오브젝트 찾기
        // find Monster's HP guage bar
        //monsterUI = GameObject.Find("MonsterUI");
        //gaugeBar = monsterUI.transform.Find("MonsterHP").gameObject;
        //gaugeBar.SetActive(false);
        


        // 현재 오브젝트의 몬스터 기본값 처음 설정하기 (작은 몬스터)
        // setting default values of little monster
        if (!isBossMonster)
        {
            // create new data from Monster class
            thisMon = new Monster();
            // setting default state
            thisMon.state = Monster.States.Idle;
            // setting default hp
            thisMon.hp = 50f;
            // setting default speed
            thisMon.moveSpeed = 10f;
            thisMon.turnSpeed = 2f;
            // setting default power
            thisMon.attackForce = 10f;
            // setting default seeking state
            thisMon.isFound = false;
            // setting default destination
            thisMon.destPosition = null;
            // attack available distance
            thisMon.distance = 2f;
            hpBarImage.fillAmount = thisMon.hp / 100;
        }

        // 현재 오브젝트의 몬스터 기본값 처음 설정하기 (보스 몬스터)
        // setting default values of boss monster
        else
        {
            // create new data from Monster class
            thisMon = new Monster();
            // setting default state
            thisMon.state = Monster.States.Idle;
            // setting default hp
            thisMon.hp = 100f;
            // setting default speed
            thisMon.moveSpeed = 30f;
            thisMon.turnSpeed = 10f;
            // setting default power
            thisMon.attackForce = 20f;
            // setting default seeking state
            thisMon.isFound = false;
            // setting default destination
            thisMon.destPosition = null;
            // attack available distance
            thisMon.distance = 5f;
        }

        // start with default state
        StartCoroutine(Idle());
    }


    // Update is called once per frame
    void Update()
    {
        // 보스몬스터일 때 플레이어를 찾은 상태라면
        // if boss monster found any player
        if (isBossMonster && thisMon.isFound)
        {
            // 보스 체력 게이지 바 Update
            // Update the guage bar
            //gaugeBar.SetActive(true);
            gaugeBar.GetComponent<Image>().fillAmount = thisMon.hp / 50f;
        }
    }
}
