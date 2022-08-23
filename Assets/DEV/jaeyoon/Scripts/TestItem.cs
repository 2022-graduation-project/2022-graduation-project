using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.name + " : <Item> " + transform.name + " 획득!");
            transform.gameObject.SetActive(false);
        }
    }
}
