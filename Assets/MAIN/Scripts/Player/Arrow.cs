<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private NormalMonster monster;    // 데미지 받는 몬스터 (일단 하나)
    private float speed = 5; // 이동 속도
    private float damage = 5f;


    private MeshCollider meshCollider;

    private void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
    }
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private NormalMonster monster;    // 데미지 받는 몬스터 (일단 하나)
    private float speed = 5; // 이동 속도
    private float damage = 300f;
>>>>>>> 6ba036443e52ad52e4b8c32c79e3e199379f3fd4



    void Update()
    {
        transform.Translate(new Vector3(0, 0, 1.0f) * speed * Time.deltaTime);  // Set direction & Shot
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
