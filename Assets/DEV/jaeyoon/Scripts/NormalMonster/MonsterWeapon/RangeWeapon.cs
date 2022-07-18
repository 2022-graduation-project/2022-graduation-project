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
        // Set direction & Shot
        transform.Translate(new Vector3(0, 1.0f, 0) * speed * Time.deltaTime);
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
