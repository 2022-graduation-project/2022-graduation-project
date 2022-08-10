using System.Collections;
using UnityEngine;

public class TestBossRolling : MonoBehaviour
{
    private BossController boss;
    private IEnumerator dotDamage;


    void Awake()
    {
        boss = transform.parent.GetComponent<BossController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("응 너 밟힘; " + other.transform.name);
            dotDamage = boss.DotDamage(other.transform.name);
            StartCoroutine(dotDamage);
        }
    }
}
