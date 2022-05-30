using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public bool isOpening = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float targetPosY = 0.325f;
    private float targetRotX = -20.181f;

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isOpening)
        {
            return;
        }
        else
        {
            if(transform.position.y > targetPosY)
            {

                Vector3 targetPos = new Vector3(transform.position.x, transform.position.y - 0.00005f, transform.position.z);
                transform.Translate(targetPos);

                //transform.Translate(Vector3.up * -0.00005f);
            }

            if(transform.rotation.x > targetRotX)
            {
                Vector3 targetRot = new Vector3(targetRotX, transform.rotation.y, transform.rotation.z);
                // 타겟 방향으로 회전함
                transform.LookAt(targetRot);
                //transform.LookAt(Vector3.Lerp(transform.position, targetRot, Time.deltaTime));
            }

        }
    }
}
