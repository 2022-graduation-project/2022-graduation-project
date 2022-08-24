using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : MonoBehaviour
{
    /* 데이터 */
    private MonsterData monsterData;


    /* 컴포넌트 */
    private Transform tr;
    private Animator animator;


    /* 공격 지역 */
    private GameObject circle;
    private GameObject square;
    private GameObject cube;    // Rolling Collider


    /* 런타임 변수 */
    private Stack<GameObject> targetPlayers;
    private PlayerController targetPlayer;

    private bool finalAttack = false;
    private float maxDistance = 5f;

    private IEnumerator recover;
    private IEnumerator checkTargetDistance;
    private IEnumerator chase;

    private WaitForSeconds waitOneSecond = new WaitForSeconds(1.0f);

    void Awake()
    {
        print("어웨이크");
    }

    void Start()
    {
        print("실행");

        monsterData = DataManager.instance.LoadJsonFile
                      <Dictionary<string, MonsterData>>
                      (Application.dataPath + "/MAIN/Data", "boss")
                      ["000_golem"];

        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();

        circle = tr.Find("Circle").gameObject;
        square = tr.Find("Square").gameObject;
        cube = tr.Find("CubeCol").gameObject;

        CheckTarget();
    }





    /*************************************************************************/
    /*                         targetPlayer 사이클                           */
    /*************************************************************************/




    void CheckTarget()
    {

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
                    targetPlayer = hit.transform.GetComponent<PlayerController>();
                }
            }

            print($"가장 가까운 오브젝트(거리, 이름) : {minDistance}, {targetPlayer?.transform.name}");
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
        while (targetPlayer != null)
        {
            if (maxDistance < Vector3.Distance(targetPlayer.transform.position, transform.position))
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

        while (targetPlayer != null)
        {
            tr.LookAt(targetPlayer.transform);
            distance = targetPlayer.transform.position - tr.position;
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
        if (targetPlayer == null)
            return;

        print($"{targetPlayer} 공격");
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








    void Damaged(PlayerController _player, float _delta)
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







    void Skill()
    {

    }

    void StoneStorm()
    {

    }

    void EarthQuake()
    {

    }

    void RollStone()
    {

    }
    void FinalAttack()
    {
        finalAttack = true;

        // 반경 내에 있는 모든 플레이어들의 Die 호출
    }




    public void Punch()
    {
        if (targetPlayer != null)
        {
            //StopCoroutine(checkTargetDistance);
            targetPlayer = null;
        }

        animator.SetTrigger("Punch");
        print("주먹");
        CheckTarget();
    }

    public void Roll()
    {
        cube.transform.position = tr.position;
        cube.SetActive(true);
        StartCoroutine(coRoll());
    }

    IEnumerator coRoll()
    {
        if (targetPlayer != null)
        {
            //StopCoroutine(checkTargetDistance);
            targetPlayer = null;
        }

        animator.SetTrigger("Roll");
        yield return StartCoroutine(Forward());
        CheckTarget();
        cube.SetActive(false);
    }

    IEnumerator Forward()
    {
        float time = 1.2f;
        float curTime = 0f;
        while (curTime < time)
        {
            curTime += Time.deltaTime;
            tr.position += tr.forward * 6f * Time.deltaTime;
            yield return null;
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

    /*************************************************************************/
    /*************************************************************************/
    /*************************************************************************/
}