using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class PlayerManager : MonoBehaviour
{
    /* Data */
    public PlayerData playerData;

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
    }

    //void Start()
    //{
    //    playerData = DataManager.instance.LoadJsonFile
    //                <Dictionary<string, PlayerData>>
    //                (Application.dataPath + "/MAIN/Data", "player")
    //                ["000_player"];

    //    //foreach(Dictionary<string, ItemData> dic in playerData.inventory)
    //    //{
    //    //    foreach (KeyValuePair<string, ItemData> itemData in dic)
    //    //        print(itemData.Key);
    //    //}

    //    //UIManager.instance.Set(playerData);
    //}

    public void BuyItem(int _price)
    {
        playerData.money -= _price;
        UIManager.instance.playerUI.UpdateMoney(playerData.money);
    }

    public void UseItem(ItemData _itemData)
    {
    }

    public void GetItem()
    {

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
        float duration = 0;
        while (duration < shieldDuration)
        {
            canDamage=true;
            yield return new WaitForSeconds(1f);
            duration++;
            if (duration >= shieldDuration)
            {
                canDamage=false;
                yield break;
            }
        }
        yield break;
    }
}