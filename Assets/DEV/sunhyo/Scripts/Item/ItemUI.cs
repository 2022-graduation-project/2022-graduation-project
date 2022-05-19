using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemUI : MonoBehaviour
{
    public GameObject[] itemBagSlots;

    public void Set(ItemBag _itemBag)
    {
        foreach (ItemData item in _itemBag.items)
        {
            GameObject itemBagSlot = GetItemSlotInPool();

            itemBagSlot?.GetComponent<ItemBagSlot>().Set(item);
        }

        gameObject.SetActive(true);
    }

    public void Reset()
    {
        ItemData item = null;
        foreach (GameObject itemBagSlot in itemBagSlots)
        {
            if (itemBagSlot.activeSelf)
                itemBagSlot.GetComponent<ItemBagSlot>().Reset();
        }

        gameObject.SetActive(false);
    }

    GameObject GetItemSlotInPool()
    {
        foreach (GameObject itemBagSlot in itemBagSlots)
        {
            if (!itemBagSlot.activeSelf)
                return itemBagSlot;
        }

        return null;
    }

    void Test(ItemData item)
    {
        print(item);
        print(item.image_name);
        print(item.item_name);
        print(item.count);
        print(item.description);
    }
}