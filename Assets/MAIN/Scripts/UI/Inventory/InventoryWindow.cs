using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWindow : MonoBehaviour, IInventory
{
    private const int ERROR = -1;
    private const int MAX = 999;

    private List<InventoryData> inventory;
    private List<InventorySlot> slots;
    private int inventory_count;

    public Transform slot_grid;
    public bool isActive = false;

    public static InventoryWindow instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        inventory = DataManager.instance.inventory;
        slots = new List<InventorySlot>(slot_grid.GetComponentsInChildren<InventorySlot>());

        for (int i = inventory.Count; i < slots.Count; i++)
        {
            inventory.Add(new InventoryData());
        }
    }

    public void OpenWindow()
    {
        isActive = true;
        gameObject.SetActive(true);
        SetInventory();
    }

    public void CloseWindow()
    {
        isActive = false;
        gameObject.SetActive(false);
    }

    public void SetInventory()
    {
        /* 인벤토리 열 때마다 실행 */
        for(int i = 0; i < inventory.Count; i++)
        {
            slots[i].SetSlot(inventory[i].item_code, inventory[i].item_count);
        }
    }

    public void ResetInventory()
    {
        foreach(InventorySlot slot in slots)
        {
            slot.ResetSlot();
        }
    }

    public bool AddItem(string _item_code, int _item_count)
    {
        // 1. 인벤토리에 동일한 아이템이 있다면 개수만 증가
        // 2. 동일한 아이템이 없는 경우
        //      3. 비어있는 슬롯이 있는 경우 해당 위치에 추가
        //      4. 인벤토리가 가득 찬 경우 -1 return

        int idx;
        
        if ((idx = inventory.FindIndex(x => x.item_code == _item_code)) != ERROR)
        {
            inventory[idx].item_count += _item_count;
        }
        else
        {
            if(inventory_count < inventory.Count)
            {
                idx = inventory.FindIndex(x => x.item_code == "");

                inventory[idx].item_code = _item_code;
                inventory[idx].item_count = _item_count;

                UpdateSlot(idx);
            }
            else
            {
                return false;
            }
        }
        
        return true;
    }

    public bool BuyItem(string _item_code, int _item_count)
    {
        // 아이템을 추가할 수 있다면 true 반환 => Shop에서 소지금 차감
        if(AddItem(_item_code, _item_count))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int RemoveItem(string _item_code, int _item_count = MAX)
    {
        // 해당하는 아이템을 찾아 null로 변경 (list 크기 및 아이템 위치 유지)

        int idx;

        if ((idx = inventory.FindIndex(x => x.item_code == _item_code)) != ERROR)
        {
            if (_item_count == MAX)
            {
                _item_count = inventory[idx].item_count;
            }

            if ((inventory[idx].item_count -= _item_count) <= 0)
            {
                inventory[idx].item_code = "";
                inventory[idx].item_count = 0;
            }

            UpdateSlot(idx);

            return idx;
        }
        else
        {
            return ERROR;
        }
    }

    public bool SellItem(string _item_code, int _item_count)
    {
        // 가지고 있는 아이템인 경우 판매
        if (RemoveItem(_item_code, _item_count) != ERROR)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SwapItem(InventorySlot from, InventorySlot to)
    {
        int fidx = slots.FindIndex(x => x == from);
        int tidx = slots.FindIndex(x => x == to);

        InventoryData tid = inventory[fidx];
        inventory[fidx] = inventory[tidx];
        inventory[tidx] = tid;

        string t_item_code = from.item_code;
        int t_item_count = from.item_count;

        from.SetSlot(to.item_code, to.item_count);
        to.SetSlot(t_item_code, t_item_count);

        DragSlot.instance.ResetSlot();
    }

    public void UpdateSlot(int _idx)
    {
        slots[_idx].SetSlot(inventory[_idx].item_code, inventory[_idx].item_count);
    }

    public void UseItem(string _item_code)
    {
        int idx;

        if((idx = inventory.FindIndex(x => x.item_code == _item_code)) != ERROR)
        {
            if (slots[idx].item_script != null)
            {
                slots[idx].item_script.Use();
                RemoveItem(_item_code, 1);
            }
        }
    }
}
