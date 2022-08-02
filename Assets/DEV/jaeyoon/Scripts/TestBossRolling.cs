using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossRolling : MonoBehaviour
{
    private Collider rollingCollider;



    void Start()
    {
        rollingCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            print("응 너 밟힘; " + other.transform.name);
    }
}
