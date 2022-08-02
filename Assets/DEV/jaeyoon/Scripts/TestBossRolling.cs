using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossRolling : MonoBehaviour
{
    private Collider rollingCollider;
    private TestBoss testboss;
    private IEnumerator dotting;


    void Awake()
    {
        testboss = transform.parent.GetComponent<TestBoss>();
        rollingCollider = GetComponent<Collider>();
    }


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("응 너 밟힘; " + other.transform.name);
            dotting = testboss.dotDamage(other.transform.name);
            StartCoroutine(dotting);
        }
    }
}
