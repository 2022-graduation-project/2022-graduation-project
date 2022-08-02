using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Archer : PlayerController
{
    public Arrow prefab_arrow;
    private List<Arrow> arrowPool = new List<Arrow>();
    private readonly int arrowMaxCount = 3; // 총 화살 개수
    private int currentIndex = 0; // 현재 장전된 화살의 인덱스
    private int destroyingIndex;  // 삭제할 (비활성화시킬) 화살의 인덱스


    private void Awake()
    {
        prefab_arrow = GameObject.Find("Arrow").GetComponent<Arrow>();
    }

    private void Start()
    {
        // 스킬용 화살 3개 미리 생성
        for (int i = 0; i < arrowMaxCount; i++)
        {
            Arrow arrow = Instantiate<Arrow>(prefab_arrow);

            // 발사하기 전까지는 비활성화
            arrow.gameObject.SetActive(false);
            // 오브젝트 풀에 추가
            arrowPool.Add(arrow);
        }
    }


    public override void UseSkill()
    {
        animator.SetTrigger("UseSkill");
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        // playerManager.keyMoveable = false;
        // playerManager.mouseMoveable = false;
        // playerManager.keyMoveable = true;
        // playerManager.mouseMoveable = true;



        if (Input.GetMouseButtonDown(0))
        {
        animator.SetTrigger("Attack");

        // 발사되어야 할 순번의 화살이 아직도 날아가고 있는 중이라면, 발사를 못 하게 한다
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
        }

    }

    private IEnumerator Destroy(int index)
    {
        yield return new WaitForSeconds(5);
        arrowPool[index].gameObject.SetActive(false);
    }
}

/*
private IEnumerator Shot()
    {
        playerManager.keyMoveable = false;
        playerManager.mouseMoveable = false;

        yield return new WaitForSeconds(2.0f);
        ArrowFactory.SetActive(true);

        GameObject arrow = Instantiate(ArrowFactory, firePosition.position, firePosition.rotation); // 화살 생성
        playerManager.keyMoveable = true;
        playerManager.mouseMoveable = true;

        Destroy(arrow, 5);
        ArrowFactory.SetActive(false);
    }
}
*/