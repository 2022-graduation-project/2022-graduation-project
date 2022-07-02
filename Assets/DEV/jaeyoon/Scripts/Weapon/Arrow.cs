using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 5; // 이동 속도


    void Update()
    {
        Vector3 dir = Vector3.forward;   // 1. Set Direction
        transform.position += dir * speed * Time.deltaTime; // 2. Shot
    }
}
