using System.Collections;
using UnityEngine;

public class MonsterDummy : MonsterController
{
    private float hp = 100f;

    void Start()
    {
        
    }

    public new void Damage(float scale)
    {
        hp -= scale;

        print("Dummy Damaged. current Hp is " + hp);

        if (hp <= 0)
            Die();
    }

    public new void Die()
    {
        print("Die");
        gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other) { }

    private void OnTriggerEnter(Collider other) { }
}