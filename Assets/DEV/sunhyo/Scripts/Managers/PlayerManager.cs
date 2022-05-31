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
    }

    void Start()
    {
        playerData = DataManager.instance.LoadJsonFile
                    <Dictionary<string, PlayerData>>
                    (Application.dataPath + "/MAIN/Data", "player")
                    ["000_player"];

        UIManager.instance.Set(playerData);
    }

    public void GetItemBag(GameObject _itemBag)
    {
        if(_itemBag.tag == "Chest")
        {
            UIManager.instance.SetItemUI2(_itemBag);
        }
        else
            UIManager.instance.SetItemUI(_itemBag);
    }

    public void LeaveItemBag(GameObject _itemBag)
    {
        if (_itemBag.tag == "Chest")
        {
            UIManager.instance.ResetItemUI2(_itemBag);
        }
        else
            UIManager.instance.ResetItemUI(_itemBag);
    }

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
}