using System.Collections;
using UnityEngine;

public class RangeWeapon : MonsterWeapon
{
    private float arrowSpeed = 3.5f; // 화살 이동 속도


    private void Update()
    {
        /* Set direction & Move arrow */
        transform.Translate(new Vector3(0, 1.0f, 0) * arrowSpeed * Time.deltaTime);
    }

    /* 몬스터(Range) 무기에 플레이어 닿음 */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player damaged");
            gameObject.SetActive(false);    // 그 화살 즉시 비활성화
        }
    }
}
