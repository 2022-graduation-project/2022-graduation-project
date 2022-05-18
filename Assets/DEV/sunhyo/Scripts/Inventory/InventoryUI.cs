using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    /*************************************************************/
    /* 임시로 싱글톤 선언했으나, 아이템 획득 시 json 쓰기 및 읽기 후 */
    /*       인벤토리 여는 경우 json 읽어 set 하는 작업 필요        */
    /*************************************************************/
    // public static InventoryUI instance;
    /*************************************************************/
    //void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else if (instance != this)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}


    public GameObject[] inventorySlots;

    public void Set()
    {
        // inventory 데이터 json에 저장 및 쓰기 필요
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