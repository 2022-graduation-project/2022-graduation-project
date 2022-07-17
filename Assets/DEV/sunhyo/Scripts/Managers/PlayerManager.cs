using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class PlayerManager : MonoBehaviour
{
    /* Data */
    public PlayerData playerData;
    public Dictionary<string, ItemData> inventory;

    /* Input */
    public bool keyMoveable = true;
    public bool mouseMoveable = true;


    /* Singleton */
    public static PlayerManager instance;

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

        playerData = DataManager.instance.LoadJsonFile
                    <Dictionary<string, PlayerData>>
                    (Application.dataPath + "/MAIN/Data", "player")
                    ["000_player"];

        inventory = playerData.inventory;
    }

    /************************************************/
    /*                   상태 이상                   */
    /************************************************/
    public IEnumerator Healing(float healAmount)
    {

        instance.playerData.curHp += healAmount;
        UIManager.instance.playerUI.UpdateHpBar(instance.playerData.maxHp, instance.playerData.curHp);
        yield break;
    }
    public IEnumerator RefillMana(float manaAmount)
    {

        instance.playerData.curHp += manaAmount;
        UIManager.instance.playerUI.UpdateMpBar(instance.playerData.maxMp, instance.playerData.curMp);
        yield break;
    }
    public IEnumerator Shielding(float shieldDuration)
    {
        //float duration = 0;
        //while (duration < shieldDuration)
        //{
        //    canDamage=true;
        //    yield return new WaitForSeconds(1f);
        //    duration++;
        //    if (duration >= shieldDuration)
        //    {
        //        canDamage=false;
        //        yield break;
        //    }
        //}
        yield break;
    }





    /**************************************************/
    /****************  아이템 관련 작업  ***************/
    /**************************************************/


    // 얘는 조건만 체크... 그리고 데이터 변경만...




    // 추가하고 싶은 아이템과 개수 전달
    // 아이템 추가 성공했을 시 true 반환
    public ItemData AddItem(ItemData _itemData, int _count=1)
    {
        string key = _itemData.image_name;

        // 소지 가능한 최대 개수를 넘는 경우 return

        if (inventory.ContainsKey(key))
        {
            inventory[key].count += _count;
        }
        else
        {
            ItemData newItemData = _itemData.DeepCopy();
            newItemData.count = _count;

            inventory.Add(key, newItemData);
        }

        return inventory[key];
    }



    // 줄이고 싶은 아이템과 개수 전달
    // 줄이기 성공 시 true 반환
    // 만약 개수가 0 이하라면 inventory에서 삭제
    public bool ReduceItem(ItemData _itemData, int _count = 1)
    {
        string key = _itemData.image_name;

        // 판매 불가능한 아이템인 경우도 추가
        if (!inventory.ContainsKey(key))
        {
            print("아이템 존재 ㄴㄴ");
            return false;
        }
        
        inventory[key].count -= _count;

        if (inventory[key].count <= 0)
        {
            inventory.Remove(key);
        }

        return true;
    }

    public bool DeleteItem(ItemData _itemData)
    {
        string key = _itemData.image_name;

        if (!inventory.ContainsKey(key))
        {
            return false;
        }

        inventory.Remove(key);

        return true;
    }

    public bool UseItem(ItemData _itemData, int _count = 1)
    {
        // 조건에 맞는 경우
        return ReduceItem(_itemData, _count);
    }

    public ItemData BuyItem(ItemData _itemData, float _premium = 1f, int _count = 1)
    {
        ItemData newItemData;
        int cost = (int)(_itemData.price * _premium * _count);
        if (playerData.money >= cost && (newItemData = AddItem(_itemData, _count)) != null)
        {
            AddMoney(cost * -1);
            return newItemData;
        }

        return null;
    }

    public bool SellItem(ItemData _itemData, float _discount = 0.7f, int _count = 1)
    {
        int cost = (int)(_itemData.price * _discount * _count);
        if (ReduceItem(_itemData, _count))
        {
            AddMoney(cost);
            return true;
        }

        return false;
    }

    void AddMoney(int _amount)
    {
        playerData.money += _amount;
    }

    /**************************************************/
}