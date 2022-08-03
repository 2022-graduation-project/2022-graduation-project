using System.Collections;
using UnityEngine;

public class TestBossRolling : MonoBehaviour
{
    private TestBoss testboss;  // 나중에 보스몬스터 오브젝트로 바뀌겠
    private IEnumerator dotDamage;


    void Awake()
    {
        testboss = transform.parent.GetComponent<TestBoss>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("응 너 밟힘; " + other.transform.name);
            dotDamage = testboss.DotDamage(other.transform.name);
            StartCoroutine(dotDamage);
        }
    }
}
