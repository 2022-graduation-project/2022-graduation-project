using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBag : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();



    /***************/
    /*    TEST     */
    /***************/
    //void Start()
    //{
    //    ItemData item = new ItemData();
    //    item.item_number = 99;
    //    item.item_name = "TEST";
    //    item.count = 5;

    //    items.Add(item);
    //}



    public void AddItem(ItemData itemData)
    {
        items.Add(itemData);
    }
}