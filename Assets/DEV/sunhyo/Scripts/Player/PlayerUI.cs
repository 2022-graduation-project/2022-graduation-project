using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private Transform Top;
    private Transform Mid;
    private Transform Bottom;



    /**********/
    /*   Top  */
    /**********/
    private Transform[] status;



    /**********/
    /*   Mid  */
    /**********/
    private Transform party;
    private Transform quest;
    private Transform inventoryUI;



    /**********/
    /* Bottom */
    /**********/
    private Transform info;
    private Text player_name;
    private Text level;
    private Text cls;
    private Text money;
    private Image HpBar;
    private Image MpBar;



    void Awake()
    {
        foreach(Transform t in transform)
        {
            if (t.gameObject.name == "Top") Top = t;
            else if (t.gameObject.name == "Mid") Mid = t;
            else if (t.gameObject.name == "Bottom") Bottom = t;
        }

        foreach(Transform t in Top)
        {
            // status
        }

        foreach(Transform t in Mid)
        {
            if (t.gameObject.name == "Party") party = t;
            else if (t.gameObject.name == "Quest") quest = t;
            else if (t.gameObject.name == "InventoryUI") inventoryUI = t.GetChild(0);
        }

        foreach (Transform t in Bottom)
        {
            if (t.gameObject.name == "Info") info = t;
        }
    }

    void Update()
    {
        if (PlayerManager.instance.keyMoveable && Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.gameObject.activeInHierarchy)
                inventoryUI.gameObject.SetActive(false);
            else inventoryUI.gameObject.SetActive(true);
        }
    }

    public void Set(PlayerData _playerData)
    {
        player_name = info.Find("Player Info").Find("Name").GetComponent<Text>();   player_name.text = _playerData.name;
        level = info.Find("Player Info").Find("Level").GetComponent<Text>();        level.text = "Lv. " + _playerData.level.ToString();
        cls = info.Find("Player Info").Find("Class").GetComponent<Text>();          cls.text = _playerData.cls;
        money = info.Find("Player Info").Find("Money").GetComponent<Text>();        money.text = _playerData.money.ToString();

        HpBar = info.Find("Hp").GetChild(0).GetComponent<Image>();                  HpBar.fillAmount = 1;
        MpBar = info.Find("Mp").GetChild(0).GetComponent<Image>();                  MpBar.fillAmount = 1;
    }

    public void UpdateHpBar(float _maxHp, float _curHp)
    {
        HpBar.fillAmount = _curHp / _maxHp;
    }

    public void UpdateMpBar(float _maxMp, float _curMp)
    {
        MpBar.fillAmount = _curMp / _maxMp;
    }

    public void UpdateMoney(float _curMoney)
    {
        money.text = _curMoney.ToString();
    }
}