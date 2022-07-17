using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ListTest : MonoBehaviour
{
    List<ItemData> list = new List<ItemData>();

    void Start()
    {
        print("hi");

        for(int i = 0; i < 10; i++)
        {
            if(i % 3 != 0)
            {
                ItemData item = new ItemData();
                item.count = i;
                list.Add(item);
            }
                
            else
                list.Add(null);
        }


        for (int j = 0; j < list.Count; j++)
        {
            print(list[j]);
            if (list[j] != null)
                print(list[j].count);
        }
            
    }
}