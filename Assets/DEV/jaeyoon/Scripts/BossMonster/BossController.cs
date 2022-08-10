using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossController : MonoBehaviour
{
    /* enum */
    private enum Skills
    {
        StoneStorm,
        EarthQuake,
        RollStone
    }


    /* 데이터 */
    private MonsterDataDummy monsterData;


    /* 컴포넌트 */
    private Transform tr;
    private Animator animator;


    /* 공격 지역 */
    private Transform circle;
    [SerializeField] private Transform square;
    private GameObject cube;    // Rolling Collider


    /* 런타임 변수 */
    private PlayerDummy targetPlayer;

    private bool finalAttack = false;
    private float maxDistance = 5f;

    private IEnumerator recover;
    private IEnumerator checkTargetDistance;
    private IEnumerator chase;

    private WaitForSeconds waitOneSecond = new WaitForSeconds(1.0f);
    private WaitForSeconds waitThreeSeconds = new WaitForSeconds(3.0f);

    private bool moveable = true;


    /* ObjectPool */
    [SerializeField] private Transform[] rocks;
    [SerializeField] private Transform[] stalagmites;





    void Awake()
    {
        print("어웨이크");
    }

    void Start()
    {
        print("실행");

        monsterData = DataManager.instance.LoadJsonFile
                      <Dictionary<string, MonsterDataDummy>>
                      (Application.dataPath + "/MAIN/Data", "boss")
                      ["000_golem"];

        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();

        circle = tr.Find("Circle");
        square = tr.Find("Square");
        cube = tr.Find("CubeCol").gameObject;

        rocks = circle.GetChild(0).GetComponentsInChildren<Transform>(true);
        //stalagmites = square.GetChild(0).GetComponentsInChildren<Transform>(true);

        CheckTarget();
    }





    /*************************************************************************/
    /*                         targetPlayer 사이클                           */
    /*************************************************************************/




    void CheckTarget()
    {
        if(!moveable)
        {
            Invoke("CheckTarget", 1f);
            return;
        }

        /* 가장 가까운 곳에 있는 targetPlayer 찾기 */
        if (targetPlayer == null)
        {
            if (chase != null)
            {
                StopCoroutine(chase);
                chase = null;
            }

            int layerMask = 1 << LayerMask.GetMask("Player");

            // 왜 -1을 해줘야 하는지는 의문... default 레이어도 빼려면 -1 안하는게 정상 아닌가? 허허
            RaycastHit[] hits = Physics.SphereCastAll(tr.position, 5f, tr.forward, 5f, ~layerMask - 1);

            float minDistance = maxDistance;
            foreach (RaycastHit hit in hits)
            {
                float distance = Vector3.Distance(hit.transform.position, tr.position);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    targetPlayer = hit.transform.GetComponent<PlayerDummy>();
                }
            }

            print($"가장 가까운 오브젝트(거리, 이름) : {minDistance}, {targetPlayer?.tr.name}");
        }


        /* 범위 내에 아무도 없어서 targetPlayer를 찾지 못한 경우 */
        if (targetPlayer == null)
        {
            // 대기

            Invoke("CheckTarget", 1f);
            if (recover == null)
            {
                recover = Recover();
                StartCoroutine(recover);
            }
        }


        /* targetPlayer를 찾은 경우 */
        else
        {
            if (recover != null)
            {
                StopCoroutine(recover);
                recover = null;
            }

            checkTargetDistance = CheckTargetDistance();
            StartCoroutine(checkTargetDistance);

            chase = Chase();
            StartCoroutine(chase);
        }
    }








    IEnumerator CheckTargetDistance()
    {
        while (targetPlayer != null && moveable != false)
        {
            if (maxDistance < Vector3.Distance(targetPlayer.tr.position, tr.position))
            {
                break;
            }

            yield return waitOneSecond;
        }

        targetPlayer = null;
        checkTargetDistance = null;
        CheckTarget();
    }









    IEnumerator Recover()
    {
        while (true)
        {
            monsterData.curHp += 1 * Time.deltaTime;
            yield return null;
        }
    }

    /*************************************************************************/
    /*************************************************************************/
    /*************************************************************************/















    /*************************************************************************/
    /*                               공격 사이클                              */
    /*************************************************************************/

    IEnumerator Chase()
    {
        Vector3 distance;
        float speed = 5f;
        float attackDelay = 3f;
        float curTime = attackDelay;

        while (targetPlayer != null && moveable != false)
        {
            tr.LookAt(targetPlayer.tr);
            distance = targetPlayer.tr.position - tr.position;
            curTime += Time.deltaTime;

            if (distance.magnitude <= 1.5f && curTime > attackDelay)
            {
                curTime = 0;
                Attack();
            }

            else if (distance.magnitude > 1.5f)
            {
                transform.position += distance.normalized * speed * Time.deltaTime;
            }

            yield return null;
        }

        chase = null;
    }








    void Attack()
    {
        if (targetPlayer == null || moveable == true)
            return;

        print($"{targetPlayer} 공격");
    }








    void Damaged(PlayerDummy _player, float _delta)
    {
        if (recover != null)
        {
            StopCoroutine(recover);
            recover = null;
        }

        if (targetPlayer == null)
        {
            targetPlayer = _player;
        }

        monsterData.curHp += _delta;

        if (!finalAttack && monsterData.maxHp * 0.4f <= monsterData.curHp && monsterData.curHp <= monsterData.maxHp * 0.6f)
        {
            FinalAttack();
        }
        else if (monsterData.curHp <= 0)
        {
            Die();
        }
    }


    public void tempSkill()
    {
        StartCoroutine(Skill());
    }

    public void tempStoneStorm()
    {
        StartCoroutine(StoneStorm());
    }

    public void tempEarthQuake()
    {
        StartCoroutine(EarthQuake());
    }


    public void tempRollStone()
    {
        StartCoroutine(RollStone());
    }


    IEnumerator Skill()
    {
        IEnumerator skill = null;
        float coolTime;
        int number;

        while (true)
        {
            moveable = false;

            number = Random.Range(0, 3);
            switch(number)
            {
                case (int)Skills.StoneStorm:
                    skill = StoneStorm();
                    break;
                case (int)Skills.EarthQuake:
                    skill = EarthQuake();
                    break;
                case (int)Skills.RollStone:
                    skill = RollStone();
                    break;
            }

            yield return StartCoroutine(skill);
            moveable = true;

            coolTime = Random.Range(8f, 15f);
            yield return StartCoroutine(WaitFor(coolTime));
        }
        
    }

    IEnumerator StoneStorm()
    {
        circle.gameObject.SetActive(true);

        float coolTime = 5f;
        float curTime = 0;
        float delayTime = 0.3f;

        int lastRock = -1;

        Vector3 pos;

        while(curTime < coolTime)
        {
            //curTime += Time.deltaTime;
            curTime += delayTime;

            for (int i = 1; i < rocks.Length; i++)
            {
                if (rocks[i].localPosition.z <= 0)
                {
                    rocks[i].GetComponent<Rigidbody>().velocity = Vector3.zero;

                    pos = Random.insideUnitCircle * 0.5f;
                    pos.z = Random.Range(0.8f, 1.0f);

                    rocks[i].localPosition = pos;

                    if (!rocks[i].gameObject.activeInHierarchy)
                    {
                        rocks[i].gameObject.SetActive(true);
                    }

                    lastRock = i;
                    break;
                }
            }

            yield return new WaitForSeconds(delayTime);
        }

        while(rocks[lastRock].localPosition.z > 0)
        {
            yield return null;
        }

        for (int i = 1; i < rocks.Length; i++)
        {
            rocks[i].gameObject.SetActive(false);
            rocks[i].position = Vector3.zero;
        }

        circle.gameObject.SetActive(false);
    }

    IEnumerator EarthQuake()
    {
        // 부채꼴 모양 범위

        yield return null;

        //square.gameObject.SetActive(true);
        //// x, y -0.5 ~ 0.5 이내

        //float coolTime = 3f;
        //float curTime = 0;

        //Vector3 pos;

        //while (curTime < coolTime)
        //{
        //    curTime += Time.deltaTime;
        //    for (int i = 1; i < stalagmites.Length; i++)
        //    {
        //        if (stalagmites[i].position.z <= 0)
        //        {
        //            pos.x = Random.Range(0f, 0.5f);
        //            pos.y = Random.Range(0f, 0.5f);
        //            pos.z = 1f;

        //            stalagmites[i].position = pos;

        //            if (!stalagmites[i].gameObject.activeInHierarchy)
        //            {
        //                stalagmites[i].gameObject.SetActive(true);
        //            }
        //        }
        //    }

        //    yield return null;
        //}


        //for (int i = 1; i < stalagmites.Length; i++)
        //{
        //    stalagmites[i].gameObject.SetActive(false);
        //    stalagmites[i].position = Vector3.zero;
        //}
    }

    IEnumerator RollStone()
    {
        print("됐니");

        square.gameObject.SetActive(true);

        print("??");

        GameObject pillar = square.GetChild(0).gameObject;
        Vector3 origin = pillar.transform.localPosition;

        pillar.transform.SetParent(tr);

        while (pillar.transform.localPosition.z < 5f)
        {
            pillar.GetComponent<Rigidbody>().AddForce(Vector3.forward * 100f, ForceMode.Impulse);
            yield return null;
        }

        square.gameObject.SetActive(false);
        pillar.transform.SetParent(square);
        pillar.transform.localPosition = origin;
    }

    void FinalAttack()
    {
        finalAttack = true;

        // 반경 내에 있는 모든 플레이어들의 Die 호출
    }




    public void Punch()
    {
        animator.SetTrigger("Punch");
        print("주먹");
        CheckTarget();
    }

    public void Roll()
    {
        if (targetPlayer != null)
        {
            targetPlayer = null;
        }
        StopCoroutine(checkTargetDistance);
        StopCoroutine(chase);
        //checkTargetDistance = chase = null;
        
        cube.transform.position = tr.position;
        cube.SetActive(true);
        StartCoroutine(coRoll());
    }

    IEnumerator coRoll()
    {
        animator.SetTrigger("Roll");
        yield return StartCoroutine(Forward());
        cube.SetActive(false);

        checkTargetDistance = CheckTargetDistance();
        chase = Chase();
        //StartCoroutine(CheckTargetDistance());
        //StartCoroutine(Chase());
        StartCoroutine(checkTargetDistance);
        StartCoroutine(chase);
    }

    IEnumerator Forward()
    {
        float time = 1.2f;
        float curTime = 0f;
        while (curTime < time)
        {
            curTime += Time.deltaTime;
            tr.position += tr.forward * 8f * Time.deltaTime;
            yield return null;
        }
    }


    public IEnumerator DotDamage(string targetName)
    {
        yield return null;

        print(targetName + " dot damaging 시작");

        float coolTime = 10.0f;
        float curTime = 0.0f;
        float interval = 0.0f;

        int totalDamage = 0;

        while (curTime <= coolTime)
        {
            curTime += Time.deltaTime;
            interval += Time.deltaTime;

            if (interval >= 1.0)
            {
                totalDamage += 5;
                print("<Dot Damage> " + targetName + " : 총 입은 데미지 " + totalDamage);
                print(curTime + ", " + interval);
                interval = 0.0f;
            }
        }
    }



    /*************************************************************************/
    /*************************************************************************/
    /*************************************************************************/















    /*************************************************************************/
    /*                                 유틸                                  */
    /*************************************************************************/

    void Die()
    {
        StopAllCoroutines();
        print($"{monsterData.name} is dead.");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 5f);
    }


    // 일단은 변수 선언해서 썼지만
    // dp 같은 방식으로 캐싱 필요
    IEnumerator WaitFor(float _time)
    {
        yield return waitThreeSeconds;
    }

    /*************************************************************************/
    /*************************************************************************/
    /*************************************************************************/
}











/* 반복적으로 호출되는 coroutine들은 update로 옮기는게 나을 수도 있을 것 같음 */