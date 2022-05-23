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
}