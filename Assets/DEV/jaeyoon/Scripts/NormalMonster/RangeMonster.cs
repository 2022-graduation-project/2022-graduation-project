using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMonster : NormalMonster
{
    /* (Range Monster) PRIVATE DATA - 공격 */
    private RangeWeapon prefab_arrow;
    private List<RangeWeapon> arrowPool = new List<RangeWeapon>();  // 오브젝트 풀
    private readonly int arrowMaxCount = 3; // 총 화살 개수
    private int currentIndex = 0; // 현재 장전된, 발사할 화살의 인덱스
    private int destroyingIndex;  // 삭제할 (비활성화시킬) 화살의 인덱스

    /* (Range Monster) PRIVATE DATA - 타이머 */
    private float timer;
    private float coolTime;




    protected override void Awake()
    {
        base.Awake();

        /* Range Monster ; 사용할 화살 프리펩 지정 */
        prefab_arrow = GameObject.Find("Arrow").GetComponent<RangeWeapon>();

        /* Range Monster ; 공격 시 발동할 애니메이션의 트리거 명 */
        attackTrigger = "BowShot";

        /* 공격 범위 & 공격 지연 시간 */
        attackRange = 3.75f;
        attackDelay = 1.5f;

        /* 타이머 초기 설정 */
        timer = coolTime = attackDelay;
    }

    
    private void Start()
    {
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

    protected override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);

        /* 쿨타임이 지났다면 == 공격 가능 상태가 되었다면 */
        if (timer >= coolTime)
        {
            animator.SetTrigger(attackTrigger);

        
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
            StartCoroutine("Destroy", destroyingIndex);

            // 방금 마지막 화살을 발사했다면 다시 첫 화살부터 장전
            if (currentIndex < arrowMaxCount - 1)
                currentIndex++;
            else
                currentIndex = 0;


            timer = 0;  // Reset Timer
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