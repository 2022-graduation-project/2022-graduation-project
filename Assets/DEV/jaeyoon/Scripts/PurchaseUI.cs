using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // ����Ƽ UI ����ϱ� ���� ����

public class PurchaseUI : MonoBehaviour
{
    string item = "";
    int price;

    // string str_price = Item1.ItemInfo.GetComponent(UI.text);


    void Start()
    {

    }

    void Update()
    {
        // ���� ���콺 ��ư Ŭ������ ��
        if (Input.GetMouseButtonDown(0))
        {
            print("Clicked " + item);
        }
    }
}
