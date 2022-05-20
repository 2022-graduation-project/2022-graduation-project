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
        Test();
    }



    public void AddItem(ItemData itemData)
    {
        items.Add(itemData);
    }

    private void Test()
    {
        ItemData item = new ItemData();
        item.image_name = "000_hppotion";
        item.item_name = "체력 회복 포션";
        item.count = 1;
        item.description = "체력 10을 회복합니다.";

        items.Add(item);

        ItemData item1 = new ItemData();
        item1.image_name = "001_mppotion";
        item1.item_name = "마나 회복 포션";
        item1.count = 1;
        item1.description = "마나 10을 회복합니다.";

        items.Add(item1);

        ItemData item2 = new ItemData();
        item2.image_name = "002_apple";
        item2.item_name = "사과";
        item2.count = 1;
        item2.description = "맛있음";

        items.Add(item2);

        print("리스트 길이 " + items.Count);
    }
}