using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : MonoBehaviour, IInventory
{
    private const int ERROR = -1;
    private const int MAX = 999;

    private List<InventoryData> inventory;
    private List<InventorySlot> slots;
    private int inventory_count;

    public Transform slot_grid;
    public Text money_txt;
    public bool isActive = false;

    public static InventoryWindow instance;

    private bool isSet = false;
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
    }

    void MyStart()
    {
        inventory = DataManager.instance.inventory;
        slots = new List<InventorySlot>(slot_grid.GetComponentsInChildren<InventorySlot>());

        for (int i = inventory.Count; i < slots.Count; i++)
        {
            inventory.Add(new InventoryData());
        }

        gameObject.SetActive(isActive);
        UpdateMoney();
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
        if(!isSet)
        {
            MyStart();
            isSet = true;
        }

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
            UpdateSlot(idx);
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

    public bool BuyItem(string _item_code, int _item_count, int _price)
    {
        // 아이템을 추가할 수 있다면 true 반환 => Shop에서 소지금 차감
        if(AddItem(_item_code, _item_count))
        {
            DataManager.instance.playerData.money -= _price;
            UpdateMoney();
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

    public bool SellItem(string _item_code, int _item_count, int _price)
    {
        // 가지고 있는 아이템인 경우 판매
        if (RemoveItem(_item_code, _item_count) != ERROR)
        {
            DataManager.instance.playerData.money += _price;
            UpdateMoney();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SwapItem(InventorySlot _from, InventorySlot _to)
    {
        int fidx = slots.FindIndex(x => x == _from);
        int tidx = slots.FindIndex(x => x == _to);

        InventoryData tid = inventory[fidx];
        inventory[fidx] = inventory[tidx];
        inventory[tidx] = tid;

        string t_item_code = _from.item_code;
        int t_item_count = _from.item_count;

        _from.SetSlot(_to.item_code, _to.item_count);
        _to.SetSlot(t_item_code, t_item_count);

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

    public InventorySlot FindItemSlot(string _item_code)
    {
        int idx = inventory.FindIndex(x => x.item_code == _item_code);

        if(idx != -1)
        {
            return slots[idx];
        }
        else
        {
            return null;
        }
        
    }

    public void UpdateMoney()
    {
        money_txt.text = DataManager.instance.playerData.money.ToString("N0") + "원";
    }
}
