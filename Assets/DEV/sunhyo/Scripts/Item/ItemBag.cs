using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBag : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();

    void Start()
    {
        Test();
        StartCoroutine("DestroyItemBag");
    }

    public bool EmptyCheck()
    {
        if (items.Count == 0)
        {
            Delete();
            return true;
        }
        return false;
    }

    public bool complete = false;

    public void PopItem(ItemData _itemData)
    {
        foreach (ItemData item in items)
        {
            if(item.item_name.Equals(_itemData.item_name))
            {
                print(item.item_name);
                items.Remove(item);
                break;
            }
        }
    }
    private void Delete()
    {
        Destroy(gameObject, 3.0f);
        complete = true;
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

    IEnumerator DestroyItemBag()
    {
        float deleteTime = 3.0f;
        float curTime = 0;
        while (curTime < deleteTime)
        {
            curTime += Time.deltaTime;
            print(curTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}