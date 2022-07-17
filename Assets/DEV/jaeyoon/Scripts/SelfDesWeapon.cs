using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDesWeapon : MonoBehaviour
{
    public bool damagePlayer = false;



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            damagePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            damagePlayer = false;
        }
    }
}
