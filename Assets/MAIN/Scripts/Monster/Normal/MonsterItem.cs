using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterItem : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            Debug.Log(other.transform.gameObject.name + " : <Item> " + transform.name + " 획득!");
            transform.gameObject.SetActive(false);
        }
    }
}
