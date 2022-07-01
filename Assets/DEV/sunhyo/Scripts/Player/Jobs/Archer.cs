using System.Collections;
using UnityEngine;


public class Archer : PlayerController
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FarAttack()
    {
        animator.SetTrigger("doShot");
    }

    public override void UseSkill()
    {
        
    }
}