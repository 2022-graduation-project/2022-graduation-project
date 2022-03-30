using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // 유니티 UI 사용하기 위한 선언

public class PurchaseUI : MonoBehaviour
{
    public Text MoneyTxt;
    public Button item1, item2, item3;
    public int money;

    void Start()
    {
        MoneyTxt.text = "Player's money : " + money.ToString();

        item1.onClick.AddListener(() => BuyItem(5000));
        item2.onClick.AddListener(() => BuyItem(7000));
        item3.onClick.AddListener(delegate { BuyItem(10000); });
    }

    public void BuyItem(int price)
    {
        if (price <= money)
        {
            money -= price;
            MoneyTxt.text = "Player's money : " + money.ToString();
        }
        else
        {
            Debug.Log("잔액이 부족합니다.");
        }
    }
}
