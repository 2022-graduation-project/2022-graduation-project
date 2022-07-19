using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private List<ItemData> itemDatas = new List<ItemData>();
    private List<InventorySlot> inventorySlots;
    private Text money;

    public static InventoryUI instance = null;

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

    void Start()
    {
        inventorySlots = new List<InventorySlot>(GetComponentsInChildren<InventorySlot>());

        // 이제... 인벤토리 최대 슬롯 개수 만큼 오브젝트 풀 만들고
        // 플레이어 데이터에서 아이템 불러와서 슬롯에 뿌리기
        // 음... 플레이어가 인벤토리 내용물을 바꾼 내용대로 저장이 되려면
        // Drag & Drop을 인벤토리 슬롯에서 하는게 아니라 인벤토리 UI에서 해야되겠구나!


        // 아무튼 동시에 돈도 설정을 해야하고
        // BuyItem, SellItem 할 때마다 돈 상태를 업데이트 해줘야 함.

        //money = transform.Find("Money").GetComponent<Text>();
        //money.text = DataManager.instance.LoadJsonFile
        //             <Dictionary<string, PlayerData>>
        //             (Application.dataPath + "/MAIN/Data", "player")
        //             ["000_player"].money.ToString();


        money = transform.Find("Money").GetComponent<Text>();
        money.text = PlayerManager.instance.playerData.money.ToString("N0") + "원";



        /***************************************************************************/
        /******************************     Test     *******************************/
        /***************************************************************************/
        ItemData temp = new ItemData();
        temp.image_name = "000_hpPotion";
        temp.item_name = "체력 회복 포션";
        temp.count = 1;
        temp.description = "체력 10을 회복합니다.";
        temp.price = 10000;

        itemDatas.Add(temp);

        temp = new ItemData();
        temp.image_name = "001_mpPotion";
        temp.item_name = "마나 회복 포션";
        temp.count = 5;
        temp.description = "마나 10을 회복합니다.";
        temp.price = 20000;

        itemDatas.Add(temp);

        temp = new ItemData();
        temp.image_name = "006_bomb";
        temp.item_name = "폭탄";
        temp.count = 5;
        temp.description = "폭탄을 던져 몬스터에게 데미지를 입힙니다.";
        temp.price = 20000;

        itemDatas.Add(temp);

        temp = new ItemData();
        temp.image_name = "005_shieldPotion";
        temp.item_name = "방어포션";
        temp.count = 5;
        temp.description = "일정 시간 동안 몬스터에게 데미지를 입지 않습니다.";
        temp.price = 20000;

        itemDatas.Add(temp);

        inventorySlots[0].Set(itemDatas[0]);
        inventorySlots[1].Set(itemDatas[1]);
        inventorySlots[2].Set(itemDatas[2]);
        inventorySlots[3].Set(itemDatas[3]);
        /***************************************************************************/
        /***************************************************************************/
        /***************************************************************************/
    }

    public void AddItem(ItemData _itemData, int _count=1)
    {
        // PlayerData 업데이트 필요
        // 그리고 여기에... ItemDummy 스크립트가 안들어가있네


        InventorySlot existSlot = FindSameItem(_itemData);

        if (existSlot != null)
        {
            itemDatas.Find(x => x.item_name == _itemData.item_name).count += _count;
            existSlot.Set(_itemData);
        }
        else
        {
            print("없는 아이템");
            itemDatas.Add(_itemData);
            InventorySlot emptySlot = GetEmptySlotInPool();
            emptySlot?.Set(_itemData);
        }
    }

    public bool DeleteItem(ItemData _itemData)
    {
        // PlayerData 업데이트 필요

        itemDatas.Remove(_itemData);

        //foreach (InventorySlot slot in inventorySlots)
        //{
        //    if (slot.itemData.item_name == _itemData.item_name)
        //    {
        //        slot.itemData = null;
        //        return true;
        //    }
        //}

        foreach (ItemData item in itemDatas)
        {
            print($"{item.item_name} is in slots");
        }

        return true;
    }

    public bool UseItem(ItemData _itemData, int _count=1)
    {
        if(_itemData.count < 1)
            return false;

        _itemData.count -= _count;

        InventorySlot tempInventorySlot = FindSameItem(_itemData);
        if (_itemData.count <= 0)
        {
            tempInventorySlot.Reset();
            DeleteItem(_itemData);
        }
        else
        {
            tempInventorySlot.SetCountObj();
        }

        return true;
    }

    public void GetMoney(int delta)
    {
        int curMoney = PlayerManager.instance.playerData.money += delta;
        money.text = curMoney.ToString("N0") + "원";
    }

    InventorySlot FindSameItem(ItemData _itemData)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.itemData != null && slot.itemData.item_name.Equals(_itemData.item_name))
            {
                return slot;
            }
        }

        return null;
    }

    InventorySlot GetEmptySlotInPool()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.itemData == null)
                return slot;
        }

        return null;
    }
}