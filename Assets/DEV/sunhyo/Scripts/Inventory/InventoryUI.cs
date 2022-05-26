using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject[] inventorySlots;

    private PlayerData playerData;

    private Transform Info;
    private Transform Item;

    /********/
    /* Info */
    /********/
    private Transform Profile;
    private Transform Information;
    private Transform Stat;




    /***************/
    /* Information */
    /***************/
    private Text Level;
    private Text Name;
    private Image HpGauge;
    private Image MpGauge;
    private Text HpText;
    private Text MpText;
    private Text Money;
    /* struct로 만들어두면 한 번에 설정할 수 있지 않을까 */


    void Start()
    {
        foreach(Transform t in transform)
        {
            if (t.gameObject.name == "Info") Info = t;
            else if (t.gameObject.name == "Item") Item = t;
        }

        foreach (Transform t in Info)
        {
            if (t.gameObject.name == "Profile") Profile = t;
            else if (t.gameObject.name == "Information") Information = t;
            else if (t.gameObject.name == "Stat") Stat = t;
        }

        foreach (Transform t in Information)
        {
            if (t.gameObject.name == "Level") Level = t.GetComponent<Text>();
            else if (t.gameObject.name == "Name") Name = t.GetComponent<Text>();
            else if (t.gameObject.name == "Hp")
            {
                HpGauge = t.Find("Background").Find("Gauge").GetComponent<Image>();
                HpText = t.Find("Text").GetComponent<Text>();
            }
            else if (t.gameObject.name == "Mp")
            {
                MpGauge = t.Find("Background").Find("Gauge").GetComponent<Image>();
                MpText = t.Find("Text").GetComponent<Text>();
            }
            else if (t.gameObject.name == "Money") Money = t.Find("Text").GetComponent<Text>();
        }
    }

    public void Set(PlayerData _playerData)
    {
        playerData = _playerData;

        /************/
        /* Set Info */
        /************/
        Level.text = "Lv." + playerData.level.ToString();
        Name.text = playerData.name;
        HpGauge.fillAmount = playerData.curHp / playerData.maxHp;
        MpGauge.fillAmount = playerData.curMp / playerData.maxMp;
        HpText.text = playerData.curHp.ToString() + "/" + playerData.maxHp.ToString();
        MpText.text = playerData.curMp.ToString() + "/" + playerData.maxMp.ToString();
        Money.text = playerData.money.ToString();

        
    }

    public void AddItem(ItemData _itemData)
    {
        GameObject existSlot = FindSameItem(_itemData);
        if (existSlot != null)
            existSlot.GetComponent<InventorySlot>().Set(_itemData);

        else
        {
            GameObject emptySlot = GetEmptySlotInPool();
            // print(emptySlot);
            emptySlot?.GetComponent<InventorySlot>().Set(_itemData);
        }
    }

    GameObject FindSameItem(ItemData _itemData)
    {
        foreach (GameObject itemSlot in inventorySlots)
        {
            if (itemSlot.GetComponent<InventorySlot>().itemData != null &&
                itemSlot.GetComponent<InventorySlot>().itemData.item_name.Equals(_itemData.item_name))
                return itemSlot;
        }

        return null;
    }

    GameObject GetEmptySlotInPool()
    {
        foreach (GameObject itemSlot in inventorySlots)
        {
            if (itemSlot.GetComponent<InventorySlot>().itemData == null)
                return itemSlot;
        }

        return null;
    }
}