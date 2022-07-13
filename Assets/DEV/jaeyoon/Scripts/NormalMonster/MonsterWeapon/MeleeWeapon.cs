using System.Collections;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
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