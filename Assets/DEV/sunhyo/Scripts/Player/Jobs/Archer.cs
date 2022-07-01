using System.Collections;
using UnityEngine;


public class Archer : PlayerController
{
    public override void UseSkill()
    {
        animator.SetTrigger("doShot");
        //StartCoroutine(Shot());
    }
}