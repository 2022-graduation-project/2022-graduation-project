using System.Collections;
using UnityEngine;

public class ChestData
{
    // prefab �̸�
    public string image_name;
    // UI ��� �̸�
    public string item_name;
    // ����
    public int count;
    // ����
    public string description;
    // ������
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
