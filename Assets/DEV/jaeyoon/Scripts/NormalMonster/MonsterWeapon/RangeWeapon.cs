using System.Collections;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    protected MeshCollider meshCollider;
    protected float damage;

    public float speed = 2; // 이동 속도


    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
    }

    void Update()
    {
        Vector3 dir = Vector3.forward;   // 1. Set Direction
        transform.Translate(dir * speed * Time.deltaTime);  // 2. Shot
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player damaged");
            gameObject.SetActive(false);
        }
    }
}
