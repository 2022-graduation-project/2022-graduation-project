using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBag : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();

    private float deleteTime = 5.0f;
    private IEnumerator coroutine;
    void Start()
    {
        //Test();
        //jsonTest();

        coroutine = DestroyItemBag(deleteTime);
        //StartCoroutine(coroutine);
    }

    public void PopItem(ItemData _itemData)
    {
        foreach (ItemData item in items)
        {
            if(item.item_name.Equals(_itemData.item_name))
            {
                items.Remove(item);
                break;
            }
        }
    }

    public void AddItem(ItemData itemData)
    {
        items.Add(itemData);
    }

    public void StartDeleteCoroutine()
    {
        coroutine = DestroyItemBag(deleteTime); 
        StartCoroutine(coroutine);
    }

    public void StopDeleteCoroutine()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator DestroyItemBag(float _deleteTime)
    {
        float curTime = 0;
        while (curTime < _deleteTime)
        {
            curTime += Time.deltaTime;
            yield return null;
        }

        if(curTime >= _deleteTime)
            Destroy(gameObject);
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

    private void jsonTest()
    {
        ItemData item = DataManager.instance.LoadJsonFile
                    <Dictionary<string, ItemData>>
                    (Application.dataPath + "/MAIN/Data", "item")
                    ["000_hpPotion"];
        items.Add(item);
    }
}