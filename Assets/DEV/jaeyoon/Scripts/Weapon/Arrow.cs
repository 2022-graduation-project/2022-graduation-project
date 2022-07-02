using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{
    public float speed = 5; // 이동 속도


    void Update()
    {
        Vector3 dir = Vector3.forward;   // 1. Set Direction
        transform.Translate(dir * speed * Time.deltaTime);  // 2. Shot
    }

    /*
    public override void Attack(float _damage)
    {
        base.Attack(_damage);
    }
    */



    /*

    public enum Type { Melee, Range };
    public Type type;
    //public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;


    public void Use()
    {
        if (type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
    }

    IEnumerator Swing()
    {
        // 1
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        // 2
        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        // 3
        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }
    // Use() 메인루틴 -> Swing() 서브루틴 -> Use() 메인루틴
    // Use() 메인루틴 + Swing() 코루틴 (Co-Op)

    */

}
