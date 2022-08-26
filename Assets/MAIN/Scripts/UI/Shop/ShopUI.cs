using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ShopUI : MonoBehaviour, IDropHandler, IUIWindow
{
    private List<ShopItemSlot> slots;
    private List<string> shopList;

    [SerializeField] private Text money;
    [SerializeField] private GameObject countPanel;

    public GameObject content;
    public GameObject ShopItemSlot;

    void Start()
    {
        shopList = DataManager.instance.shopItemList;

        for (int i=0; i<shopList.Count; i++)
        {
            Instantiate(ShopItemSlot, content.transform);
        }

        slots = new List<ShopItemSlot>(content.GetComponentsInChildren<ShopItemSlot>());

        for (int i = 0; i < shopList.Count; i++)
        {
            slots[i].Set(shopList[i], this);
        }

        UpdateMoney();
    }

    public void SetWindow()
    {
        gameObject.SetActive(true);
        money.text = DataManager.instance.playerData.money.ToString("N0") + "원";
        CheckActiveSlot(DataManager.instance.playerData.money);
    }

    public void Buy(string _item_code)
    {
        int count = 1;
        int curMoney;
        int price;
        // 수량 선택 창 생성

        curMoney = DataManager.instance.playerData.money;
        money.text = curMoney.ToString("N0") + "원";
        price = DataManager.instance.itemDict[_item_code].price;

        if (curMoney >= price)
        {
            InventoryWindow.instance.BuyItem(_item_code, count, price);
        }

        UpdateMoney();
        CheckActiveSlot(DataManager.instance.playerData.money);
    }

    public void Sell(string _item_code)
    {
        int count = 1;
        int price = 0;
        // 수량 선택 창 생성

        print(_item_code);
        price = DataManager.instance.itemDict[_item_code].price;
        InventoryWindow.instance.SellItem(_item_code, count, (int)(price * 0.8f));
        UpdateMoney();
        CheckActiveSlot(DataManager.instance.playerData.money);
    }

    void CheckActiveSlot(int _money)
    {
        foreach (ShopItemSlot slot in slots)
        {
            if (slot.item_code == null)
                continue;

            if (slot.price > _money)
            {
                slot.SetActiveSlot(false);
            }
            else
            {
                slot.SetActiveSlot(true);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.item_code == "")
            return;

        Sell(DragSlot.instance.item_code);
        DragSlot.instance.ResetSlot();
    }

    public void UpdateMoney()
    {
        money.text = DataManager.instance.playerData.money.ToString("N0") + "원";
    }
}