using System.Collections;
using UnityEngine;

public class ChestData : ItemData
{
    //// prefab �̸�
    //public string image_name;
    //// UI ��� �̸�
    //public string item_name;
    //// ����
    //public int count;
    //// ����
    //public string description;
    // ������
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
