using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public bool isOpening = false;
    public bool isClosing = false;
    private Transform[] comps;
    private Transform lid;
    private Vector3 eulerLid;

    // Start is called before the first frame update
    void Start()
    {
        comps = gameObject.GetComponentsInChildren<Transform>();
        lid = comps[1];
    }

    private float targetPosY = 0.325f;
    private float targetRotX = 340f;

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isOpening)
        {
            if (!isClosing)
            {
                return;
            }
            else
            {
                Closing();
            }
            return;
        }
        else
        {
            Opening();
        }
    }

    private void Opening()
    {
        eulerLid = lid.rotation.eulerAngles;
        if (eulerLid.x >= targetRotX || (eulerLid.x >= 0 && eulerLid.x <= 10))
        //if (eulerLid.x >= targetRotX || eulerLid.x == 0)
        {
            Vector3 targetRot = new Vector3(-2f, 0, 0);
            //Vector3 targetRot = new Vector3(targetRotX, 0, 0);

            // 타겟 방향으로 회전함
            lid.Rotate(targetRot);
            //lid.LookAt(targetRot);
            //lid.Rotate(Vector3.Lerp(lid.rotation.eulerAngles, targetRot, 0.0001f * Time.deltaTime));
        }
        {/*
            else
            {
                if (lid.position.y > targetPosY)
                {
                    Vector3 targetPos = new Vector3(lid.position.x, lid.position.y - 0.00005f, lid.position.z);
                    lid.Translate(targetPos);

                    //transform.Translate(Vector3.up * -0.00005f);
                }
            }
            */
        }

    }

    private void Closing()
    {
        eulerLid = lid.rotation.eulerAngles;
        if (eulerLid.x <= 360 && eulerLid.x >= 300)
        //if (eulerLid.x <= 360 && eulerLid.x > 0)
        {
            Vector3 targetRot = new Vector3(2f, 0, 0);
            //Vector3 targetRot = new Vector3(targetRotX, 0, 0);

            // 타겟 방향으로 회전함
            lid.Rotate(targetRot);
            //lid.LookAt(targetRot);
            //lid.Rotate(Vector3.Lerp(lid.rotation.eulerAngles, targetRot, 0.0001f * Time.deltaTime));
        }
    }
}
