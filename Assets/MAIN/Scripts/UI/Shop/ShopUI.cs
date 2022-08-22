using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ShopUI : MonoBehaviour, IDropHandler, IUIWindow
{
    private List<ShopItemSlot> slots;
    private List<string> itemDatas;

    [SerializeField] private Text money;
    [SerializeField] private GameObject countPanel;

    public GameObject content;

    public void SetWindow()
    {
        slots = new List<ShopItemSlot>(content.GetComponentsInChildren<ShopItemSlot>());
        itemDatas = DataManager.instance.LoadJsonFile<List<string>>
                    (Application.dataPath + "/MAIN/Data", "shop");

        money.text = PlayerManager.instance.playerData.money.ToString("N0") + "원";

        CheckActiveSlot(PlayerManager.instance.playerData.money);
    }

    public void Buy(ItemData _itemData)
    {
        ItemData newItemData = _itemData.DeepCopy();
        int count = 1;
        int curMoney;
        // 수량 선택 창 생성

        InventoryUI.instance.GetMoney(-newItemData.price * count);
        curMoney = PlayerManager.instance.playerData.money;
        money.text = curMoney.ToString("N0") + "원";
        InventoryUI.instance.AddItem(newItemData, count);
        CheckActiveSlot(curMoney);
    }

    public void Sell(string _item_code)
    {
        print("판매");

        int count = 1;
        int curMoney;
        // 수량 선택 창 생성

        //if(InventoryUI.instance.UseItem(_itemData, count))
        //{
        //    InventoryUI.instance.GetMoney((int)(_itemData.price * 0.7f) * count);
        //    curMoney = PlayerManager.instance.playerData.money;
        //    money.text = curMoney.ToString("N0") + "원";
        //    CheckActiveSlot(curMoney);
        //}
    }

    void CheckActiveSlot(int _money)
    {
        foreach(ShopItemSlot slot in slots)
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
        if (DragSlot.instance.item_code == null)
            return;

        Sell(DragSlot.instance.item_code);
        if(DragSlot.instance.item_count == 0)
            DragSlot.instance.Reset();
    }
}