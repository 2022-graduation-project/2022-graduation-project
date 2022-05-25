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
        UIManager.instance.itemUI.Set(_itemBag.GetComponent<ItemBag>());
        if (_itemBag.GetComponent<ItemBag>().EmptyCheck())
        {
            // Text UI ... ? (EMPTY)

            UIManager.instance.itemUI.CallResetWithDelay();
        };
    }

    public void LeaveItemBag()
    {
        UIManager.instance.itemUI.Reset();
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