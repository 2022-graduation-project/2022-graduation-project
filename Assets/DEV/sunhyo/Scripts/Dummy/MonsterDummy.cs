using System.Collections;
using UnityEngine;

public class MonsterDummy : MonoBehaviour
{
    private float hp = 100f;

    public void Damage(float scale)
    {
        hp -= scale;

        print("Dummy Damaged. current Hp is " + hp);

        if (hp <= 0)
            Die();
    }

    public void Die()
    {
        print("Die");
        gameObject.SetActive(false);
    }
}