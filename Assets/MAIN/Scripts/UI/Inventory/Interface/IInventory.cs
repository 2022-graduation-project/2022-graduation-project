using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    public void SetInventory();
    public void ResetInventory();
    public void UpdateSlot(int _idx);
    public bool AddItem(string _item_code, int _item_count);
    public int RemoveItem(string _item_code, int _item_count);
    public void UseItem(string _item_code);
    public void SwapItem(InventorySlot from, InventorySlot to);
    public bool SellItem(string _item_code, int _item_count, int _price);
    public bool BuyItem(string _item_code, int _item_count, int _price);
}
