using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject[] inventorySlots;
    public void Set()
    {

    }

    public void AddItem(ItemData _itemData)
    {
        GameObject existSlot = FindSameItem(_itemData);

        if (existSlot != null)
            existSlot.GetComponent<InventorySlot>().itemData.count++;

        else
        {
            GameObject emptySlot = GetEmptySlotInPool();

            if (emptySlot != null)
            {
                emptySlot.GetComponent<InventorySlot>().itemData = _itemData;
                SetColorA(emptySlot);
            }
        }
    }

    GameObject FindSameItem(ItemData _itemData)
    {
        foreach (GameObject itemSlot in inventorySlots)
        {
            if (itemSlot.GetComponent<InventorySlot>().itemData.item_name.Equals(_itemData.item_name))
                return itemSlot;
        }

        return null;
    }

    GameObject GetEmptySlotInPool()
    {
        foreach (GameObject itemSlot in inventorySlots)
        {
            if (!itemSlot.activeSelf)
                return itemSlot;
        }

        return null;
    }

    void SetColorA(GameObject _inventotySlot)
    {
        Color color = _inventotySlot.transform.Find("Icon").GetComponent<Image>().color;
        color.a = 1f;
        _inventotySlot.transform.Find("Icon").GetComponent<Image>().color = color;
    }
}