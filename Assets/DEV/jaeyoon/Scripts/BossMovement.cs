using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 0.5f;

    void Start()
    {

    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        
        Vector3 direction = new Vector3(h, 0, v);
        /*
        // 이동했을 때
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // 뒤를 바라볼 때는 이동만
            if (Mathf.Abs(angle) < 180)
            {
                angle = angle * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, angle);
            }
        }
        */
        transform.position += direction * speed * Time.deltaTime;
    }
}
