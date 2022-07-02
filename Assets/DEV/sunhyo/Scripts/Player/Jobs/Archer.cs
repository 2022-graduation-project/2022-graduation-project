using System.Collections;
using UnityEngine;


public class Archer : PlayerController
{
    public GameObject ArrowFactory;    // 화살 생산할 공장
    public Transform firePosition; // 출발 좌표



    public override void UseSkill()
    {
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        animator.SetTrigger("UseSkill");

        yield return new WaitForSeconds(0.5f);

        playerManager.keyMoveable = false;
        playerManager.mouseMoveable = false;
        ArrowFactory.SetActive(true);

        GameObject arrow = Instantiate(ArrowFactory, firePosition.position, firePosition.rotation); // 화살 생성
        Destroy(arrow, 5);

        playerManager.keyMoveable = true;
        playerManager.mouseMoveable = true;
        ArrowFactory.SetActive(false);
    }
}