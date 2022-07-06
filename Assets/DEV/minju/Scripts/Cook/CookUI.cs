using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CookUI : MonoBehaviour
{
    public GameObject[] inventorySlots;

    [SerializeField] private PlayerData playerData;

    [SerializeField] private Transform Item;


    public Transform inventory;
    void Awake()
    {
        inventory = transform.GetChild(0);

        
    }


    public void AddItem(ItemData _itemData)
    {
        
        {
            GameObject emptySlot = GetEmptySlotInPool();
            // print(emptySlot);
            emptySlot?.GetComponent<exInventorySlot>().Set(_itemData);
        }
    }

    GameObject FindSameItem(ItemData _itemData)
    {
        foreach (GameObject itemSlot in inventorySlots)
        {
            if (itemSlot.GetComponent<exInventorySlot>().itemData != null &&
                itemSlot.GetComponent<exInventorySlot>().itemData.item_name.Equals(_itemData.item_name))
                return itemSlot;
        }

        return null;
    }

    GameObject GetEmptySlotInPool()
    {
        foreach (GameObject itemSlot in inventorySlots)
        {
            if (itemSlot.GetComponent<exInventorySlot>().itemData == null)
                return itemSlot;
        }

        return null;
    }
}