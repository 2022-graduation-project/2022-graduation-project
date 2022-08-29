using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{
    private NormalMonster monster;    // 데미지 받는 몬스터 (일단 하나)
    private float speed = 5; // 이동 속도




    /* Protected Variable */
    private void Awake()
    {
        damage = 50f;
    }

    void Update()
    {
        transform.Translate(new Vector3(0, 0, 1.0f) * speed * Time.deltaTime);  // Set direction & Shot
    }


    public override void Attack(float _damage, PlayerController _pc = null)
    {
        /*
        // attack effects
        Instantiate(slashEffect, transform.position + new Vector3(0, 0, 0.3f), Quaternion.identity);
        slashSound.Play();

        attackable = true;
        damage = _damage;
        pc = _pc;
        */
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            monster = other.transform.GetComponent<NormalMonster>();
            // 임시
            monster.dDamaged(damage);
            gameObject.SetActive(false);
        }
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
