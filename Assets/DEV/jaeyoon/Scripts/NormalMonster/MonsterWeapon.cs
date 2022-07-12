using System.Collections;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour
{
    protected MeshCollider meshCollider;
    protected float damage;

    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player damaged");
        }
    }
}