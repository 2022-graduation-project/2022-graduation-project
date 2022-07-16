using System.Collections;
using UnityEngine;

public class ItemData
{
    public string image_name;
    public string item_name;
    public int count;
    public string description;
    public int fullness;
    public int price;

    public ItemData DeepCopy()
    {
        ItemData newItemData = new ItemData();
        newItemData.image_name = image_name;
        newItemData.item_name = item_name;
        newItemData.count = count;
        newItemData.description = description;
        newItemData.fullness = fullness;
        newItemData.price = price;

        return newItemData;
    }
}