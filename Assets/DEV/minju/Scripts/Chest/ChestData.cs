using System.Collections;
using UnityEngine;

public class ChestData : ItemData
{
    //// prefab 이름
    //public string image_name;
    //// UI 띄울 이름
    //public string item_name;
    //// 개수
    //public int count;
    //// 설명
    //public string description;
    // 포만감
    public int saturation;

    public ChestData DeepCopy()
    {
        ChestData newChestData = new ChestData();
        newChestData.image_name = image_name;
        newChestData.item_name = item_name;
        newChestData.count = count;
        newChestData.description = description;
        newChestData.saturation = saturation;

        return newChestData;
    }
}
