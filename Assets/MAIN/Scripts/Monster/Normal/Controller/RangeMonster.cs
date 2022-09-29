using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMonster : NormalMonster
{
    /*----------------------------------------------------------------
     *              [Range Monster] LOCAL DATA
     * --------------------------------------------------------------*/

    public RangeWeapon prefab_arrow;
    private List<RangeWeapon> arrowPool = new List<RangeWeapon>();  // 오브젝트 풀
    private readonly int arrowMaxCount = 5; // 총 화살 개수
    private int currentIndex = 0; // 현재 장전된, 발사할 화살의 인덱스
    private int destroyingIndex;  // 삭제할 (비활성화시킬) 화살의 인덱스

    private float timer = 0.0f;    // 타이머


    /*----------------------------------------------------------------
     *              Inherited Methods (SetMonsterData, Set)
     * --------------------------------------------------------------*/

    public override void SetMonsterData()
    {
        monsterData = DataManager.instance.LoadJsonFile
              <Dictionary<string, MonsterData>>
              (Application.dataPath + "/MAIN/Data", "monster")
              ["003_goblin"];
        monsterData.curHp = monsterData.maxHp;
    }

    public override void Set()
    {
        base.Set();

        SetMonsterData();

        /* Protected Variables */
        attackDistance = 4.0f;
        attackCool = 3.0f;

        /* (Range Monster) 사용할 화살 미리 생성 */
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



    /*----------------------------------------------------------------
     *              [Range Monster] 몬스터 별 공격 코루틴
     * --------------------------------------------------------------*/

    public override IEnumerator coAttack()
    {
        yield return new WaitForSeconds(attackCool);


        if (timer < attackCool)
            yield break;

        else
        {
            animator.SetTrigger("RangeAttack");
            /*
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) // 애니메이션 완전히 끝나야 화살 발사
                StartCoroutine(Shot(currentIndex));
            */
            StartCoroutine(Shot(currentIndex));

            // 방금 마지막 화살을 발사했다면 다시 첫 화살부터 장전
            if (currentIndex < arrowMaxCount - 1)
                currentIndex++;
            else
                currentIndex = 0;

            timer = 0;
        }
    }

    private IEnumerator Shot(int index) // 화살 발사
    {
        yield return null;

        // 발사되어야 할 순번의 화살이 아직도 사용 중이라, 발사 불가
        if (arrowPool[index].gameObject.activeSelf)
            yield break;

        // 화살의 출발지와 방향(지금 몬스터가 바라보는 방향) 설정
        arrowPool[index].transform.position = prefab_arrow.transform.position;
        arrowPool[index].transform.rotation = this.transform.rotation * Quaternion.Euler(90f, 0f, 0f);

        // 화살 활성화, 발사
        arrowPool[index].gameObject.SetActive(true);

        // 현재 발사된 화살 -> 5초 지나면 자동 제거
        destroyingIndex = currentIndex;
        if (arrowPool[index].gameObject.activeSelf)
        {
            destroyingIndex = currentIndex;
            StartCoroutine(Destroy(destroyingIndex));
        }
    }

    private IEnumerator Destroy(int index)  // 사용 끝난 화살 자동 비활성화
    {
        yield return new WaitForSeconds(5);
        arrowPool[index].gameObject.SetActive(false);
    }
}