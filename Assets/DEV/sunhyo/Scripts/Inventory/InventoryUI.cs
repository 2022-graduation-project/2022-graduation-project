using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private List<ItemData> itemDatas = new List<ItemData>();
    private List<InventorySlot> inventorySlots;

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

    void Start()
    {
        inventorySlots = new List<InventorySlot>(GetComponentsInChildren<InventorySlot>());

        ItemData temp = new ItemData();
        temp.image_name = "001_mpPotion";
        temp.item_name = "마나 회복 포션";
        temp.count = 2;
        temp.description = "마나 10을 회복합니다.";
        temp.fullness = 0;

        itemDatas.Add(temp);

        //itemDatas.Find(x => x == temp).count += temp.count;

        //foreach (ItemData item in itemDatas)
        //    print(item.count);

        inventorySlots[0].Set(itemDatas[0]);
    }

    public void AddItem(ItemData _itemData)
    {
        InventorySlot existSlot = FindSameItem(_itemData);

        if (existSlot != null)
        {
            itemDatas.Find(x => x.item_name == _itemData.item_name).count += _itemData.count;
            existSlot.Set(_itemData);
        }
        else
        {
            itemDatas.Add(_itemData);
            InventorySlot emptySlot = GetEmptySlotInPool();
            emptySlot?.Set(_itemData);
        }
    }

    public bool DeleteItem(ItemData _itemData)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.itemData.item_name == _itemData.item_name)
            {
                slot.itemData = null;
                return true;
            }
        }

        return false;
    }

    public bool UseItem(ItemData _itemData)
    {
        return false;
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