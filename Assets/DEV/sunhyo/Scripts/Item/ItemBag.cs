using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBag : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();



    /***************/
    /*    TEST     */
    /***************/
    void Start()
    {
        //ItemData item = new ItemData();
        //item.itemKey = "000_test";
        //item.itemValue.item_name = "TEST";
        //item.itemValue.count = 5;
        //item.itemValue.description = "테스트입니다.";

        ItemData item = new ItemData();
        item.image_name = "image";
        item.item_name = "TEST";
        item.count = 5;
        item.description = "테스트입니다.";

        items.Add(item);
    }



    public void AddItem(ItemData itemData)
    {
        items.Add(itemData);
    }
}