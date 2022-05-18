using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemUI : MonoBehaviour
{
    public GameObject[] itemBagSlots;

    public void Set(ItemBag _itemBag)
    {
        GameObject itemBagSlot = GetItemSlotInPool();
        foreach (ItemData item in _itemBag.items)
        {
            Test(item);

            itemBagSlot?.GetComponent<ItemBagSlot>().Set(item);
            itemBagSlot?.SetActive(true);
        }

        gameObject.SetActive(true);
    }

    public void Reset()
    {
        ItemData item = null;
        foreach (GameObject itemBagSlot in itemBagSlots)
        {
            if (itemBagSlot.activeSelf)
            {
                itemBagSlot.GetComponent<ItemBagSlot>().Reset();
                itemBagSlot.SetActive(false);
            }
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
        //print(item);
        //print(item.itemKey);
        //print(item.itemValue);
        //print(item.itemValue.item_name);
        //print(item.itemValue.count);
        //print(item.itemValue.description);

        print(item);
        print(item.item_name);
        print(item.count);
        print(item.description);
    }
}