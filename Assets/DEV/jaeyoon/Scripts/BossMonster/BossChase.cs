using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossChase : MonoBehaviour
{
    public Transform bossTarget = null; // 추적할 대상의 좌표
    public bool findTarget = false;



    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            findTarget = true;
            bossTarget = other.transform;
            Debug.Log("Boss : Target found");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        findTarget = false;
        bossTarget = null;
        Debug.Log("Boss : Target lost");
    }



}
