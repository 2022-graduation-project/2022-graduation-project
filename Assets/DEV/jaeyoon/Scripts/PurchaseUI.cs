using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // ����Ƽ UI ����ϱ� ���� ����

public class PurchaseUI : MonoBehaviour
{
    private PlayerData player;

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
            Debug.Log("�ܾ��� �����մϴ�.");
        }
    }
}
