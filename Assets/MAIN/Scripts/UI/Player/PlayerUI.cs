using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image hp;
    public Image mp;

    public Transform skills;
    public Transform items;

    private InventorySlot[] item_slots;

    private void Start()
    {
        Set(DataManager.instance.playerData);
    }

    public void Set(PlayerData _playerData)
    {
        hp.fillAmount = _playerData.curHp / _playerData.maxHp;
        mp.fillAmount = _playerData.curMp / _playerData.maxMp;

        item_slots = items.GetComponentsInChildren<InventorySlot>();
    }

    public void UpdateHpBar(float _maxHp, float _curHp)
    {
        print("체력 변경" + (_curHp / _maxHp));
        hp.fillAmount = _curHp / _maxHp;
    }

    public void UpdateMpBar(float _maxMp, float _curMp)
    {
        mp.fillAmount = _curMp / _maxMp;
    }

    public void ItemQuickSlot(int _type)
    {
        item_slots[_type].GetComponent<QuickSlotDragable>().UseItem();
    }

    public bool FindItemQuickSlot(string _item_code)
    {
        foreach (var slot in item_slots)
        {
            if(slot.item_code == _item_code)
            {
                return true;
            }
        }

        return false;
    }
}