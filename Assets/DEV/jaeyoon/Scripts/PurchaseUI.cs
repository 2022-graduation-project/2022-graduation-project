using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // 유니티 UI 사용하기 위한 선언

public class PurchaseUI : MonoBehaviour
{
    public Text playerMoney;
    int money;

    void Start()
    {
        money = 10000;
    }

    void Update()
    {
        playerMoney.text = money.ToString();
        Debug.Log(playerMoney);
        Debug.Log(playerMoney.GetType());
    }
    /*
    public void ClickItem()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        print(clickObject.name + ", " + clickObject.GetComponentInChildren<Text>().text);
    }
    */
}
