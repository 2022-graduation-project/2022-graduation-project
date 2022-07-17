using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerUI playerUI;
    public ItemUI itemUI;
    public InventoryUI inventoryUI;

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.activeInHierarchy == true)
            {
                PlayerManager.instance.mouseMoveable = true;
                inventory.SetActive(false);
            }
            else
            {
                PlayerManager.instance.mouseMoveable = false;
                inventory.SetActive(true);
            }
                
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (shop.activeInHierarchy == true)
            {
                PlayerManager.instance.mouseMoveable = true;
                shop.SetActive(false);
            }
                
            else
            {
                PlayerManager.instance.mouseMoveable = false;
                shop.SetActive(true);
            }
        }
    }
    //public void Set(PlayerData _playerData)
    //{
    //    playerUI = GameObject.Find("PlayerUI").gameObject.GetComponent<PlayerUI>();
    //    itemUI = GameObject.Find("Mid").transform.Find("ItemUI").GetComponent<ItemUI>();
    //    inventoryUI = GameObject.Find("Mid").transform.Find("InventoryUI").GetComponent<exInventoryUI>();

    //    playerUI.Set(_playerData);
    //    inventoryUI.Set(_playerData);
    //}

    //public void SetItemUI(GameObject _itemBag)
    //{
    //    itemUI.Set(_itemBag.GetComponent<ItemBag>());
    //    _itemBag.GetComponent<ItemBag>().StopDeleteCoroutine();
    //}

    //public void ResetItemUI(GameObject _itemBag)
    //{
    //    itemUI.Reset(_itemBag.GetComponent<ItemBag>());
    //    // reset 할 때... coroutine을 다시 돌려야 하나?
    //    //_itemBag?.GetComponent<ItemBag>().StartDeleteCoroutine();
    //}
}