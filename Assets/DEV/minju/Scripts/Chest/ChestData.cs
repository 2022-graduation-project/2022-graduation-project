using System.Collections;
using UnityEngine;

public class ChestData
{
    // prefab 이름
    public string image_name;
    // UI 띄울 이름
    public string item_name;
    // 개수
    public int count;
    // 설명
    public string description;
    // 포만감
    public int saturation;

    public ChestData DeepCopy()
    {
        ChestData newItemData = new ChestData();
        newItemData.image_name = image_name;
        newItemData.item_name = item_name;
        newItemData.count = count;
        newItemData.description = description;
        newItemData.saturation = saturation;

        return newItemData;
    }
}
