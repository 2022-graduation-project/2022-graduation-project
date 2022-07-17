using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private List<InventorySlot> inventorySlots;
    private Text money;

    public GameObject go;

    void Start()
    {
        inventorySlots = new List<InventorySlot>(GetComponentsInChildren<InventorySlot>());
        foreach (InventorySlot slot in inventorySlots)
        {
            slot.SetInventoryScript(this);
        }

        money = transform.Find("Money").GetComponent<Text>();
        money.text = PlayerManager.instance.playerData.money.ToString("N0") + "원";

        go.AddComponent<ItemDummy>();
    }

    public void AddItem(ItemData _itemData)
    {
        InventorySlot existSlot = FindSameItem(_itemData);

        if (existSlot != null)
        {
            existSlot.Set();
        }
        else
        {
            InventorySlot emptySlot = GetEmptySlotInPool();
            emptySlot?.Set(_itemData, go.GetComponent<ItemDummy>());
        }
    }

    public bool ReduceItem(ItemData _itemData)
    {
        // 감소시킬 수량 팝업
        int _count = 1;

        return UIManager.instance.ReduceItem(_itemData, _count);
    }

    public bool DeleteItem(ItemData _itemData)
    {
        return UIManager.instance.DeleteItem(_itemData);
    }

    public bool UseItem(ItemData _itemData, int _count=1)
    {
        return UIManager.instance.UseItem(_itemData);
    }

    public void Match()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            slot.Match();
        }
    }

    void GetMoney(int delta)
    {
        int curMoney = PlayerManager.instance.playerData.money += delta;
        money.text = curMoney.ToString("N0") + "원";
    }

    public void SetMoney()
    {
        money.text = PlayerManager.instance.playerData.money.ToString("N0") + "원";
    }

    InventorySlot FindSameItem(ItemData _itemData)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.itemData != null && slot.itemData.item_name.Equals(_itemData.item_name))
            {
                return slot;
            }
        }

        return null;
    }

    InventorySlot GetEmptySlotInPool()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.itemData == null)
                return slot;
        }

        return null;
    }
}