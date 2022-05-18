using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject[] inventorySlots;

    void Start()
    {
        ItemData item = new ItemData();
        item.image_name = "000_hpPotion";
        item.item_name = "TEST";
        item.count = 1;
        item.description = "테스트입니다.";
        AddItem(item);
    }
    public void Set()
    {

    }

    public void AddItem(ItemData _itemData)
    {
        GameObject existSlot = FindSameItem(_itemData);
        if (existSlot != null)
            existSlot.GetComponent<InventorySlot>().Set(_itemData);

        else
        {
            GameObject emptySlot = GetEmptySlotInPool();
            print(emptySlot);
            emptySlot?.GetComponent<InventorySlot>().Set(_itemData);
        }
    }

    GameObject FindSameItem(ItemData _itemData)
    {
        foreach (GameObject itemSlot in inventorySlots)
        {
            if (itemSlot.GetComponent<InventorySlot>().itemData != null)
            {
                itemSlot.GetComponent<InventorySlot>().itemData.item_name.Equals(_itemData.item_name);
                return itemSlot;
            }
        }

        return null;
    }

    GameObject GetEmptySlotInPool()
    {
        foreach (GameObject itemSlot in inventorySlots)
        {
            if (itemSlot.GetComponent<InventorySlot>().itemData == null)
                return itemSlot;
        }

        return null;
    }
}