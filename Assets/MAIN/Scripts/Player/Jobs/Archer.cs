using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Archer : PlayerController
{
    public Arrow prefab_arrow;
    private List<Arrow> arrowPool = new List<Arrow>();
    private readonly int arrowMaxCount = 3; // 총 화살 개수
    private int currentIndex; // 현재 장전된 화살의 인덱스



    public override void Start()
    {
        base.Start();

        prefab_arrow = GameObject.Find("Quiver").transform.GetChild(0).GetComponent<Arrow>();
        currentIndex = 0;
        // bow = GameObject.Find("Wooden Bow").gameObject.GetComponent<Bow>();

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

    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0))
            animator.SetTrigger("Attack");
        if (Input.GetKeyDown(KeyCode.G))
            UseSkill();
    }


    private void UseSkill()
    {
        animator.SetTrigger("BowShot");
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        // playerManager.keyMoveable = false;
        // playerManager.mouseMoveable = false;
        // playerManager.keyMoveable = true;
        // playerManager.mouseMoveable = true;


        // 발사되어야 할 순번의 화살이 아직도 날아가고 있는 중이라면, 발사를 못 하게 한다
        if (arrowPool[currentIndex].gameObject.activeSelf)
        {
            yield break;
        }


        yield return new WaitForSeconds(0.2f);


        // 화살의 출발지와 방향(지금 플레이어가 바라보는 방향) 설정
        arrowPool[currentIndex].transform.position = prefab_arrow.transform.position;
        arrowPool[currentIndex].transform.rotation = prefab_arrow.transform.rotation;

        // 화살 활성화, 발사
        arrowPool[currentIndex].gameObject.SetActive(true);

        // 현재 발사된 화살 -> 5초 지나면 자동 제거되도록 설정
        StartCoroutine("Destroy", currentIndex);

        // 방금 마지막 화살을 발사했다면 다시 첫 화살부터 장전
        if (currentIndex < arrowMaxCount - 1)
            currentIndex++;
        else
            currentIndex = 0;
    }

    private IEnumerator Destroy(int index)
    {
        yield return new WaitForSeconds(5);
        arrowPool[index].gameObject.SetActive(false);
    }
}