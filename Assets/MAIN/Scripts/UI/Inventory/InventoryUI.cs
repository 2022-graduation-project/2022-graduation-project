using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IUIWindow
{
    public class InventoryItem
    {
        public string item_code;
        public int item_count;

        public InventoryItem(string _item_code, int _item_count)
        {
            item_code = _item_code;
            item_count = _item_count;
        }
    }

    private List<InventoryItem> inventory;
    private List<InventorySlot> slots;

    public Transform contents;
    public GameObject slot;
    public Text money_txt;

    public static InventoryUI instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void SetWindow()
    {
        for (int i = 0; i < 56; i++)
        {
            Instantiate(slot, contents);
        }

        slots = new List<InventorySlot>(GetComponentsInChildren<InventorySlot>());
        money_txt.text = DataManager.instance.playerData.money.ToString("N0") + "원";
    }

    public void AddItem(string _item_code, int _count=1)
    {
        InventorySlot existSlot = FindSameItem(_item_code);

        if (existSlot != null)
        {
            inventory.Find(x => x.item_code == _item_code).item_count += _count;
            existSlot.Set(_item_code);
        }
        else
        {
            print("없는 아이템");
            inventory.Add(new InventoryItem(_item_code, _count));
            InventorySlot emptySlot = GetEmptySlotInPool();
            emptySlot?.Set(_item_code);
        }
    }

    public void DeleteItem(string _item_code)
    {
        inventory.Remove(inventory.Find(x => x.item_code == _item_code));
    }

    public void UseItem(string _item_code, int _count=1)
    {
        InventoryItem temp_item = inventory.Find(x => x.item_code == _item_code);
        if(temp_item == null || temp_item.item_count < 1)
            return;

        temp_item.item_count -= _count;
        //if()

        InventorySlot tempInventorySlot = FindSameItem(_item_code);
        if (temp_item.item_count <= 0)
        {
            tempInventorySlot.Reset();
            DeleteItem(_item_code);
        }
        else
        {
            tempInventorySlot.SetCountObj();
        }
    }

    public void GetMoney(int delta)
    {
        int curMoney = DataManager.instance.playerData.money += delta;
        money_txt.text = curMoney.ToString("N0") + "원";
    }

    InventorySlot FindSameItem(string _item_code)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item_code != null && slot.item_code.Equals(_item_code))
            {
                return slot;
            }
        }

        return null;
    }

    InventorySlot GetEmptySlotInPool()
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item_code == null)
                return slot;
        }

        return null;
    }
}