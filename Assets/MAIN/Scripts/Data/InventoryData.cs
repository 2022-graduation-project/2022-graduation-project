using System.Collections;
using UnityEngine;


public class InventoryData
{
    public string item_code;
    public int item_count;

    public InventoryData(string _item_code = "", int _item_count = 0)
    {
        item_code = _item_code;
        item_count = _item_count;
    }
}