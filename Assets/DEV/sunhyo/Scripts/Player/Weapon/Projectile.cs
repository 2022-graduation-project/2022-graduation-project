using System.Collections;
using UnityEngine;

public class Projectile : Weapon
{
    private Rigidbody body;
    private SphereCollider sphereCollider;

    private float damage;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {

        }
    }
}