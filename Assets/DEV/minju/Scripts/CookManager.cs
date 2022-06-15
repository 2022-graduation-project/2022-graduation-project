using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject cookUI;
    public GameObject inventory;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // ���� UI ȭ�� ����
            cookUI.SetActive(true);
            // �κ��丮 ����
            inventory.SetActive(true);
            // UI �����ϱ�


        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // ���� UI ȭ�� �������
            cookUI.SetActive(false);
            // �κ��丮 �������
            inventory.SetActive(false);


        }
    }
}
