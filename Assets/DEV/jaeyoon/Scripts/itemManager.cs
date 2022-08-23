using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemManager : MonoBehaviour
{
    private GameObject item;


    public int SelectItemIndex()
    {
        System.Random random = new System.Random();
        int n = random.Next(10);    // Item Set -> 10개의 종류 있다고 설정하고 랜덤으로 인덱스 설정

        Debug.Log(n+1);
        return n;
    }

    public void DropItem(NormalMonster _monster, int index)
    {
        item = GameObject.Find("ItemPool").transform.GetChild(index).gameObject;
        //item = GameObject.Find("ItemPool").transform.GetChild(0).gameObject;
        item.transform.position = _monster.transform.position + new Vector3(0, 1.1f, 0);  // 아이템 떨굴 위치 (죽은 자리 + 1) 설정

        item.SetActive(true);
    }
}
