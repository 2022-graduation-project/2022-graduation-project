using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // ����Ƽ UI ����ϱ� ���� ����

public class PurchaseUI : MonoBehaviour
{
    public Text MoneyTxt;
    public Button item1, item2, item3;
    public int money;

    void Start()
    {
        MoneyTxt.text = "Player's money : " + money.ToString();

        item1.onClick.AddListener(() => buyItem(5000));
        item2.onClick.AddListener(() => buyItem(7000));
        item3.onClick.AddListener(delegate { buyItem(10000); });
    }

    void buyItem(int price)
    {
        if (price <= money)
        {
            money -= price;
            MoneyTxt.text = "Player's money : " + money.ToString();
        }
        else
        {
            Debug.Log("�ܾ��� �����մϴ�.");
        }
    }

    //void Update()
    //{
    //    MoneyTxt.text = "Player's money : " + money.ToString();
    //}
}
