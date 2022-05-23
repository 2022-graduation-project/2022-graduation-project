using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class PlayerManager : MonoBehaviour
{
    /* Data */
    public PlayerData playerData;

    /*  UI  */
    public PlayerUI playerUI;
    public ItemUI itemUI;
    public InventoryUI inventoryUI;


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

        playerUI = GameObject.Find("PlayerUI").gameObject.GetComponent<PlayerUI>();
        itemUI = GameObject.Find("Mid").transform.Find("ItemUI").GetComponent<ItemUI>();
        inventoryUI = GameObject.Find("Mid").transform.Find("InventoryUI").GetComponent<InventoryUI>();

        print(playerData);
        print(playerData.name);

        playerUI.Set(playerData);
    }

    public void GetItemBag(GameObject _itemBag)
    {
        itemUI.Set(_itemBag.GetComponent<ItemBag>());
        if (_itemBag.GetComponent<ItemBag>().EmptyCheck())
        {
            // Text UI ... ? (EMPTY)

            itemUI.CallResetWithDelay();
        };
    }

    public void LeaveItemBag()
    {
        itemUI.Reset();
    }

    public void BuyItem(int _price)
    {
        playerData.money -= _price;
        playerUI.UpdateMoney(playerData.money);
    }

    public void UseItem(ItemData _itemData)
    {
    }

    public void GetItem()
    {

    }
}