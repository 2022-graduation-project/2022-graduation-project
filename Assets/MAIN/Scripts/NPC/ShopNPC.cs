using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public GameObject shop;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            shop.SetActive(true);
        }
    }
}
