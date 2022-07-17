using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerUI playerUI;
    public ItemUI itemUI;
    public InventoryUI inventoryUI;
    public ShopUI shopUI;

    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject shop;

    /* Singleton */
    public static UIManager instance;
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

        inventory = transform.Find("Inventory").GetChild(0).gameObject;
        shop = transform.Find("Shop").GetChild(0).gameObject;

        inventoryUI = inventory.GetComponent<InventoryUI>();
        shopUI = shop.GetComponent<ShopUI>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.activeInHierarchy == true)
                inventory.SetActive(false);
            else
                inventory.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (shop.activeInHierarchy == true)
                shop.SetActive(false);
            else
                shop.SetActive(true);
        }
    }

    public bool AddItem(ItemData _itemData, int _count = 1)
    {
        ItemData newItemData;
        if((newItemData = PlayerManager.instance.AddItem(_itemData, _count)) != null)
        {
            inventoryUI.AddItem(_itemData);
            return true;
        }

        return false;
    }

    public bool ReduceItem(ItemData _itemData, int _count = 1)
    {
        if(PlayerManager.instance.ReduceItem(_itemData, _count))
        {
            return true;
        }

        return false;
    }

    public bool DeleteItem(ItemData _itemData)
    {
        if(PlayerManager.instance.DeleteItem(_itemData))
        {
            return true;
        }

        return false;
    }

    public bool UseItem(ItemData _itemData, int _count = 1)
    {
        if (PlayerManager.instance.UseItem(_itemData, _count))
        {
            return true;
        }

        return false;
    }

    public bool BuyItem(ItemData _itemData, float _premium = 1f, int _count = 1)
    {
        ItemData newItemData;
        if ((newItemData = PlayerManager.instance.BuyItem(_itemData, _premium, _count)) != null)
        {
            inventoryUI.AddItem(_itemData);
            inventoryUI.SetMoney();
            return true;
        }
        
        return false;
    }

    public bool SellItem(ItemData _itemData, float _discount = 0.7f, int _count = 1)
    {
        if (PlayerManager.instance.SellItem(_itemData, _discount, _count))
        {
            inventoryUI.SetMoney();
            inventoryUI.Match();
            return true;
        }
        
        return false;
    }
}