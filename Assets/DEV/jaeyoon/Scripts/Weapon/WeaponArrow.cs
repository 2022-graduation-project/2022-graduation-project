using UnityEngine;
using System.Collections;

public class WeaponArrow : Weapon
{
    private int _damage;

    void OnCollisionEnter(Collision collision)  // 충돌 로직
    {
        if (collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject, 3); // 탄피가 바닥에 닿으면 3초 뒤 사라짐
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    /*
    void Attack()   // 근거리냐 원거리냐
    {
        animator.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot");
    }
    */

    public Transform bulletPos;
    public GameObject bullet;
    public Transform bulletCasePos;
    public GameObject bulletCase;

    /*
    public void Use()   // 거리에 따라 공격 선택
    {
        if (type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        else if (type == Type.Range)
        {
            StartCoroutine("Shot");
        }
    }
    */

    public IEnumerator Shot()
    {
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);  // 탄피 생
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        // bulletRigid.velocity = bulletPos.forward * 50;  // 탄속 50

        yield return null;

        // 탄피 배출
        GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = intantBullet.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);

        // 탄피에 랜덤한 힘
        /*
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
        */
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
