using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBag : MonoBehaviour
{
    public List<ChestData> chests = new List<ChestData>();

    private float deleteTime = 5.0f;
    private IEnumerator coroutine;
    void Start()
    {
        //Test();

        coroutine = DestroyChestBag(deleteTime);
        //StartCoroutine(coroutine);
    }

    public void PopChest(ChestData _chestData)
    {
        foreach (ChestData chest in chests)
        {
            if(chest.item_name.Equals(_chestData.item_name))
            {
                //print(chest.item_name);
                chests.Remove(chest);
                break;
            }
        }
    }

    public void AddChest(ChestData chestData)
    {
        chests.Add(chestData);
    }

    public void StartDeleteCoroutine()
    {
        coroutine = DestroyChestBag(deleteTime); 
        StartCoroutine(coroutine);
    }

    public void StopDeleteCoroutine()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator DestroyChestBag(float _deleteTime)
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
        ChestData item = new ChestData();
        item.image_name = "000_hppotion";
        item.item_name = "체력 회복 포션";
        item.count = 1;
        item.description = "체력 10을 회복합니다.";

        chests.Add(item);

        ChestData item1 = new ChestData();
        item1.image_name = "001_mppotion";
        item1.item_name = "마나 회복 포션";
        item1.count = 1;
        item1.description = "마나 10을 회복합니다.";

        chests.Add(item1);

        ChestData item2 = new ChestData();
        item2.image_name = "002_apple";
        item2.item_name = "사과";
        item2.count = 1;
        item2.description = "맛있음";

        chests.Add(item2);

        print("리스트 길이 " + chests.Count);
    }
}