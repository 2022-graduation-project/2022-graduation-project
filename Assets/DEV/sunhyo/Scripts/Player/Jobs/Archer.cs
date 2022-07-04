using System.Collections;
using UnityEngine;


public class Archer : PlayerController
{
    public GameObject ArrowFactory;    // 화살 생산할 공장
    public Transform firePosition; // 출발 좌표



    public override void UseSkill()
    {
        animator.SetTrigger("UseSkill");
        StartCoroutine(Shot());
    }

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