using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // 유니티 UI 사용하기 위한 선언

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
        // 왼쪽 마우스 버튼 클릭했을 때
        if (Input.GetMouseButtonDown(0))
        {
            print("Clicked " + item);
        }
    }
}
