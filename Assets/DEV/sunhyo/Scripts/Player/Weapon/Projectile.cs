using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject player;

    private Rigidbody body;
    private SphereCollider sphereCollider;

    private float damage;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();

        body.AddForce(player.transform.forward * 100.0f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            other.GetComponent<MonsterController>().Damage(damage);
        }
    }
}