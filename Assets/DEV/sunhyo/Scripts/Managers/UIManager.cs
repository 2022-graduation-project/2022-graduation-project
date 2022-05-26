using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerUI playerUI;
    public ItemUI itemUI;
    public InventoryUI inventoryUI;

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
    }

    public void Set(PlayerData _playerData)
    {
        playerUI = GameObject.Find("PlayerUI").gameObject.GetComponent<PlayerUI>();
        itemUI = GameObject.Find("Mid").transform.Find("ItemUI").GetComponent<ItemUI>();
        inventoryUI = GameObject.Find("Mid").transform.Find("InventoryUI").GetComponent<InventoryUI>();

        playerUI.Set(_playerData);
    }

    public void SetItemUI(GameObject _itemBag)
    {
        itemUI.Set(_itemBag.GetComponent<ItemBag>());
        _itemBag.GetComponent<ItemBag>().StopDeleteCoroutine();
    }

    public void ResetItemUI(GameObject _itemBag)
    {
        itemUI.Reset(_itemBag.GetComponent<ItemBag>());
        // reset 할 때... coroutine을 다시 돌려야 하나?
        _itemBag?.GetComponent<ItemBag>().StartDeleteCoroutine();
    }
    
}