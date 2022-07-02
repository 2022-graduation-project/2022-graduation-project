using System.Collections;
using UnityEngine;


public class Archer : PlayerController
{
    public GameObject ArrowFactory;    // 화살 생산할 공장
    public Transform firePosition; // 출발 좌표


    public override void UseSkill()
    {
        //PlayerManager.keyMoveable = false;
        //PlayerManager.mouseMoveable = false;

        animator.SetTrigger("doShot");

        GameObject arrow = Instantiate(ArrowFactory, firePosition.position, firePosition.rotation); // 화살 생성


        Destroy(arrow, 5);

        //StartCoroutine(Shot());
    }
}