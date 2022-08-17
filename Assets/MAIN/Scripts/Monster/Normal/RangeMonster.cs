using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMonster : NormalMonster
{
    public override void SetMonsterData()
    {
        monsterData = DataManager.instance.LoadJsonFile
              <Dictionary<string, MonsterData>>
              (Application.dataPath + "/MAIN/Data", "monster")
              ["003_goblin"];
    }


    /* (Range Monster) PRIVATE DATA - 공격 */
    public RangeWeapon prefab_arrow;
    private List<RangeWeapon> arrowPool = new List<RangeWeapon>();  // 오브젝트 풀
    private readonly int arrowMaxCount = 3; // 총 화살 개수
    private int currentIndex = 0; // 현재 장전된, 발사할 화살의 인덱스
    private int destroyingIndex;  // 삭제할 (비활성화시킬) 화살의 인덱스

    /* (Range Monster) PRIVATE DATA - 타이머 */
    private float timer;
    private float coolTime;



    public override void Set()
    {
        SetMonsterData();

        /* Protected Variables */
        attackDistance = 4f;
        attackCool = 2.0f;



        /* 타이머 초기 설정 */
        timer = 0.0f;
        coolTime = 20f;

        /* 초기 세팅 - 사용할 화살 미리 생성 */
        for (int i = 0; i < arrowMaxCount; i++)
        {
            RangeWeapon arrow = Instantiate<RangeWeapon>(prefab_arrow);

            // 발사하기 전까지는 비활성화
            arrow.gameObject.SetActive(false);
            // 오브젝트 풀에 추가
            arrowPool.Add(arrow);
        }
    }



    private void Update()
    {
        // 추적 범위 내에서 플레이어 발견!
        if (target != null)
        {
            isFound = true;
            distance = Vector3.Distance(transform.position, target.position);   // 현재 몬스터-플레이어 사이 거리 측정

            // 공격 범위보다 더 멀리 떨어져 있는 경우 -> 추적 계속
            if (distance >/*monsterData.attackDistance*/1.5f)
            {
                animator.SetBool("Walk", true);
                Chase(/*monsterData.moveSpeed*/1.5f);
            }

            // 공격 범위 진입 -> 추적 중지, 공격 시작
            else
            {
                animator.SetBool("Walk", false);
                StartCoroutine(coAttack());
            }
        }

        else
        {
            animator.SetBool("Walk", false);
            isFound = false;
        }


        timer += Time.deltaTime;
    }




    private IEnumerator coAttack()
    {
        yield return null;

        timer = 0;  // Reset Timer



        /* 쿨타임이 지났다면 == 공격 가능 상태가 되었다면 */
        if (timer >= coolTime)
        {
            animator.SetTrigger("RangeAttack");


            // 발사되어야 할 순번의 화살이 아직도 사용 중이라, 발사 불가
            if (arrowPool[currentIndex].gameObject.activeSelf)
            {
                yield break;
            }

            // 화살의 출발지와 방향(지금 몬스터가 바라보는 방향) 설정
            arrowPool[currentIndex].transform.position = prefab_arrow.transform.position;
            arrowPool[currentIndex].transform.rotation = this.transform.rotation * Quaternion.Euler(90f, 0f, 0f);

            // 화살 활성화, 발사
            arrowPool[currentIndex].gameObject.SetActive(true);

            // 현재 발사된 화살 -> 5초 지나면 자동 제거
            destroyingIndex = currentIndex;
            StartCoroutine(Destroy(destroyingIndex));

            // 방금 마지막 화살을 발사했다면 다시 첫 화살부터 장전
            if (currentIndex < arrowMaxCount - 1)
                currentIndex++;
            else
                currentIndex = 0;
        }
    }


    /*------------------------------------------------------
     *              DESTROY - 사용 끝난 화살 자동 비활성화
     * ----------------------------------------------------*/

    private IEnumerator Destroy(int index)
    {
        yield return new WaitForSeconds(5);
        arrowPool[index].gameObject.SetActive(false);
    }


}