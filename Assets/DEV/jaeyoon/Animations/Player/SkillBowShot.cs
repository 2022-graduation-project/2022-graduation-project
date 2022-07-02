using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBowShot : MonoBehaviour
{
    public Animator animator;

    private void delayShooting()    // 속도 딜레이 (스킬)
    {
        animator.speed = 0.5f;
    }

    private void recoverSpeed()    // 속도 원상복귀
    {
        animator.speed = 1.0f;
    }
}
